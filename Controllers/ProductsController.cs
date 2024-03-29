using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWS.API.Data;
using PWS.API.Models;

namespace PWS.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly PMSDbContext _context;

        public ProductsController(PMSDbContext pmsDbContext)
        {
            this._context = pmsDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            product.ProductId = Guid.NewGuid();
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetProductByGuid(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if (product == null) { return NotFound(); }
            return Ok(product);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, Product updatedProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) { 
                return NotFound(); 
            } 
            product.Name = updatedProduct.Name; 
            product.Price = updatedProduct.Price;
            product.Category = updatedProduct.Category;
            product.Color = updatedProduct.Color;
            await _context.SaveChangesAsync();
            
            return Ok(product);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null) { return NotFound(); }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        
    }
}
