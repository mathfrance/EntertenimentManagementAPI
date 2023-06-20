using EntertenimentManager.Domain.Commands.Contracts;
using Flunt.Notifications;

namespace EntertenimentManager.Domain.Commands.Category
{
    public class GetCategoryByIdCommand : Notifiable<Notification>, ICommandTokenAuthorization
    {
        public int Id { get; set; } = 0;
        public int UserId { get; set; } = 0;

        public bool IsRequestFromAdmin { get; set; } = false;

        public void Validate()
        {
        }
    }
}
