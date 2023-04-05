using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Repositories.Contracts;

namespace EntertenimentManager.Tests.Repositories
{
    public class FakeAccountRepositiry : IAccountRepository
    {
        public void Create(User user)
        {
        }
        public void Update(User user)
        {
        }

        public Role GetRole(int roleId)
        {
            return new Role();
        }

        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
