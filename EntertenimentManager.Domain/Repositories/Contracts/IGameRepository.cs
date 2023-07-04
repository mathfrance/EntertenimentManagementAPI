using EntertenimentManager.Domain.Entities.Itens;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface IGameRepository : IItemRepository
    {
        Task CreateAsync(Game game);
        Task UpdateAsync(Game game);
        Task DeleteAsync(Game game);
        Task<Game> GetById(int id);
        Task<IEnumerable<Game>> GetAllByPersonalId(int personalListId);
        Task<IEnumerable<Platform>> GetPlatformsByIds(IEnumerable<int> platformsIds);
    }
}
