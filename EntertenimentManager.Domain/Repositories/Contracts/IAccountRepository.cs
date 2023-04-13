using EntertenimentManager.Domain.Entities.Users;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface IAccountRepository
    {
        void Create(User user);
        void Update(User user);
        Task<User> GetByEmail(string email);
        Task<Role> GetRole(int roleId);
    }
}
