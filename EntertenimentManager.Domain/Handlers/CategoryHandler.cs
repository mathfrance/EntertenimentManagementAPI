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
        IHandler<GetAllCategoriesCommand>,
        IHandler<GetCategoryByIdCommand>

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

        public async Task<ICommandResult> Handle(GetCategoryByIdCommand command)
        {
            if (!command.IsRequestFromAdmin && !await _repository.IsCategoryAssociatedWithUserIdAsync(command.Id, command.UserId))
                return new GenericCommandResult(false, "Não foi possível obter a categoria informada", command.Notifications);

            var category = await _repository.GetById(command.Id);

            if (category == null)
                return new GenericCommandResult(false, "Não foi possível obter a categoria informada", command.Notifications);

            return new GenericCommandResult(true, "Categoria obtida com sucesso", category);
        }
    }
}
