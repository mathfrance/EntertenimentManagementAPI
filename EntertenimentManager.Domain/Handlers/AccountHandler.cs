using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Account;
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Commands.User;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Enum;
using EntertenimentManager.Domain.Handlers.Contract;
using EntertenimentManager.Domain.Repositories.Contracts;
using EntertenimentManager.Domain.SharedContext.ValueObjects;
using Flunt.Notifications;
using SecureIdentity.Password;
using System;
using System.Reflection;

namespace EntertenimentManager.Domain.Handlers
{
    public class AccountHandler :
        Notifiable<Notification>,
        IHandler<CreateAccountCommand>,
        IHandler<UpdateAccountCommand>
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

            var user = new User(command.Name, command.Email, PasswordHasher.Hash(password), command.Image.FileName);


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

        public ICommandResult Handle(UpdateAccountCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Não foi possível alterar o usuário", command.Notifications);
            }

            var user = _repository.GetByEmail(command.Email);

            user.Update(command.Name, PasswordHasher.Hash(command.Password), command.Image.FileName);

            _repository.Update(user);


            return new GenericCommandResult(true, "Usuário alterado com sucesso", user);
        }
    }
}
