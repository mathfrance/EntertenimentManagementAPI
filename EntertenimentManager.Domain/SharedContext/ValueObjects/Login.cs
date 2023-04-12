using EntertenimentManager.Domain.Commands.User;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.SharedContext.ValueObjects.Contracts;
using Flunt.Notifications;
using Flunt.Validations;
using SecureIdentity.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.SharedContext.ValueObjects
{
    public class Login : Notifiable<Notification>, IValueObject
    {

        public Login(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;

            AddNotifications(new Contract<Login>()
                .Requires()
                .IsNotNullOrEmpty(Name, "Informe o nome")
                .IsGreaterThan(Name, 3, "O nome precisa ter pelo menos 3 caracteres")
                .IsLowerThan(Name, 80, "O nome precisa ter no máximo 80 caracteres")
                .IsEmail(Email, "Informe um email válido")
                );
        }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
    }
}
