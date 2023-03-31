using EntertenimentManager.Domain.Entities.Users;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface IAccountRepository
    {
        void Create(User user);
        void Update(User user);
        Role GetRole(int roleId);
    }
}
