using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Music.Store.Domain.Entities
{
    public class InvoiceLine : EntityBase
    {      
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int TrackId { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("TrackId")]
        public Track Track { get; set; }
    }
}
