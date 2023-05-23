using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Category;
using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Handlers;
using EntertenimentManager.Tests.Repositories;
using EntertenimentManager.Tests.Storages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntertenimentManager.Tests.HandlerTests
{
    [TestClass]
    public class CategoryHandlerTests
    {
        private readonly CategoryHandler _categoryHandler = new(new FakeCategoryRepository());
        private readonly GetAllCategoriesCommand _categoriesCommand = new GetAllCategoriesCommand() { UserId = 1};
        private GenericCommandResult _result = new();

        #region CategoryHandler        
        [TestMethod]
        public async Task ShouldReturnSuccessWhenGetAllCategoriesCommand()
        {
            var res = await _categoryHandler.Handle(_categoriesCommand);
            _result = (GenericCommandResult)res;
            Assert.IsTrue(_result.Success);
        }
        #endregion
    }
}
