using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Commands.PersonalList;
using EntertenimentManager.Domain.Entities.Lists;
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
        private readonly IPersonalListRepository _personalListRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PersonalListHandler(IPersonalListRepository personalListRepository, ICategoryRepository categoryRepository)
        {
            _personalListRepository = personalListRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ICommandResult> Handle(GetAllPersonalListsByCategoryIdCommand command)
        {
            if (!command.IsRequestFromAdmin && !await _categoryRepository.IsCategoryAssociatedWithUserIdAsync(command.CategoryId, command.UserId))
                return new GenericCommandResult(false, "Categoria indisponível", command.Notifications);

            var personalLists = await _personalListRepository.GetAllByCategoryId(command.CategoryId);            

            return new GenericCommandResult(true, "Listas obtidas com sucesso", personalLists);
        }
       
        public async Task<ICommandResult> Handle(GetPersonalListByIdCommand command)
        {
            if (!command.IsRequestFromAdmin && !await _personalListRepository.IsPersonalListAssociatedWithUserIdAsync(command.Id, command.UserId))
                return new GenericCommandResult(false, "Lista indisponível", command.Notifications);

            var personalList = await _personalListRepository.GetById(command.Id);

            if (personalList == null)
                return new GenericCommandResult(false, "Não foi possível obter a lista", command.Notifications);

            return new GenericCommandResult(true, "Lista obtida com sucesso", personalList);
        }        
    }
}
