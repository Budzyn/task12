using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.HtmlHelper;
using SportsStore.WebUI.Models;
using System.Web;

using Moq;


namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IProductsRepository>mock=new Mock<IProductsRepository>();
            mock.Setup(m => m.Products)
                .Returns(
                    new Product[]
                    {
                        new Product {ProductId = 1, Name = "P1"}, new Product {ProductId = 2, Name = "P2"},
                        new Product {ProductId = 3, Name = "P3",}, new Product {ProductId = 4, Name = "P4"},
                        new Product {ProductId = 5, Name = "P5"}
                    }.AsQueryable());
            ProductController controller=new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            IEnumerable<Product>result=(IEnumerable<Product>)controller.List(2).Model;

            //Assert
            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length==2);
            Assert.AreEqual(prodArray[0].Name,"P4");
            Assert.AreEqual(prodArray[1].Name,"P5");

        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            SportsStore.WebUI.HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);
            Assert.AreEqual(result.ToString(),@"<a href=""Page1"">1</a>"+@"<a class=""selected"" href=""Page2"">2</a>"+@"<a href=""Page3"">3</a>");
        }

        // This test fail for example, replace result or delete this test to see all tests pass
        [Test]
        public void TestFault()
        {
            Assert.IsTrue(false);
        }
    }
}
