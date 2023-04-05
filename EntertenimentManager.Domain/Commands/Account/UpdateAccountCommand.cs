using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Commands.User;
using Flunt.Notifications;
using Flunt.Validations;

namespace EntertenimentManager.Domain.Commands.Account
{
    public class UpdateAccountCommand : Notifiable<Notification>, ICommand
    {

        public UpdateAccountCommand()
        { }

        public UpdateAccountCommand(string name, string passwordHash, string image)
        {
            Name = name;
            PasswordHash = passwordHash;
            Image = image;
        }

        public string Name { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;

        public void Validate()
        {
            AddNotifications(new Contract<UpdateAccountCommand>()
                .Requires()
                .IsNotNullOrEmpty(Name, "Informe o nome")
                .IsGreaterThan(Name, 3, "O nome precisa ter pelo menos 3 caracteres")
                .IsLowerThan(Name, 80, "O nome precisa ter no máximo 80 caracteres")
                );
        }
    }
}
