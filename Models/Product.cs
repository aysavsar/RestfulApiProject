using System.ComponentModel.DataAnnotations;

namespace RestfulApiProject.Models
{
    public class Product
    {
        // Ürün ID'si (zorunlu)
        public int Id { get; set; }

        // Ürün adı (zorunlu, maksimum 100 karakter)
        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Ürün adı 100 karakteri geçemez.")]
        public string Name { get; set; }

        // Ürün fiyatı (zorunlu)
        [Required(ErrorMessage = "Ürün fiyatı zorunludur.")]
        public decimal Price { get; set; }

        // Ürün açıklaması (maksimum 500 karakter)
        [StringLength(500, ErrorMessage = "Açıklama 500 karakteri geçemez.!")]
        public string Description { get; set; }
    }
}