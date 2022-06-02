using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSalement.Models
{
    /// <summary>
    /// Покупка
    /// </summary>
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public int SalesPointId { get; set; }
        public int? BuyerId { get; set; }
        public ICollection<SalesData> SalesData { get; set; }
        public int TotalAmount { get; set; }

        public Sale(DateTime date, string time, int salesPointId, int? buyerId, ICollection<SalesData> salesData, int totalAmount)
        {
            Date = date;
            Time = time;
            SalesPointId = salesPointId;
            BuyerId = buyerId;
            SalesData = salesData;
            TotalAmount = totalAmount;
        }

        /// <summary>
        /// EF constructor
        /// </summary>
        private Sale(int id, DateTime date, string time, int salesPointId, int? buyerId, int totalAmount)
        {
            Id = id;
            Date = date;
            Time = time;
            SalesPointId = salesPointId;
            BuyerId = buyerId;
            TotalAmount = totalAmount;
        }

    }
}
