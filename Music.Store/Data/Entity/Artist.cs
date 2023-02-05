using System.ComponentModel.DataAnnotations;

namespace Music.Store.Data.Entity
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; }

        public string Bio { get; set; }


        [StringLength(500)]
        public string ImageUrl { get; set; }

        public bool? IsPopular { get; set; }

    }
}
