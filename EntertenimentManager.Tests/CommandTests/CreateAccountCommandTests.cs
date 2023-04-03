using EntertenimentManager.Domain.Commands.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.CommandTests
{
    [TestClass]
    public class CreateAccountCommandTests
    {
        private readonly CreateAccountCommand _validCommand = new("Fulano", "fulano@email.com", "hashpass", "");
        private readonly CreateAccountCommand _invalidCommand = new("", "", "", "");


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