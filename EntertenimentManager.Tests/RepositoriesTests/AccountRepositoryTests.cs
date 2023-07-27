using EntertenimentManager.Infra.Repositories;
using EntertenimentManager.Tests.RepositoriesTests.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.RepositoriesTests
{
    [TestClass]
    public class AccountRepositoryTests
    {
        private readonly AccountRepository _repository;
        private readonly SqliteBuider _sqlLite;

        public AccountRepositoryTests()
        {
            _sqlLite = new SqliteBuider();
            _repository = new(_sqlLite.CreateDatabase().InicialLoad().Build());

        }

        [TestMethod]
        public async Task GetRolesTest()
        {
            var returno = await _repository.GetRole(1);
            Assert.IsNotNull(returno);
        }
    }
}
