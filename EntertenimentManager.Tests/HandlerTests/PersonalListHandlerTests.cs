using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntertenimentManager.Tests.Repositories;
using EntertenimentManager.Domain.Commands.PersonalList;

namespace EntertenimentManager.Tests.HandlerTests
{
    [TestClass]
    public class PersonalListHandlerTests
    {
        private readonly PersonalListHandler _personalListHandler = new(new FakePersonalListRepository());
        private readonly GetAllPersonalListsByCategoryIdCommand _getAllPersonalListsCommand = new(1);
        private readonly GetPersonalListByIdCommand _getPersonalListByIdCommand = new(0);
        private readonly GetPersonalListByIdCommand _getANotExistentPersonalListByIdCommand = new(-1);
        private GenericCommandResult _result = new();

        #region GetAllPersonalListsByCategoryIdCommand        
        [TestMethod]
        public async Task ShouldReturnSuccessWhenGetAllCategoriesCommand()
        {
            var res = await _personalListHandler.Handle(_getAllPersonalListsCommand);
            _result = (GenericCommandResult)res;
            Assert.IsTrue(_result.Success);
        }
        #endregion

        #region GetPersonalListByIdCommand        
        [TestMethod]
        public async Task ShouldReturnFailWhenGetANotExistentCategoryByIdCommand()
        {
            var res = await _personalListHandler.Handle(_getANotExistentPersonalListByIdCommand);
            _result = (GenericCommandResult)res;
            Assert.IsFalse(_result.Success);
        }

        [TestMethod]
        public async Task ShouldReturnSuccessWhenGetAExistentCategoryByIdCommand()
        {
            var res = await _personalListHandler.Handle(_getPersonalListByIdCommand);
            _result = (GenericCommandResult)res;
            Assert.IsTrue(_result.Success);
        }
        #endregion
    }
}
