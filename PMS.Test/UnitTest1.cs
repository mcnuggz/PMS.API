using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using PWS.API.Controllers;
using PWS.API.Data;
using PWS.API.Models;
using Xunit;


namespace PMS.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetProducts_Returns_Products()
        {
            //arrange
            var products = new List<Product>{
            new Product{ ProductId = Guid.NewGuid(), Name= "Cottonelle Fresh Feel Flushable Wet Wipes, Adult Wet Wipes", Category="Health & Household", Color="Blue", Price=15.79M},
            new Product{ ProductId = Guid.NewGuid(), Name= "e.l.f. Monochromatic Multi Stick, Luxuriously Creamy & Blendable Color, For Eyes, Lips & Cheeks", Category="Beauty & Personal Care", Color="Dazzling Peony", Price=5.00M},
            new Product{ ProductId = Guid.NewGuid(), Name= "Meat Thermometer Digital for Grilling and Cooking", Category="Home & Kitchen", Color="Red/Black", Price=7.98M},
        };

            var mockSet = new Mock<DbSet<PMSDbContext>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.AsQueryable().Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.AsQueryable().Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.AsQueryable().ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.AsQueryable().GetEnumerator());

            var mockContext = new Mock<PMSDbContext>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.AsQueryable().Provider);
            var controller = new ProductsController(mockContext.Object);

            // act
            var result = await controller.GetProducts();

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.Equal(3, returnedProducts.Count());


        }
    }
}