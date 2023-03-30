﻿using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Entities.Users;
using Flunt.Notifications;
using Flunt.Validations;

namespace EntertenimentManager.Domain.Commands.User
{
    public class CreateUserCommand : Notifiable<Notification>, ICommand
    {

        public CreateUserCommand()
        { }

        public CreateUserCommand(string name, string email, string passwordHash, string image)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Image = image;
        }

        public string Name { get; set; } = string.Empty;
        public string Email { get;  set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Image { get;  set; } = string.Empty;

        public void Validate()
        {
            AddNotifications(new Contract<CreateUserCommand>()
                .Requires()
                .IsNullOrEmpty(Name, "Informe o nome")
                .IsLowerThan(Name, 3, "O nome precisa ter pelo menos 3 caracteres")
                .IsGreaterThan(Name, 80, "O nome precisa ter no máximo 80 caracteres")
                .IsNullOrEmpty(Email, "Informe um email")
                .IsEmailOrEmpty(Email, "Informe um email válido")
                );
        }
    }
}
