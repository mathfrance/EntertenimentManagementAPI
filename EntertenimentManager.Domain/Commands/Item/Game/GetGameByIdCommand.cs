using EntertenimentManager.Domain.Commands.Contracts;
using Flunt.Notifications;

namespace EntertenimentManager.Domain.Commands.Item.Game
{
    public class GetGameByIdCommand : Notifiable<Notification>, ICommandTokenAuthorization
    {
        public int UserId { get; set; } = 0;
        public int Id { get; set; } = 0;
        public bool IsRequestFromAdmin { get; set; } = false;

        public void Validate()
        {
        }
    }
}
