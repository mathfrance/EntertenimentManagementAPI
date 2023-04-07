
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Commands.User;
using Flunt.Notifications;
using Flunt.Validations;

namespace EntertenimentManager.Domain.Commands.Account
{
    public class AllowAdminCommand : Notifiable<Notification>, ICommand
    {

        public AllowAdminCommand()
        { }

        public AllowAdminCommand(string email, bool allow)
        {
            Email = email;
            Allow = allow;
        }
        public string Email { get; set; } = string.Empty;
        public bool Allow { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<CreateAccountCommand>()
                .Requires()
                .IsEmail(Email, "Informe um email válido")
                );
        }
    }
}
