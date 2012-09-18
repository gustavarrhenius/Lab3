using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Controllers;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using Moq;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class FilterProductsTest
    {
        [TestMethod]
        public void Can_Filter_Products()
        {
            // Arrange
            // - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
           new Product {ProductID = 1, Name = "P1", Category = Cat1},
           new Product {ProductID = 2, Name = "P2", Category = Cat2},
           new Product {ProductID = 3, Name = "P3", Category = Cat1},
           new Product {ProductID = 4, Name = "P4", Category = Cat2},
           new Product {ProductID = 5, Name = "P5", Category = Cat3}
       }.AsQueryable());
            // Arrange - create a controller and make the page size 3 items
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            // Action
            Product[] result = ((ProductsListViewModel)controller.List("Cat2", 1).Model)
                .Products.ToArray();
            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == Cat2);
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == Cat2);
        }

        public Category Cat3 { get; set; }

        public Category Cat2 { get; set; }

        public Category Cat1 { get; set; }
    }
}
