using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Commands.User;
using EntertenimentManager.Domain.Handlers;
using EntertenimentManager.Tests.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.HandlerTests
{
    [TestClass]
    public class AccountHandlerTests
    {
        private readonly CreateAccountCommand _validCommand = new("Fulano", "fulano@email.com", "hashpass", "");
        private readonly CreateAccountCommand _invalidCommand = new("", "", "", "");
        private readonly AccountHandler _accountHandler = new (new FakeAccountRepositiry());
        private GenericCommandResult _result = new(); 


        [TestMethod]
        public void ShouldReturnFailWhenCommandIsInvalid()
        {
            _result = (GenericCommandResult)_accountHandler.Handle(_invalidCommand);
            Assert.IsFalse(_result.Success);
        }

        [TestMethod]
        public void ShouldReturnValidWhenCommandIsValid()
        {
            _result = (GenericCommandResult)_accountHandler.Handle(_validCommand);
            Assert.IsTrue(_result.Success);
        }
    }
}
