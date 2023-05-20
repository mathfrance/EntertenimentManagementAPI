using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Repositories.Contracts;
using SecureIdentity.Password;

namespace EntertenimentManager.Tests.Repositories
{
    public class FakeAccountRepositiry : IAccountRepository
    {
        public Task CreateAsync(User user)
        {
            return Task.CompletedTask;
        }
        public Task UpdateAsync(User user)
        {
           return Task.CompletedTask;
        }

        public Task DeleteAsync(User user)
        {
            return Task.CompletedTask;
        }

        public Task<Role> GetRole(int roleId)
        {
            return Task.FromResult(new Role());
        }

        public Task<User> GetByEmailTracking(string email)
        {
            var hashPass = PasswordHasher.Hash("Pass123");
            if (email != "fulano@email.com" && email != "beltrano@email.com")
            {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                return Task.FromResult<User>(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            }
            return Task.FromResult(new User("Fulano", "fulano@email.com", hashPass, "base64Image", new CategoryFactory()));

        }

        public Task<User> GetByEmailNoTracking(string email)
        {
            var hashPass = PasswordHasher.Hash("Pass123");
            if (email != "fulano@email.com" && email != "beltrano@email.com")
            {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                return Task.FromResult<User>(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            }
            return Task.FromResult(new User("Fulano", "fulano@email.com", hashPass, "base64Image", new CategoryFactory()));

        }        
    }
}
