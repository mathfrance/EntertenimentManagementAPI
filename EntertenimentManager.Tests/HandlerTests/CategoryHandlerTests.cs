using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Category;
using EntertenimentManager.Domain.Handlers;
using EntertenimentManager.Tests.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.HandlerTests
{
    [TestClass]
    public class CategoryHandlerTests
    {
        private readonly CategoryHandler _categoryHandler = new(new FakeCategoryRepository());
        private readonly GetAllCategoriesCommand _categoriesCommand = new() { UserId = 1};
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
