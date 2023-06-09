﻿using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.SharedContext.ValueObjects;
using Flunt.Notifications;
using Flunt.Validations;

namespace EntertenimentManager.Domain.Commands.Account
{
    public class UpdateAccountCommand : Notifiable<Notification>, ICommand
    {

        public UpdateAccountCommand()
        { }

        public UpdateAccountCommand(string name, string email, string password, string image = "")
        {
            Name = name;
            Email = email;
            Password = password;
            NewImage = new(image);
        }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RequestEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Image NewImage { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<UpdateAccountCommand>()
                .Requires()
                .IsNotNullOrEmpty(Name, "Informe o nome")
                .IsGreaterThan(Name, 3, "O nome precisa ter pelo menos 3 caracteres")
                .IsLowerThan(Name, 80, "O nome precisa ter no máximo 80 caracteres")
                .IsEmail(Email, "Informe um email válido")
                );
        }

        public bool HasImageToUpdate()
        {
            return NewImage != null && !string.IsNullOrEmpty(NewImage.FileName);
        }
    }
}
