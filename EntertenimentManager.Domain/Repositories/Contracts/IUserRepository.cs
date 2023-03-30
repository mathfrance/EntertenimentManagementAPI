using EntertenimentManager.Domain.Entities.Users;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface IUserRepository
    {
        void Create(User user);
        void Update(User user);
    }
}
