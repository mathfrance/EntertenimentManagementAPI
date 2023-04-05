using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.SharedContext.ValueObjects;
using Flunt.Notifications;
using Flunt.Validations;

namespace EntertenimentManager.Domain.Commands.User
{
    public class CreateAccountCommand : Notifiable<Notification>, ICommand
    {

        public CreateAccountCommand()
        { }

        public CreateAccountCommand(string name, string email, string image)
        {
            Name = name;
            Email = email;
            Image = new(image);
        }

        public string Name { get; set; } = string.Empty;
        public string Email { get;  set; } = string.Empty;
        public Image Image { get;  set; }

        public void Validate()
        {
            AddNotifications(new Contract<CreateAccountCommand>()
                .Requires()
                .IsNotNullOrEmpty(Name, "Informe o nome")
                .IsGreaterThan(Name, 3, "O nome precisa ter pelo menos 3 caracteres")
                .IsLowerThan(Name, 80, "O nome precisa ter no máximo 80 caracteres")
                .IsEmail(Email, "Informe um email válido")
                );

            AddNotifications(Image.Notifications);
        }
    }
}
