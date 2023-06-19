using EntertenimentManager.Domain.Entities.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        Task<bool> IsCategoryAssociatedWithUserIdAsync(int id, int requestUserId);
        Task<IEnumerable<Category>> GetAllByUserId(int userId);
        Task<Category> GetById(int Id);

    }
}
