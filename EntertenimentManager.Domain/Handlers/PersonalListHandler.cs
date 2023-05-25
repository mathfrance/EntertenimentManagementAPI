using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Commands.PersonalList;
using EntertenimentManager.Domain.Handlers.Contract;
using EntertenimentManager.Domain.Repositories.Contracts;
using Flunt.Notifications;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Handlers
{
    public class PersonalListHandler :
        Notifiable<Notification>,
        IHandler<GetPersonalListByIdCommand>
    {
        private readonly IPersonalListRepository _repository;

        public PersonalListHandler(IPersonalListRepository repository)
        {
            _repository = repository;
        }
        public async Task<ICommandResult> Handle(GetPersonalListByIdCommand command)
        {
            var category = await _repository.GetById(command.Id);

            if (category == null)
                return new GenericCommandResult(false, "Não foi possível obter a lista", command.Notifications);

            return new GenericCommandResult(true, "Lista obtida com sucesso", category);
        }
    }
}
