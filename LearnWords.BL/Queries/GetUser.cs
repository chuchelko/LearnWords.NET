using LearnWords.DAL.Core;
using LearnWords.Services.Models;
using AutoMapper;
using MediatR;
using LearnWords.DAL.Interfaces;

namespace LearnWords.Services.Queries;

public static class GetUser
{
    public record Query(Guid UserId) : IRequest<UserDTO>;

    public class Handler : IRequestHandler<Query, UserDTO>
    {
        private readonly ICoreRepository _repository;
        private readonly IMapper _mapper;

        public Handler(ICoreRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public Task<UserDTO> Handle(Query request, CancellationToken cancellationToken)
        {
            if(request.UserId == Guid.Empty)
                return Task.FromResult(_mapper.Map<UserDTO>(_repository.GetFirstUser()));
            return Task.FromResult(_mapper.Map<UserDTO>(_repository.Users.Get(request.UserId)));
        }
    }
}
