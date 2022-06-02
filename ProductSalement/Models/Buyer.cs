using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSalement.Models
{
    /// <summary>
    /// Покупатель
    /// </summary>
    public class Buyer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Sale> Sales { get; set; }

        public Buyer(string name)
        {
            Name = name;
        }

        /// <summary>
        /// EF constructor
        /// </summary>
        private Buyer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
