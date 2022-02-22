using LearnWords.DAL.Core;
using LearnWords.DAL.Entities;
using LearnWords.DAL.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWords.Services.Commands;

public static class CreateUser
{
    public record Command(string Name, int Age, string Email, Language NativeLanguage) : IRequest;


    public class Handler : IRequestHandler<Command>
    {
        private readonly ICoreRepository _repository;

        public Handler(ICoreRepository repository)
        {
            this._repository = repository;
        }
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            User user = new User()
            {
                Name = request.Name,
                Age = request.Age,
                Email = request.Email,
                NativeLanguage = request.NativeLanguage
            };

            await _repository.Users.AddAsync(user);
            await _repository.Users.SaveAsync();
            return Unit.Value;
        }
    }
}
