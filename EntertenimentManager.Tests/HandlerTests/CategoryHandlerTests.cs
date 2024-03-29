﻿using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Category;
using EntertenimentManager.Domain.Handlers;
using EntertenimentManager.Tests.FakeRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.HandlerTests
{
    [TestClass]
    public class CategoryHandlerTests
    {
        private readonly CategoryHandler _categoryHandler = new(new FakeCategoryRepository());
        private readonly GetAllCategoriesCommand _getAllCategoriesCommand = new() { UserId = 1};
        private readonly GetCategoryByIdCommand _getCategoryByIdCommand = new();
        private readonly GetCategoryByIdCommand _getANotExistentCategoryByIdCommand = new() { Id = -1 };
        private readonly GetCategoryByIdCommand _getANotAssociateUserIdCommand = new() { UserId = -1 };
        private GenericCommandResult _result = new();

        #region GetAllCategoriesCommand        
        [TestMethod]
        public async Task ShouldReturnSuccessWhenGetAllCategoriesCommand()
        {
            var res = await _categoryHandler.Handle(_getAllCategoriesCommand);
            _result = (GenericCommandResult)res;
            Assert.IsTrue(_result.Success);
        }
        #endregion

        #region GetCategoryByIdCommand        
        [TestMethod]
        public async Task ShouldReturnFailWhenGetANotExistentCategoryByIdCommand()
        {
            var res = await _categoryHandler.Handle(_getANotExistentCategoryByIdCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Não foi possível obter a categoria informada");
        }

        [TestMethod]
        public async Task ShouldReturnSuccessWhenGetAExistentCategoryByIdCommand()
        {
            var res = await _categoryHandler.Handle(_getCategoryByIdCommand);
            _result = (GenericCommandResult)res;
            Assert.IsTrue(_result.Success);
        }

        [TestMethod]
        public async Task ShouldReturnFailWhensCategoryIsNotAssociatedWithUserIdCommand()
        {
            var res = await _categoryHandler.Handle(_getANotAssociateUserIdCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Categoria indisponível");
        }
        #endregion
    }
}
