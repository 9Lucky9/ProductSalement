using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSalement.Models
{
    /// <summary>
    /// Вложенная модель данных
    /// </summary>
    [Owned]
    public class ProvidedProduct
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public int ProductQuantity { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public ProvidedProduct(int productId, int productQuantity, Product product)
        {
            ProductId = productId;
            ProductQuantity = productQuantity;
            Product = product;
        }

        /// <summary>
        /// EF constructor
        /// </summary>
        public ProvidedProduct(int productId, int productQuantity)
        {
            ProductId = productId;
            ProductQuantity = productQuantity;
        }
    }
}
