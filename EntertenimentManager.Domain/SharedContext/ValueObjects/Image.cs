using EntertenimentManager.Domain.Commands.User;
using EntertenimentManager.Domain.SharedContext.ValueObjects.Contracts;
using Flunt.Notifications;
using Flunt.Validations;
using System.Reflection;
using System.Text.RegularExpressions;
using System;

namespace EntertenimentManager.Domain.SharedContext.ValueObjects
{
    public class Image : Notifiable<Notification>, IValueObject
    {
        public string Base64Image { get; private set; } = string.Empty;
        public string FileName { get; private set; } = string.Empty;
        public byte[] ImageBytes { get; private set; }

        public Image(string base64Image)
        {
            Base64Image = base64Image;

            AddNotifications(new Contract<Image>()
               .Requires()
               .IsNotNullOrEmpty(Base64Image, "Imagem não informada")
               );

            if (this.IsValid)
            {
                ConvertImage();
            }
        }

        private void ConvertImage()
        {
            FileName = $"{Guid.NewGuid()}.jpg";
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(Base64Image, "");

            try
            {
                ImageBytes = Convert.FromBase64String(data);
            }
            catch (Exception)
            {
                AddNotification(Base64Image, "Formato de imagem suportado. Por favor insira uma imagem no formato Base 64");
            }
        }
    }
}
