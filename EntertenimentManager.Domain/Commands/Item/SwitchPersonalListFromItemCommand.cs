using EntertenimentManager.Domain.Commands.Contracts;
using Flunt.Notifications;

namespace EntertenimentManager.Domain.Commands.Item
{
    public class SwitchPersonalListFromItemCommand : Notifiable<Notification>, ICommandTokenAuthorization
    {
        public int ItemId { get; set; } = 0;
        public int NewPersonalListId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public bool IsRequestFromAdmin { get; set; } = false;

        public void Validate()
        {
            
        }
    }
}
