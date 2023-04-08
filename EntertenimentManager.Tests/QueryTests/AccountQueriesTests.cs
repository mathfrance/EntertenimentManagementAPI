using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.QueryTests
{
    [TestClass]
    public class AccountQueriesTests
    {
        private readonly List<User> _users;

        public AccountQueriesTests()
        {
            _users = new List<User>
            {
                new User("Fulano", "fulano@email.com", "hashpass", "image"),
                new User("Beltrano", "beltrano@email.com", "hashpass", "image"),
                new User("Sicrano", "sicrano@email.com", "hashpass", "image")
            };
        }

        [TestMethod]
        public void ShouldReturnOnlyOneResultWhenPassAExistingEmail()
        {
            var result = _users.AsQueryable().Where(AccountQueries.GetByEmail("beltrano@email.com"));
            Assert.AreEqual(1, result.Count());
        }
    }
}
