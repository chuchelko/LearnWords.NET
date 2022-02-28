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
    private readonly IMapper _mapper;


    public IndexModel(ILogger<IndexModel> logger, IMediator mediator, IMapper mapper)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
        Task<UserDTO> userTask = _mediator.Send(new GetUser.Query(Guid.Empty));
        userTask.Wait();
        _user = userTask.Result;
    }
    public JsonResult OnGetFetchWords(DAL.Entities.Language FirstLang, DAL.Entities.Language SecondLang)
    {
        Task<List<WordDTO>> wordsTask = _mediator.Send(new GetUsersWords.Query(_user.Id, FirstLang, SecondLang));
        wordsTask.Wait();
        List<WordDTO> _words = wordsTask.Result;

        foreach (var word in _words)
            if (word.From != FirstLang)
                (word.WordTo, word.WordFrom) = (word.WordFrom, word.WordTo);
        _logger.LogInformation($"{FirstLang} {SecondLang}");
        return new JsonResult(_words);
    }
    public void OnGetAddWords(string FirstWord, string SecondWord, DAL.Entities.Language FirstLang, DAL.Entities.Language SecondLang)
    {
        _mediator.Send(new CreateWord.Command(_user.Id, FirstLang, SecondLang, FirstWord, SecondWord, null));
        _logger.LogInformation($"Добавлен объект {FirstLang}-{SecondLang} : {FirstWord}, {SecondWord}");
    }
}
