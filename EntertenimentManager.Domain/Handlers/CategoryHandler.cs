using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Category;
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Handlers.Contract;
using EntertenimentManager.Domain.Repositories.Contracts;
using Flunt.Notifications;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Handlers
{
    public class CategoryHandler :
        Notifiable<Notification>,
        IHandler<GetAllCategoriesCommand>
    {
        private readonly ICategoryRepository _repository;

        public CategoryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<ICommandResult> Handle(GetAllCategoriesCommand command)
        {
            var categories = await _repository.GetAllByUserId(command.UserId);

            if (categories == null)
                return new GenericCommandResult(false, "Não foi possível obter as categorias do usuário", command.Notifications);


            return new GenericCommandResult(true, "Categorias obtidas com sucesso", categories);
        }
    }
}
