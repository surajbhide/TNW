using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TNW;
using TNW.Controllers;

namespace TNW.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            // Arrange

            // Act
            var sut = new HomeController();

            // Assert
            sut.WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("This web application allows you to track your networth.", result.ViewBag.Message);
        }

        [Test]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
