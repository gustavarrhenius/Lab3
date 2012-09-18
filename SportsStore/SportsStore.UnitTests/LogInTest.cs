using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.Infrastructure.Abstract;
using Moq;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class LogInTest
    {
        [TestMethod]
           public void Can_Login_With_Valid_Credentials() {
               // Arrange - create a mock authentication provider
               Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
               mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);
               // Arrange - create the view model
               LogOnViewModel model = new LogOnViewModel {
                   UserName = "admin",
                   Password = "secret"
               };
               // Arrange - create the controller
               AccountController target = new AccountController(mock.Object);

               // Act - authenticate using valid credentials
               ActionResult result = target.LogOn(model, "/MyURL");
               // Assert
               Assert.IsInstanceOfType(result, typeof(RedirectResult));
               Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
           }
        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            // Arrange - create a mock authentication provider
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);
            // Arrange - create the view model
            LogOnViewModel model = new LogOnViewModel
            {
                UserName = "badUser",
                Password = "badPass"
            };
            // Arrange - create the controller
            AccountController target = new AccountController(mock.Object);
            // Act - authenticate using valid credentials
            ActionResult result = target.LogOn(model, "/MyURL");
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
