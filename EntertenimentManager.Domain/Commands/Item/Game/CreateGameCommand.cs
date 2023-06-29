using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.SharedContext.ValueObjects;
using Flunt.Notifications;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Commands.Item.Game
{
    public class CreateGameCommand : Notifiable<Notification>, ICommandTokenAuthorization
    {
        public string Developer { get; set; } = string.Empty;
        public List<Platform> Platforms { get; set; } = new();
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public Image ThumbImage { get; set; }
        public int ReleaseYear { get; set; } = 0;
        public int BelongsToId { get; set; }
        public int UserId { get; set; } = 0;
        public bool IsRequestFromAdmin { get; set; } = false;

        public void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}
