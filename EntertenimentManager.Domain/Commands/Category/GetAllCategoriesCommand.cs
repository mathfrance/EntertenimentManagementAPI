using EntertenimentManager.Domain.Commands.Contracts;
using Flunt.Notifications;

namespace EntertenimentManager.Domain.Commands.Category
{
    public class GetAllCategoriesCommand : Notifiable<Notification>, ICommand
    {
        public int UserId { get; set; } = 0;
        public void Validate()
        {            
        }
    }
}
