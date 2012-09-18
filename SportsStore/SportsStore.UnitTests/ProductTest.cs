using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using Moq;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
           new Product {ProductID = 1, Name = "P1"},
           new Product {ProductID = 2, Name = "P2"},
           new Product {ProductID = 3, Name = "P3"},
       }.AsQueryable());
            // Arrange - create a controller
            AdminController target = new AdminController(mock.Object);
            // Action
            Product[] result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();
            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }

         [TestMethod]
           public void Can_Edit_Product() {
               // Arrange - create the mock repository
               Mock<IProductRepository> mock = new Mock<IProductRepository>();
               mock.Setup(m => m.Products).Returns(new Product[] {
                   new Product {ProductID = 1, Name = "P1"},
                   new Product {ProductID = 2, Name = "P2"},
                   new Product {ProductID = 3, Name = "P3"},
               }.AsQueryable());
               // Arrange - create the controller
               AdminController target = new AdminController(mock.Object);
               // Act
               Product p1 = target.Edit(1).ViewData.Model as Product;
               Product p2 = target.Edit(2).ViewData.Model as Product;
               Product p3 = target.Edit(3).ViewData.Model as Product;
               // Assert
               Assert.AreEqual(1, p1.ProductID);
               Assert.AreEqual(2, p2.ProductID);
               Assert.AreEqual(3, p3.ProductID);
           }
             [TestMethod]
             public void Cannot_Edit_Nonexistent_Product()
             {
                 // Arrange - create the mock repository
                 Mock<IProductRepository> mock = new Mock<IProductRepository>();
                 mock.Setup(m => m.Products).Returns(new Product[] {
               new Product {ProductID = 1, Name = "P1"},
               new Product {ProductID = 2, Name = "P2"},
               new Product {ProductID = 3, Name = "P3"},
           }.AsQueryable());
                 // Arrange - create the controller
                 AdminController target = new AdminController(mock.Object);
                 // Act
                 Product result = (Product)target.Edit(4).ViewData.Model;
                 // Assert
                 Assert.IsNull(result);
             }


             [TestMethod]
             public void Can_Delete_Valid_Products()
             {
                 // Arrange - create a Product
                 Product prod = new Product { ProductID = 2, Name = "Test" };
                 // Arrange - create the mock repository
                 Mock<IProductRepository> mock = new Mock<IProductRepository>();
                 mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                prod,
                new Product {ProductID = 3, Name = "P3"},
            }.AsQueryable());
                            // Arrange - create the controller
                            AdminController target = new AdminController(mock.Object);
                            // Act - delete the product
                            target.Delete(prod.ProductID);
                            // Assert - ensure that the repository delete method was
                            // called with the correct Product
                            mock.Verify(m => m.DeleteProduct(prod));
                        }

                [TestMethod]
                public void Cannot_Delete_Invalid_Products() {
                    // Arrange - create the mock repository
                    Mock<IProductRepository> mock = new Mock<IProductRepository>();
                    mock.Setup(m => m.Products).Returns(new Product[] {
                        new Product {ProductID = 1, Name = "P1"},
                        new Product {ProductID = 2, Name = "P2"},
                        new Product {ProductID = 3, Name = "P3"},
                    }.AsQueryable());
                    // Arrange - create the controller
                    AdminController target = new AdminController(mock.Object);
                    // Act - delete using an ID that doesn't exist
                    target.Delete(100);
                    // Assert - ensure that the repository delete method was
                    // called with the correct Product
                    mock.Verify(m => m.DeleteProduct(It.IsAny<Product>()), Times.Never());
                }

            [TestMethod]
           public void Can_Retrieve_Image_Data() {
               // Arrange - create a Product with image data
               Product prod = new Product {
                   ProductID = 2,
                   Name = "Test",
                   ImageData = new byte[] {},
                   ImageMimeType = "image/png" };
               // Arrange - create the mock repository
               Mock<IProductRepository> mock = new Mock<IProductRepository>();
               mock.Setup(m => m.Products).Returns(new Product[] {
                   new Product {ProductID = 1, Name = "P1"},
                   prod,
                   new Product {ProductID = 3, Name = "P3"}
               }.AsQueryable());
               // Arrange - create the controller
               ProductController target = new ProductController(mock.Object);
               // Act - call the GetImage action method
               ActionResult result = target.GetImage(2);
               // Assert
               Assert.IsNotNull(result);
               Assert.IsInstanceOfType(result, typeof(FileResult));
               Assert.AreEqual(prod.ImageMimeType, ((FileResult)result).ContentType);
           }


            [TestMethod]
            public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
            {
                // Arrange - create the mock repository
                Mock<IProductRepository> mock = new Mock<IProductRepository>();
                mock.Setup(m => m.Products).Returns(new Product[] {
               new Product {ProductID = 1, Name = "P1"},
               new Product {ProductID = 2, Name = "P2"}
           }.AsQueryable());
                // Arrange - create the controller
                ProductController target = new ProductController(mock.Object);
                // Act - call the GetImage action method
                ActionResult result = target.GetImage(100);
                // Assert
                Assert.IsNull(result);
            }

            [TestMethod]
            public void Generate_Category_Specific_Product_Count()
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
                ProductController target = new ProductController(mock.Object);
                target.PageSize = 3;
                // Action - test the product counts for different categories
                int res1 = ((ProductsListViewModel)target.ProductList("Cat1").Model).PagingInfo.TotalItems;
                int res2 = ((ProductsListViewModel)target.ProductList("Cat2").Model).PagingInfo.TotalItems;
                int res3 = ((ProductsListViewModel)target.ProductList("Cat3").Model).PagingInfo.TotalItems;
                int resAll = ((ProductsListViewModel)target.ProductList(null).Model).PagingInfo.TotalItems;
                // Assert
                Assert.AreEqual(res1, 2);
                Assert.AreEqual(res2, 2);
                Assert.AreEqual(res3, 1);
                Assert.AreEqual(resAll, 5);
            }

            public Category Cat3 { get; set; }

            public Category Cat2 { get; set; }

            public Category Cat1 { get; set; }
    }
}
