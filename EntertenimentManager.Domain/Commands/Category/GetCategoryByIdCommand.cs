using EntertenimentManager.Domain.Commands.Contracts;
using Flunt.Notifications;

namespace EntertenimentManager.Domain.Commands.Category
{
    public class GetCategoryByIdCommand : Notifiable<Notification>, ICommand
    {
        public int Id { get; set; } = 0;

        public void Validate()
        {
        }
    }
}
