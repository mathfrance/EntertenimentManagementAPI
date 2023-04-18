using EntertenimentManager.Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Commands.Account
{
    public class LoginCommand : Notifiable<Notification>, ICommand
    {

        public LoginCommand()
        { }

        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
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
