using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.QueryTests
{
    [TestClass]
    public class AccountQueriesTests
    {
        private List<User> _users;

        public AccountQueriesTests()
        {
            _users = new List<User>();
            _users.Add(new User("Fulano", "fulano@email.com", "hashpass", "image"));
            _users.Add(new User("Beltrano", "beltrano@email.com", "hashpass", "image"));
            _users.Add(new User("Sicrano", "sicrano@email.com", "hashpass", "image"));
        }

        [TestMethod]
        public void ShouldReturnOnlyOneResultWhenPassAExistingEmail()
        {
            var result = _users.AsQueryable().Where(AccountQueries.GetByEmail("beltrano@email.com"));
            Assert.AreEqual(1, result.Count());
        }
    }
}
