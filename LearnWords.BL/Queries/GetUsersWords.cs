using LearnWords.DAL.Core;
using LearnWords.Services.Models;
using AutoMapper;
using MediatR;
using LearnWords.DAL.Interfaces;

namespace LearnWords.Services.Queries;

public static class GetUsersWords
{
    public record Query(Guid UserId, DAL.Entities.Language FirstLang, DAL.Entities.Language SecondLang) : IRequest<List<WordDTO>>;

    public class Handler : IRequestHandler<Query, List<WordDTO>>
    {
        private readonly ICoreRepository _repository;
        private readonly IMapper _mapper;

        public Handler(ICoreRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public Task<List<WordDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_mapper.Map<List<WordDTO>>(_repository.GetUsersWords(request.UserId, request.FirstLang, request.SecondLang).ToList()));
        }
    }
}
