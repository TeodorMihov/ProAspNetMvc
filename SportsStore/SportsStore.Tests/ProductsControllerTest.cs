namespace SportsStore.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using SportsStore.Domain.Abstract;
    using SportsStore.Domain.Entites;
    using SportsStore.UI.Controllers;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using UI.HtmlHelpers;
    using UI.Models;
    [TestClass]
    public class ProductsControllerTest
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            });

            ProductController pContr = new ProductController(mock.Object);
            pContr.PageSize = 3;

            //ACT
            var result = (ProductsListViewModel)pContr.List(null, 2).Model;

            // Assert
            Product[] products = result.Products.ToArray();
            Assert.IsTrue(products.Length == 2);
            Assert.AreEqual(products[0].Name, "P4");
            Assert.AreEqual(products[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Generates_Page_Links()
        {
            // Arrange - define an HTML helper - we need to do this
            // in order to apply the extension method
            HtmlHelper myHelper = null;

            // Arrange - create PagingInfo data
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Arrange - set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
            + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
            + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
            result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            });

            // Arrange
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            // Arrange
            // - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
            });

            // Arrange - create a controller and make the page size 3 items
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Action
            Product[] result = ((ProductsListViewModel)controller.List("Cat2", 1).Model)
            .Products.ToArray();

            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count() 
        {
            // Arrange
            // - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
                });

            // Arrange - create a controller and make the page size 3 items
            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            // Action - test the product counts for different categories
            int res1 = ((ProductsListViewModel)target
            .List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductsListViewModel)target
            .List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductsListViewModel)target
            .List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((ProductsListViewModel)target
            .List(null).Model).PagingInfo.TotalItems;

            // Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
