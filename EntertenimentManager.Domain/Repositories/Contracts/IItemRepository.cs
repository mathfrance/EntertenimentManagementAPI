using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface IItemRepository
    {
        Task<PersonalList> GetPersonalListById(int id);
        Task<bool> IsItemAssociatedWithUserIdAsync(int id, int requestUserId);
        Task<bool> IsSwitchBetweenSameTypePersonalLists(int id, int requestUserId);
    }
}
