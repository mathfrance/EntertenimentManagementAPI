using EntertenimentManager.Domain.Commands.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.CommandTests
{
    [TestClass]
    public class DeleteAccountCommandTests
    {
        private readonly DeleteAccountCommand _validCommand = new("fulano@gmail.com");
        private readonly DeleteAccountCommand _invalidCommand = new("notEmail.com");

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
