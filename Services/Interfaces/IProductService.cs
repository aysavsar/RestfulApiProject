using RestfulApiProject.Models;
using System.Collections.Generic;

namespace RestfulApiProject.Services.Interfaces
{
    /// <summary>
    /// Ürün yönetimi için servis arayüzü.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Belirtilen filtreleme ve sıralama kriterlerine göre ürünleri getirir.
        /// </summary>
        List<Product> GetProducts(string name, string sortBy, string sortOrder, int page, int pageSize);

        /// <summary>
        /// Belirtilen ID'ye sahip ürünü getirir.
        /// </summary>
        Product GetProductById(int id);

        /// <summary>
        /// Yeni bir ürün oluşturur.
        /// </summary>
        Product CreateProduct(Product product);

        /// <summary>
        /// Belirtilen ID'ye sahip ürünü günceller.
        /// </summary>
        void UpdateProduct(int id, Product product);

        /// <summary>
        /// Belirtilen ID'ye sahip ürünü siler.
        /// </summary>
        void DeleteProduct(int id);

        /// <summary>
        /// Belirtilen ID'ye sahip ürünün belirli alanlarını günceller (Patch işlemi).
        /// </summary>
        void PartialUpdateProduct(int id, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<Product> patchDoc);
    }
}
