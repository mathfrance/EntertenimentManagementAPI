using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Queries;
using EntertenimentManager.Domain.Repositories.Contracts;
using EntertenimentManager.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntertenimentManager.Infra.Repositories
{
    public class PersonalListRepository : IPersonalListRepository
    {
        private readonly EntertenimentManagementDataContext _context;
        public PersonalListRepository(EntertenimentManagementDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PersonalList>> GetAllByCategoryId(int categoryId)
        {
            return await _context
                        .PersonalLists
                        .AsNoTracking()
                        .Include(x => x.Items)
                        .Where(PersonalListQueries.GetByCategoryId(categoryId))
                        .ToListAsync();
        }

        public async Task<PersonalList?> GetById(int id)
        {
            return await _context
                        .PersonalLists
                        .AsNoTracking()
                        .Include(x => x.Items)
                        .FirstOrDefaultAsync(PersonalListQueries.GetById(id));

        }
    }
}
