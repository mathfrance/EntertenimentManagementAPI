using EntertenimentManager.Domain.Commands.Contracts;
using Flunt.Notifications;

namespace EntertenimentManager.Domain.Commands.PersonalList
{
    public class GetAllPersonalListsByCategoryIdCommand : Notifiable<Notification>, ICommand
    {
        public int CategoryId { get; set; } = 0;

        public void Validate()
        {
        }
    }
}