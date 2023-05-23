using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Queries;
using EntertenimentManager.Domain.Repositories.Contracts;
using EntertenimentManager.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        .Where(CategoryQueries.GetByUserId(userId))
                        .ToListAsync();                    
        }
    }
}
