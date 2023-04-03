using EntertenimentManager.Domain.Commands.User;
using EntertenimentManager.Domain.Entities.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.EntityTests
{
    [TestClass]
    public class UserEntityTests
    {
        private readonly User _user = new("Fulano", "fulano@email.com", "hashpass");
        private const int EMPTY = 0;


        [TestMethod]
        public void ShouldNotAddRoleWhenIsNull()
        {
            _user.AddRole(null);
            Assert.AreEqual(EMPTY, _user.Roles.Count);
        }

        [TestMethod]
        public void ShouldAddRoleWhenIsNotNull()
        {
            _user.AddRole(new());
            Assert.AreNotEqual(EMPTY, _user.Roles.Count);
        }
    }
}
