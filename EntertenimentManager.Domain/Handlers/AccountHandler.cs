using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Account;
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Enum;
using EntertenimentManager.Domain.Handlers.Contract;
using EntertenimentManager.Domain.Repositories.Contracts;
using EntertenimentManager.Domain.SharedContext.ValueObjects;
using Flunt.Notifications;
using SecureIdentity.Password;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Handlers
{
    public class AccountHandler :
        Notifiable<Notification>,
        IHandler<CreateAccountCommand>,
        IHandler<UpdateAccountCommand>,
        IHandler<AllowAdminCommand>,
        IHandler<LoginCommand>

    {
        private readonly IAccountRepository _repository;
        private readonly IImageStorage _imageStorage;

        public AccountHandler(IAccountRepository repository, IImageStorage storage)
        {
            _repository = repository;
            _imageStorage = storage;
        }

        public async Task<ICommandResult> Handle(CreateAccountCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível criar o usuário", command.Notifications);

            var password = PasswordGenerator.Generate();

            var user = new User(command.Name, command.Email, PasswordHasher.Hash(password), command.Image.FileName);


            var role = await _repository.GetRole((int)EnumRoles.user);

            user.AddRole(role);

            _repository.Create(user);

            await _imageStorage.Upload(command.Image.ImageBytes, command.Image.FileName);

            Login login = new(command.Name, command.Email, password);            

            return new GenericCommandResult(true, "Usuário criado com sucesso", login);
        }

        public async Task<ICommandResult> Handle(UpdateAccountCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível alterar o usuário", command.Notifications);

            var user = await _repository.GetByEmail(command.Email);

            user.Update(command.Name, PasswordHasher.Hash(command.Password), command.Image.FileName);

            _repository.Update(user);


            return new GenericCommandResult(true, "Usuário alterado com sucesso", user);
        }

        public async Task<ICommandResult> Handle(AllowAdminCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível alterar a permissão", command.Notifications);

            var user = await _repository.GetByEmail(command.Email);

            var role = await _repository.GetRole((int)EnumRoles.admin);

            string message;

            if (command.Allow)
            {
                user.AddRole(role);
                message = "Permissão adicionada com sucesso";
            }
            else
            {
                user.RemoveRole(role);
                message = "Permissão removida com sucesso";
            }

            _repository.Update(user);

            return new GenericCommandResult(true, message, user);
        }

        public async Task<ICommandResult> Handle(LoginCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível realizar o login", command.Notifications);

            var user = await _repository.GetByEmail(command.Email);

            if (!PasswordHasher.Verify(user.PasswordHash, command.Password))
                return new GenericCommandResult(false, "Usuário ou senha inválidos", null);

            return new GenericCommandResult(true, "Login realizado com sucesso", user);
        }
    }
}
