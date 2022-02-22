using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LearnWords.Services.Commands;
using LearnWords.Services.Queries;
using LearnWords.Services.Models;
using MediatR;
using AutoMapper;

namespace LearnWords.WebApp.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IMediator _mediator;
    public UserDTO _user;
    public List<WordDTO> _words;
    private readonly IMapper _mapper;
    public DAL.Entities.Language FirstLang { get; set; }
    public DAL.Entities.Language SecondLang { get; set; }


    public IndexModel(ILogger<IndexModel> logger, IMediator mediator, IMapper mapper)
    {
        FirstLang = DAL.Entities.Language.English;
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
        Task<UserDTO> userTask = _mediator.Send(new GetUser.Query(Guid.Empty));
        userTask.Wait();
        _user = userTask.Result;
        Task<List<WordDTO>> wordsTask = _mediator.Send(new GetUsersWords.Query(_user.Id, FirstLang, SecondLang));
        wordsTask.Wait();
        _words = wordsTask.Result;
    }
    public JsonResult OnGetFetchWords(int FirstLang, int SecondLang)
    {
        this.FirstLang = (DAL.Entities.Language)FirstLang;
        this.SecondLang = (DAL.Entities.Language)SecondLang;
        Task<List<WordDTO>> wordsTask = _mediator.Send(new GetUsersWords.Query(_user.Id, this.FirstLang, this.SecondLang));
        wordsTask.Wait();
        _words = wordsTask.Result;

        foreach (var word in _words)
            if (word.From != this.FirstLang)
                (word.WordTo, word.WordFrom) = (word.WordFrom, word.WordTo);
        _logger.LogInformation($"{FirstLang} {SecondLang}");
        return new JsonResult(_words);
    }
    public async Task<IActionResult> OnPostAddWordsAsync(string FirstLangWord, string SecondLangWord, int FirstLang, int SecondLang)
    {
        await _mediator.Send(new CreateWord.Command(_user.Id, (DAL.Entities.Language)FirstLang, (DAL.Entities.Language)SecondLang, FirstLangWord, SecondLangWord, null));
        _logger.LogInformation($"Добавлен объект {FirstLang}-{SecondLang} : {FirstLangWord}, {SecondLangWord}");
        return RedirectToPage("./Index", new { FirstLang = FirstLang, SecondLang = SecondLang });
    }
}
