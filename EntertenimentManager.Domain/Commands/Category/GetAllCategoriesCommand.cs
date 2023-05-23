using EntertenimentManager.Domain.Commands.Account;
using EntertenimentManager.Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace EntertenimentManager.Domain.Commands.Category
{
    public class GetAllCategoriesCommand : Notifiable<Notification>, ICommand
    {

        public GetAllCategoriesCommand()
        { }

        public int UserId { get; set; } = 0;

        public void Validate()
        {
        }
    }
}
