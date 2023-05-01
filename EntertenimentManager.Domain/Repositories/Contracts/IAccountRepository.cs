using EntertenimentManager.Domain.Entities.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface IAccountRepository
    {
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<User> GetByEmailTracking(string email);
        Task<User> GetByEmailNoTracking(string email);
        Task<Role> GetRole(int roleId);
    }
}
