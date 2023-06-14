using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Music.Store.Domain.Entities
{
    public class Customer : EntityBase
    {      
        [Required, MaxLength(20)]
        public string FirstName { get; set; }

        [Required, MaxLength(20)]
        public string LastName { get; set; }

        [MaxLength(80)]
        public string Company { get; set; }

        [MaxLength(70)]
        public string Address { get; set; }

        [MaxLength(40)]
        public string City { get; set; }

        [MaxLength(40)]
        public string State { get; set; }

        [MaxLength(40)]
        public string Country { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        [MaxLength(24)]
        public string Phone { get; set; }

        [MaxLength(24)]
        public string Fax { get; set; }

        [MaxLength(60)]
        public string Email { get; set; }

        public int? SupportRepId { get; set; }

        [ForeignKey("SupportRepId")]
        public User SupportRep { get; set; }
    }
}
