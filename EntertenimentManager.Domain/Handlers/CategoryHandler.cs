using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Account;
using EntertenimentManager.Domain.Commands.Category;
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Entities.Categories.Contracts;
using EntertenimentManager.Domain.Handlers.Contract;
using EntertenimentManager.Domain.Repositories.Contracts;
using Flunt.Notifications;
using SecureIdentity.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível obter as categorias do usuário", command.Notifications);


            var categories = await _repository.GetAllByUserId(command.UserId);

            if (categories == null)
                return new GenericCommandResult(false, "Não foi possível obter as categorias do usuário", command.Notifications);


            return new GenericCommandResult(true, "Categorias obtidas com sucesso", categories);
        }
    }
}
