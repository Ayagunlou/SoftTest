using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace SoftTest.Models
{
    public class PreOrderModel
    {
        [Key]
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Vender { get; set; }
        public int? Price { get; set; }
        public DateTime DeliveryDate { get; set; }
        public char? Terminate { get; set; }
        public int? quantity { get; set;}
        public long? Total { get; set; }

    }
}
