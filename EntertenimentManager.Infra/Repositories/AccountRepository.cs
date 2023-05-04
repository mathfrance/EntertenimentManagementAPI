using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Queries;
using EntertenimentManager.Domain.Repositories.Contracts;
using EntertenimentManager.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntertenimentManager.Infra.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly EntertenimentManagementDataContext _context;
        public AccountRepository(EntertenimentManagementDataContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public Task<User?> GetByEmailTracking(string email)
        {
            return _context
                        .Users
                        .Include(x => x.Roles)
                        .Include(x => x.Categories)
                        .FirstOrDefaultAsync(AccountQueries.GetByEmail(email));
        }

        public Task<User?> GetByEmailNoTracking(string email)
        {
            return _context
                        .Users
                        .AsNoTracking()
                        .Include(x => x.Roles)
                        .Include(x => x.Categories)
                        .FirstOrDefaultAsync(AccountQueries.GetByEmail(email));
        }

        public Task<Role?> GetRole(int roleId)
        {
            return _context.Roles.FirstOrDefaultAsync(AccountQueries.GetRoleById(roleId));
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
