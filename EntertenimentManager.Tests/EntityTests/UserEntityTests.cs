using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Enumerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.EntityTests
{
    [TestClass]
    public class UserEntityTests
    {
        private readonly User _user = new("Fulano", "fulano@email.com", "hashpass", "image", new CategoryFactory());
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

        [TestMethod]
        public void ShouldCreateAllCategoriesFromEnum()
        {
            _user.CreateCategories();
            var length = Enum.GetValues(typeof(EnumCategories)).Length;
            Assert.AreEqual(length, _user.Categories.Count);
        }

        [TestMethod]
        public void ShouldNotAddExistsCategoriesFromEnum()
        {
            _user.CreateCategories();
            _user.UpdateCategories();
            var length = Enum.GetValues(typeof(EnumCategories)).Length;
            Assert.AreEqual(length, _user.Categories.Count);
        }

        [TestMethod]
        public void ShouldAddNewCategoriesFromEnumWhenItsUpdated()
        {
            var length = Enum.GetValues(typeof(EnumCategories)).Length;
            _user.UpdateCategories();
            Assert.AreEqual(length, _user.Categories.Count);
        }
    }
}
