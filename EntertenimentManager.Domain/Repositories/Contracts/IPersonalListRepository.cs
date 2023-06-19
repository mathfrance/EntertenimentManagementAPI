using EntertenimentManager.Domain.Entities.Lists;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface IPersonalListRepository
    {
        Task<bool> IsPersonalListAssociatedWithUserIdAsync(int id, int requestUserId);
        Task<IEnumerable<PersonalList>> GetAllByCategoryId(int categoryId);
        Task<PersonalList> GetById(int id);
    }
}
