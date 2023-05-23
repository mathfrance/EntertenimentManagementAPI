using EntertenimentManager.Domain.Entities.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface ICategoryRepository
    {      
        Task<IEnumerable<Category>> GetAllByUserId(int userId);
    }
}
