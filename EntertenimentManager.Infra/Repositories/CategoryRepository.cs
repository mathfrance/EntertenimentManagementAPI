using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Queries;
using EntertenimentManager.Domain.Repositories.Contracts;
using EntertenimentManager.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntertenimentManager.Infra.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EntertenimentManagementDataContext _context;
        public CategoryRepository(EntertenimentManagementDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllByUserId(int userId)
        {
            return await _context
                        .Categories
                        .AsNoTracking()
                        .Include(x => x.Lists)
                        .Where(CategoryQueries.GetByUserId(userId))
                        .ToListAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _context
                        .Categories
                        .AsNoTracking()
                        .Include(x => x.Lists)
                        .FirstOrDefaultAsync(CategoryQueries.GetById(id));

        }

        public async Task<bool> IsCategoryAssociatedWithUserIdAsync(int id, int requestUserId)
        {
            return await _context.Categories
                        .AnyAsync(c => c.Id == id && c.Owner.Id == requestUserId);
        }
    }
}
