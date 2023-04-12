using EntertenimentManager.Domain.SharedContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntertenimentManager.Tests.ValueObjectsTests
{
    [TestClass]
    public class LoginTests
    {
        private readonly string _invalidEmail = "notValidEmail.com";
        private readonly string _validEmail = "Valid@email.com";
        [TestMethod]
        public void ShouldBeValidWhenPassAValidLogin()
        {
            var login = new Login("Fulano",_validEmail, "Pass123");

            Assert.IsTrue(login.IsValid);
        }


        [TestMethod]
        public void ShouldBeInvalidWhenPassAInvalidEmail()
        {
            var login = new Login("Fulano", _invalidEmail, "Pass123");

            Assert.IsFalse(login.IsValid);
        }
    }
}
