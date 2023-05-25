using EntertenimentManager.Domain.Commands.Contracts;
using Flunt.Notifications;

namespace EntertenimentManager.Domain.Commands.PersonalList
{
    public class GetPersonalListByIdCommand : Notifiable<Notification>, ICommand
    {

        public GetPersonalListByIdCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; } = 0;

        public void Validate()
        {
        }
    }
}
