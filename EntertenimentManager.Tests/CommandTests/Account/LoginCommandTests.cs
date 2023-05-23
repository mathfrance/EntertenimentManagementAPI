using EntertenimentManager.Domain.Commands.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.CommandTests
{
    [TestClass]
    public class LoginCommandTests
    {
        private readonly LoginCommand _validCommand = new("fulano@gmail.com", "pass123");
        private readonly LoginCommand _invalidCommand = new("notEmail.com", "pass123");

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
