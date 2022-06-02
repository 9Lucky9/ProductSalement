using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductSalement.Models
{
    /// <summary>
    /// Купленные продукты
    /// </summary>
    [Owned]
    public class SalesData
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public int ProductQuantity { get; set; }
        public int ProductAmount { get; set; }
        [ForeignKey("ProductId")]
        [JsonIgnore]
        public Product Product { get; set; }

        public SalesData(int productId, int productQuantity, int productAmount)
        {
            ProductId = productId;
            ProductQuantity = productQuantity;
            ProductAmount = productAmount;
        }
    }
}