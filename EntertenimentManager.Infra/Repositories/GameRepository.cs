using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Queries;
using EntertenimentManager.Domain.Repositories.Contracts;
using EntertenimentManager.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EntertenimentManager.Infra.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly EntertenimentManagementDataContext _context;
        public GameRepository(EntertenimentManagementDataContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Game game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetAllByPersonalId(int personalListId)
        {
            return await _context
                        .Games
                        .AsNoTracking()
                        .Where(GameQueries.GetByPersonalListId(personalListId))
                        .ToListAsync();
        }

        public async Task<Game?> GetById(int id)
        {
            return await _context
                       .Games
                       .FirstOrDefaultAsync(GameQueries.GetById(id));
        }

        public async Task<PersonalList?> GetPersonalListById(int id)
        {
            return await _context
                        .PersonalLists
                        .Include(x => x.Category)
                        .FirstOrDefaultAsync(PersonalListQueries.GetById(id));
        }

        public async Task<IEnumerable<Platform>> GetPlatformsByIds(IEnumerable<int> platformsIds)
        {
            return await _context
                        .Platforms
                        .Where(GameQueries.GetAllPlatformsByIds(platformsIds))
                        .ToListAsync();
        }

        public async Task<bool> IsItemAssociatedWithUserIdAsync(int id, int requestUserId)
        {
            return await _context.Games
                        .AnyAsync(g => g.Id == id && g.BelongsTo.Category.Owner.Id == requestUserId);
        }

        public async Task<bool> IsSwitchBetweenSameTypePersonalLists(int itemId, int newPersonalListCategoryType)
        {
            return await _context.Games
                        .AnyAsync(g => g.Id == itemId && g.BelongsTo.Category.Type == newPersonalListCategoryType);
        }

        public async Task UpdateAsync(Game game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }
    }
}
