using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.UI.Controllers;
using SportsStore.UI.Infrastructure.Abstract;
using SportsStore.UI.Models;

namespace SportsStore.Tests
{
    [TestClass]
    public class AdminSecurityTests
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            // Arrange - create a mock authentication provider
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);

            //Arrange - create the view model
            LoginViewModel model = new LoginViewModel
            {
                UserName = "admin",
                Password = "secret"
            };

            // Arrange - create the controller
            AccountController target = new AccountController(mock.Object);

            // Act - authenticate using valid credentials
            ActionResult result = target.Login(model, "MyURL");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("MyURL", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Can_Login_With_Invalid_Password()
        {
            // Arrange - create a mock authentication provider
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);

            //Arrange - create the view model
            LoginViewModel model = new LoginViewModel
            {
                UserName = "admin",
                Password = "invalid"
            };

            // Arrange - create the controller
            AccountController target = new AccountController(mock.Object);

            // Act - authenticate using invalid password
            ActionResult result = target.Login(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Login_With_Invalid_UserName()
        {
            // Arrange - create a mock authentication provider
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);

            //Arrange - create the view model
            LoginViewModel model = new LoginViewModel
            {
                UserName = "invalid",
                Password = "secret"
            };

            // Arrange - create the controller
            AccountController target = new AccountController(mock.Object);

            // Act - authenticate using invalid password
            ActionResult result = target.Login(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
