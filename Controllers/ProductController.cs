using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductHandle.Models;

namespace ProductHandle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly WebApiContext _webApiContext;
        public ProductController(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _webApiContext.Products.ToListAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _webApiContext.Products.FindAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.ProductId) return BadRequest();
            _webApiContext.Entry(product).State = EntityState.Modified;
            await _webApiContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Created(Product product)
        {
            await _webApiContext.Products.AddAsync(product);
            await _webApiContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductById), new {id=product.ProductId},product);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var productToDelete = await _webApiContext.Products.FindAsync(id);
            if(productToDelete == null) return NotFound();
            _webApiContext.Products.Remove(productToDelete);
            await _webApiContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
