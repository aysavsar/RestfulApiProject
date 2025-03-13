using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using RestfulApiProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestfulApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Örnek ürün listesi (statik olarak tanımlandı)
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Elma", Price = 1.20M, Description = "Taze elma" },
            new Product { Id = 2, Name = "Muz", Price = 0.50M, Description = "Olgun muz" }
        };

        // GET: api/products
        // Tüm ürünleri listele veya isme göre filtrele
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts(
            [FromQuery] string name = null, // İsme göre filtreleme
            [FromQuery] string sortBy = "id", // Sıralama kriteri (varsayılan: id)
            [FromQuery] string sortOrder = "asc", // Sıralama yönü (varsayılan: artan)
            [FromQuery] int page = 1, // Sayfa numarası (varsayılan: 1)
            [FromQuery] int pageSize = 10) // Sayfa başına ürün sayısı (varsayılan: 10)
        {
            var query = products.AsQueryable();

            // İsme göre filtreleme
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            // Sıralama
            if (sortBy.ToLower() == "price")
            {
                query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
            }
            else
            {
                query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id);
            }

            // Sayfalama
            var totalCount = query.Count();
            var totalPages = (int)System.Math.Ceiling(totalCount / (double)pageSize);

            var result = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Sonuçları döndür
            return Ok(new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize,
                Products = result
            });
        }

        // GET: api/products/{id}
        // Belirli bir ürünü ID'ye göre getir
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { message = "Ürün bulunamadı." });
            }

            return Ok(product);
        }

        // POST: api/products
        // Yeni bir ürün ekle
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(new { message = "Ürün bilgisi boş olamaz." });
            }

            // Yeni ürün ID'si belirle
            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);

            // 201 Created durum kodu ile yanıt ver
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/products/{id}
        // Var olan bir ürünü güncelle
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return NotFound(new { message = "Ürün bulunamadı." });
            }

            // Ürün bilgilerini güncelle
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;

            return NoContent();
        }

        // DELETE: api/products/{id}
        // Belirli bir ürünü sil
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { message = "Ürün bulunamadı." });
            }

            // Ürünü listeden sil
            products.Remove(product);

            return NoContent();
        }

        // PATCH: api/products/{id}
        // Belirli bir ürünü kısmen güncelle
        [HttpPatch("{id}")]
        public ActionResult PartialUpdateProduct(int id, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { message = "Ürün bulunamadı." });
            }

            // Patch dokümanını uygula
            patchDoc.ApplyTo(product, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}