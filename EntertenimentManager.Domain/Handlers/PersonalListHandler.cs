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
        IHandler<GetAllPersonalListsByCategoryIdCommand>,
        IHandler<GetPersonalListByIdCommand>
    {
        private readonly IPersonalListRepository _repository;

        public async Task<ICommandResult> Handle(GetAllPersonalListsByCategoryIdCommand command)
        {
            var personalLists = await _repository.GetAllByCategoryId(command.CategoryId);

            return new GenericCommandResult(true, "Listas obtidas com sucesso", personalLists);
        }

        public PersonalListHandler(IPersonalListRepository repository)
        {
            _repository = repository;
        }
        public async Task<ICommandResult> Handle(GetPersonalListByIdCommand command)
        {
            var personalList = await _repository.GetById(command.Id);

            if (personalList == null)
                return new GenericCommandResult(false, "Não foi possível obter a lista", command.Notifications);

            return new GenericCommandResult(true, "Lista obtida com sucesso", personalList);
        }        
    }
}
