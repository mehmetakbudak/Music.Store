using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Music.Store.Data.Entity
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(160)]
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public Artist Artist { get; set; }

        public virtual List<Track> Tracks { get; set; }
    }
}