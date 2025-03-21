using RestfulApiProject.Models;
using RestfulApiProject.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RestfulApiProject.Services
{
    /// <summary>
    /// Ürün yönetimi ile ilgili işlemleri gerçekleştiren servis sınıfı.
    /// </summary>
    public class ProductService : IProductService
    {
        // Örnek ürün verilerini içeren statik liste.
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Elma", Price = 1.20M, Description = "Taze elma" },
            new Product { Id = 2, Name = "Muz", Price = 0.50M, Description = "Olgun muz" }
        };

        /// <summary>
        /// Ürünleri filtreleme, sıralama ve sayfalama desteğiyle getirir.
        /// </summary>
        public List<Product> GetProducts(string name, string sortBy, string sortOrder, int page, int pageSize)
        {
            var query = products.AsQueryable();

            // İsim filtresi uygula
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            // Sıralama işlemi
            if (sortBy.ToLower() == "price")
            {
                query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
            }
            else
            {
                query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id);
            }

            // Sayfalama işlemi
            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// Belirtilen ID'ye sahip ürünü getirir.
        /// </summary>
        public Product GetProductById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Yeni bir ürün oluşturur ve listeye ekler.
        /// </summary>
        public Product CreateProduct(Product product)
        {
            product.Id = products.Max(p => p.Id) + 1; // Yeni ID oluştur
            products.Add(product);
            return product;
        }

        /// <summary>
        /// Mevcut bir ürünü günceller.
        /// </summary>
        public void UpdateProduct(int id, Product product)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
            }
        }

        /// <summary>
        /// Belirtilen ID'ye sahip ürünü siler.
        /// </summary>
        public void DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
            }
        }

        /// <summary>
        /// Ürünün belirli alanlarını günceller (PATCH işlemi).
        /// </summary>
        public void PartialUpdateProduct(int id, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<Product> patchDoc)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                patchDoc.ApplyTo(product);
            }
        }
    }
}
