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

            return new GenericCommandResult(true, "Categorias obtidas com sucesso", categories);
        }
    }
}
