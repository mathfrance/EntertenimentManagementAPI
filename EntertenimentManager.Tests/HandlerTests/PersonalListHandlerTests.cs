using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntertenimentManager.Tests.FakeRepositories;
using EntertenimentManager.Domain.Commands.PersonalList;

namespace EntertenimentManager.Tests.HandlerTests
{
    [TestClass]
    public class PersonalListTests
    {
        private readonly PersonalListHandler _personalListHandler = new(new FakePersonalListRepository(), new FakeCategoryRepository());
        private readonly GetAllPersonalListsByCategoryIdCommand _getAllPersonalListsCommand = new() { CategoryId = 1};
        private readonly GetAllPersonalListsByCategoryIdCommand _getANotAssociateUserIdWithCategoryCommand = new() { CategoryId = 1, UserId = -1};
        private readonly GetPersonalListByIdCommand _getPersonalListByIdCommand = new();
        private readonly GetPersonalListByIdCommand _getANotExistentPersonalListByIdCommand = new() { Id = -1}; 
        private readonly GetPersonalListByIdCommand _getANotAssociateUserIdWithpersonalListCommand = new() { UserId = -1}; 
        private GenericCommandResult _result = new();

        #region GetAllPersonalListsByCategoryIdCommand        
        [TestMethod]
        public async Task ShouldReturnSuccessWhenGetAllCategoriesCommand()
        {
            var res = await _personalListHandler.Handle(_getAllPersonalListsCommand);
            _result = (GenericCommandResult)res;
            Assert.IsTrue(_result.Success);
        }

        [TestMethod]
        public async Task ShouldReturnFailWhensCategoryIsNotAssociatedWithUserIdCommand()
        {
            var res = await _personalListHandler.Handle(_getANotAssociateUserIdWithCategoryCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Categoria indisponível");
        }
        #endregion

        #region GetPersonalListByIdCommand        
        [TestMethod]
        public async Task ShouldReturnFailWhenGetANotExistentPersonalListByIdCommand()
        {
            var res = await _personalListHandler.Handle(_getANotExistentPersonalListByIdCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Não foi possível obter a lista");
        }

        [TestMethod]
        public async Task ShouldReturnSuccessWhenGetAExistentPersonalListByIdCommand()
        {
            var res = await _personalListHandler.Handle(_getPersonalListByIdCommand);
            _result = (GenericCommandResult)res;
            Assert.IsTrue(_result.Success);
        }

        [TestMethod]
        public async Task ShouldReturnFailWhensPersonalListIsNotAssociatedWithUserIdCommand()
        {
            var res = await _personalListHandler.Handle(_getANotAssociateUserIdWithpersonalListCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Lista indisponível");
        }
        #endregion
    }
}
