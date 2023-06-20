using EntertenimentManager.Domain.Commands.Account;
using EntertenimentManager.Domain.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace EntertenimentManager.Domain.Commands.Category
{
    public class GetAllCategoriesCommand : Notifiable<Notification>, ICommandTokenAuthorization
    {
        public int UserId { get; set; } = 0;
        public bool IsRequestFromAdmin { get; set; } = false;
        public void Validate()
        {
        }
    }
}
