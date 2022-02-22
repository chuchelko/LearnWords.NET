using LearnWords.DAL.Entities;
using LearnWords.DAL.Interfaces;
using MediatR;

namespace LearnWords.Services.Commands;

public static class CreateWord
{
    public record Command(Guid UserId, Language From, Language To, string WordFrom
        , string WordTo, string? Description) : IRequest;


    public class Handler : IRequestHandler<Command>
    {
        private readonly ICoreRepository _repository;

        public Handler(ICoreRepository repository)
        {
            this._repository = repository;
        }
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            Word word = new Word()
            {
                UserId = request.UserId,
                From = request.From,
                To = request.To,
                WordFrom = request.WordFrom,
                WordTo = request.WordTo,
                Description = request.Description
            };

            await _repository.Words.AddAsync(word);
            await _repository.Words.SaveAsync();
            return Unit.Value;
        }
    }
}
