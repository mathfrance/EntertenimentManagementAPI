using EntertenimentManager.Domain.Entities.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface IAccountRepository
    {
        void Create(User user);
        void Update(User user);
        Task<User> GetByEmailTracking(string email);
        Task<User> GetByEmailNoTracking(string email);
        Task<Role> GetRole(int roleId);
    }
}
