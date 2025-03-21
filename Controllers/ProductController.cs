using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using RestfulApiProject.Models;
using RestfulApiProject.Services.Interfaces;
using System.Collections.Generic;

namespace RestfulApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts(
            [FromQuery] string name = null,
            [FromQuery] string sortBy = "id",
            [FromQuery] string sortOrder = "asc",
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var products = _productService.GetProducts(name, sortBy, sortOrder, page, pageSize);
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound(new { message = "Ürün bulunamadı." });
            }

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(new { message = "Ürün bilgisi boş olamaz." });
            }

            var createdProduct = _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = _productService.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound(new { message = "Ürün bulunamadı." });
            }

            _productService.UpdateProduct(id, product);
            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound(new { message = "Ürün bulunamadı." });
            }

            _productService.DeleteProduct(id);
            return NoContent();
        }

        // PATCH: api/products/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialUpdateProduct(int id, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound(new { message = "Ürün bulunamadı." });
            }

            _productService.PartialUpdateProduct(id, patchDoc);
            return NoContent();
        }
    }
}