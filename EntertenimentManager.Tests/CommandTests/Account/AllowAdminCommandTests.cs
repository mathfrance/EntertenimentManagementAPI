using EntertenimentManager.Domain.Commands.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.CommandTests.Account
{
    [TestClass]
    public class AllowAdminCommandTests
    {
        private readonly AllowAdminCommand _validCommand = new("fulano@gmail.com", true);
        private readonly AllowAdminCommand _invalidCommand = new("notEmail.com", true);

        [TestMethod]
        public void ShouldReturnInvalidWhenCommandIsInvalid()
        {
            _invalidCommand.Validate();
            Assert.IsFalse(_invalidCommand.IsValid);
        }

        [TestMethod]
        public void ShouldReturnValidWhenCommandIsValid()
        {
            _validCommand.Validate();
            Assert.IsTrue(_validCommand.IsValid);
        }
    }

}
