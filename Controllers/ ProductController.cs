using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RestfulApiProject.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;

namespace RestfulApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Apple", Price = 1.20M, Description = "Fresh apple" },
            new Product { Id = 2, Name = "Banana", Price = 0.50M, Description = "Ripe banana" }
        };

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts([FromQuery] string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var filteredProducts = products.Where(p => p.Name.Contains(name)).ToList();
                return Ok(filteredProducts);
            }
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(new { message = "Product is null" });
            }

            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;

            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            products.Remove(product);

            return NoContent();
        }

        // PATCH: api/products/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialUpdateProduct(int id, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            patchDoc.ApplyTo(product, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
