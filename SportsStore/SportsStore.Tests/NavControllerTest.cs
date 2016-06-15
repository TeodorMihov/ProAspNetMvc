using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entites;
using SportsStore.UI.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Tests
{
    [TestClass]
    public class NavControllerTest
    {
        [TestMethod]
        public void Can_Create_Categories()
        {
            // Arrange
            // - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] 
            {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                new Product {ProductID = 2, Name = "P2", Category = "Apples"},
                new Product {ProductID = 3, Name = "P3", Category = "Plums"},
                new Product {ProductID = 4, Name = "P4", Category = "Oranges"},
            });

            // Arrange - create the controller
            NavController target = new NavController(mock.Object);
            
            // Act = get the set of categories
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();
            
            // Assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Apples");
            Assert.AreEqual(results[1], "Oranges");
            Assert.AreEqual(results[2], "Plums");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Arrange
            // - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1", Category = "Apples"},
            new Product {ProductID = 4, Name = "P2", Category = "Oranges"},
            });
            
            // Arrange - create the controller
            NavController target = new NavController(mock.Object);
            
            // Arrange - define the category to selected
            string categoryToSelect = "Apples";
            
            // Action
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;
            
            // Assert
            Assert.AreEqual(categoryToSelect, result);
        }

    }
}
