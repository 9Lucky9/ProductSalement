using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSalement.Models
{
    /// <summary>
    /// Точка продажи
    /// </summary>
    public class SalesPoint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<ProvidedProduct> ProvidedProducts { get; set; }

        public SalesPoint(string name, ICollection<ProvidedProduct> providedProducts)
        {
            Name = name;
            ProvidedProducts = providedProducts;
        }

        /// <summary>
        /// EF constructor
        /// </summary>
        public SalesPoint(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
