﻿using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Repositories.Contracts;
using SecureIdentity.Password;

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

        public Task<Role> GetRole(int roleId)
        {
            return Task.FromResult(new Role());
        }

        public Task<User> GetByEmail(string email)
        {
            var hashPass = PasswordHasher.Hash("Pass123");
            return Task.FromResult(new User("Fulano", "fulano@email.com", hashPass, "base64Image"));
        }
    }
}
