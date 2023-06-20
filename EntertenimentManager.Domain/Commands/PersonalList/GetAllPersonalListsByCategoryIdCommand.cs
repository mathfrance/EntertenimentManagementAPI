using EntertenimentManager.Domain.Commands.Contracts;
using Flunt.Notifications;

namespace EntertenimentManager.Domain.Commands.PersonalList
{
    public class GetAllPersonalListsByCategoryIdCommand : Notifiable<Notification>, ICommandTokenAuthorization
    {
        public int CategoryId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public bool IsRequestFromAdmin { get; set; } = false;

        public void Validate()
        {
        }
    }
}