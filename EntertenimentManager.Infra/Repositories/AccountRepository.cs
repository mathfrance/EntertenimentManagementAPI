using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Enum;
using EntertenimentManager.Domain.Queries;
using EntertenimentManager.Domain.Repositories.Contracts;
using EntertenimentManager.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EntertenimentManager.Infra.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly EntertenimentManagementDataContext _context;
        public AccountRepository(EntertenimentManagementDataContext context)
        {
            _context = context;
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User? GetByEmail(string email)
        {
            return _context
                        .Users
                        .AsNoTracking()
                        .Include(x => x.Roles)
                        .FirstOrDefault(AccountQueries.GetByEmail(email));
        }

        public Role? GetRole(int roleId)
        {
            return _context.Roles.FirstOrDefault(AccountQueries.GetRoleById(roleId));
        }

        public void Update(User user)
        {
            var userUpdated = _context
                        .Users
                        .AsNoTracking()
                        .Include(x => x.Roles)
                        .FirstOrDefaultAsync(AccountQueries.GetByEmail(user.Email));

        }
    }
}
