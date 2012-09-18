using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Abstract;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CategoriesTest
    {
        [TestMethod]
        public void Can_Create_Categories()
        {
            // Arrange
            // - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
           new Product {ProductID = 1, Name = "P1", Category = Apples},
           new Product {ProductID = 2, Name = "P2", Category = Apples},
           new Product {ProductID = 3, Name = "P3", Category = Plums},
           new Product {ProductID = 4, Name = "P4", Category = Oranges},
       }.AsQueryable());
            // Arrange - create the controller
            NavController target = new NavController(mock.Object);
            // Act = get the set of categories
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();
            // Assert

            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], Apples.Name);
            Assert.AreEqual(results[1], Oranges.Name);
            Assert.AreEqual(results[2], Plums.Name);
        }

        public Category Apples { get; set; }

        public Category Plums { get; set; }

        public Category Oranges { get; set; }
    }


}
