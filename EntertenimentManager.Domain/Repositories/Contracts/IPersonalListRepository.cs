using EntertenimentManager.Domain.Entities.Lists;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface IPersonalListRepository
    {
        Task<IEnumerable<PersonalList>> GetAllByCategoryId(int categoryId);
        Task<PersonalList> GetById(int id);
    }
}
