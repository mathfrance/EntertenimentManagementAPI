using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Account;
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Entities.Categories.Contracts;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Enumerators;
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
        IHandler<LoginCommand>,
        IHandler<DeleteAccountCommand>

    {
        private readonly IAccountRepository _repository;
        private readonly IImageStorage _imageStorage;
        private readonly ICategoryFactory _categoryFactory;

        public AccountHandler(IAccountRepository repository, IImageStorage storage, ICategoryFactory categoryFactory)
        {
            _repository = repository;
            _imageStorage = storage;
            _categoryFactory = categoryFactory;
        }

        public async Task<ICommandResult> Handle(CreateAccountCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível criar o usuário", command.Notifications);

            var password = PasswordGenerator.Generate();

            var user = new User(command.Name, command.Email, PasswordHasher.Hash(password), command.Image.FileName, _categoryFactory);

            user.CreateCategories();

            var role = await _repository.GetRole((int)EnumRoles.user);

            user.AddRole(role);

            await _repository.CreateAsync(user);

            await _imageStorage.UploadAsync(command.Image.ImageBytes, command.Image.FileName);

            Login login = new(command.Name, command.Email, password);            

            return new GenericCommandResult(true, "Usuário criado com sucesso", login);
        }

        public async Task<ICommandResult> Handle(UpdateAccountCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível alterar o usuário", command.Notifications);

            if(command.RequestEmail != command.Email)
            {
                var existingEmail = await _repository.GetByEmailNoTracking(command.Email);
                if (existingEmail != null)
                    return new GenericCommandResult(false, "Não foi possível alterar o usuário, já existe um cadastro com o email informado", command.Notifications);
            }                

            var user = await _repository.GetByEmailTracking(command.RequestEmail);

            if (user == null)
                return new GenericCommandResult(false, "Não foi possível alterar o usuário", command.Notifications);

            user.UpdateCategories();

            if (command.HasImageToUpdate())
            {
                user.Update(command.Name, command.Email, PasswordHasher.Hash(command.Password), command.NewImage.FileName);
                await _imageStorage.UploadAsync(command.NewImage.ImageBytes, command.NewImage.FileName);
            }
            else
            {
                user.Update(command.Name, command.Email, PasswordHasher.Hash(command.Password), "");
            }           

            await _repository.UpdateAsync(user);            

            return new GenericCommandResult(true, "Usuário alterado com sucesso", null);
        }

        public async Task<ICommandResult> Handle(AllowAdminCommand command)
        {

            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível alterar a permissão", command.Notifications);

            var user = await _repository.GetByEmailTracking(command.Email);

            if (user == null)
                return new GenericCommandResult(false, "Não foi possível alterar a permissão", command.Notifications);

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

            await _repository.UpdateAsync(user);

            return new GenericCommandResult(true, message, null);
        }

        public async Task<ICommandResult> Handle(LoginCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível realizar o login", command.Notifications);

            var user = await _repository.GetByEmailNoTracking(command.Email);

            if (user == null)
                return new GenericCommandResult(false, "Usuário ou senha inválidos", null);

            if (!PasswordHasher.Verify(user.PasswordHash, command.Password))
                return new GenericCommandResult(false, "Usuário ou senha inválidos", null);

            return new GenericCommandResult(true, "Login realizado com sucesso", user);
        }

        public async Task<ICommandResult> Handle(DeleteAccountCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível realizar a exclusão", command.Notifications);

            var user = await _repository.GetByEmailTracking(command.Email);

            if (user == null)
                return new GenericCommandResult(false, "Não foi possível realizar a exclusão", null);

            await _repository.DeleteAsync(user);

            return new GenericCommandResult(true, "Exclusão realizado com sucesso", user);
        }
    }
}
