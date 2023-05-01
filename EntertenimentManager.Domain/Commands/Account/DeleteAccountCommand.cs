using EntertenimentManager.Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace EntertenimentManager.Domain.Commands.Account
{
    public class DeleteAccountCommand : Notifiable<Notification>, ICommand
    {

        public DeleteAccountCommand()
        { }

        public DeleteAccountCommand(string email)
        {
            Email = email;
        }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<CreateAccountCommand>()
                .Requires()
                .IsEmail(Email, "Informe um email válido")
                );
        }
    }
}
