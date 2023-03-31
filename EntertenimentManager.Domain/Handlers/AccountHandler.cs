using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Commands.User;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Enum;
using EntertenimentManager.Domain.Handlers.Contract;
using EntertenimentManager.Domain.Repositories.Contracts;
using Flunt.Notifications;
using SecureIdentity.Password;
using System;

namespace EntertenimentManager.Domain.Handlers
{
    public class AccountHandler :
        Notifiable<Notification>,
        IHandler<CreateAccountCommand>
    {
        private readonly IAccountRepository _repository;

        public AccountHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateAccountCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Não foi possível criar o usuário", command.Notifications);
            }

            var password = PasswordGenerator.Generate();
            var user = new User(command.Name, command.Email, PasswordHasher.Hash(password));

            var role = _repository.GetRole((int)EnumRoles.user);

            user.AddRole(role);

            _repository.Create(user);

            var login = new
            {
                command.Name,
                command.Email,
                Password = password
            };

            return new GenericCommandResult(true, "Usuário criado com sucesso", login);
        }
    }
}
