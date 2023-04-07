using EntertenimentManager.Domain.Entities.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.EntityTests
{
    [TestClass]
    public class UserEntityTests
    {
        private readonly User _user = new("Fulano", "fulano@email.com", "hashpass", "image");
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

        [TestMethod]
        public void ShouldNotAddRoleWhenItsAlreadyExist()
        {
            _user.AddRole(new());
            _user.AddRole(new());
            Assert.AreEqual(1, _user.Roles.Count);
        }

        [TestMethod]
        public void ShouldNotRemoveRoleWhenIsNull()
        {
            _user.AddRole(new());
            _user.RemoveRole(null);
            Assert.AreNotEqual(EMPTY, _user.Roles.Count);
        }

        [TestMethod]
        public void ShouldRemoveRoleWhenIsNotNull()
        {
            _user.AddRole(new());
            _user.RemoveRole(new());
            Assert.AreEqual(EMPTY, _user.Roles.Count);
        }
    }
}
