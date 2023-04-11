using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.QueryTests
{
    [TestClass]
    public class AccountQueriesTests
    {
        private readonly List<User> _users;
        private readonly List<Role> _roles;
        private readonly int _existingIdRole = 0;
        private readonly int _notExistingIdRole = 99;
        public AccountQueriesTests()
        {
            _users = new List<User>
            {
                new User("Fulano", "fulano@email.com", "hashpass", "image"),
                new User("Beltrano", "beltrano@email.com", "hashpass", "image"),
                new User("Sicrano", "sicrano@email.com", "hashpass", "image")
            };

            _roles = new List<Role>
            {
                new Role()
            };
        }

        [TestMethod]
        public void ShouldReturnOnlyOneResultWhenPassAExistingEmail()
        {
            var result = _users.AsQueryable().Where(AccountQueries.GetByEmail("beltrano@email.com"));
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void ShouldReturnOneResultWhenPassAExistingId()
        {
            var result = _roles.AsQueryable().Where(AccountQueries.GetRoleById(_existingIdRole));
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void ShouldNotReturnResultWhenPassANotExistingId()
        {
            var result = _roles.AsQueryable().Where(AccountQueries.GetRoleById(_notExistingIdRole));
            Assert.AreEqual(0, result.Count());
        }
    }
}
