using EntertenimentManager.Domain.Commands.Contracts;
using Flunt.Notifications;

namespace EntertenimentManager.Domain.Commands.Category
{
    public class GetCategoryByIdCommand : Notifiable<Notification>, ICommand
    {
        public GetCategoryByIdCommand()
        {
            
        }
        public GetCategoryByIdCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; } = 0;

        public void Validate()
        {
        }
    }
}
