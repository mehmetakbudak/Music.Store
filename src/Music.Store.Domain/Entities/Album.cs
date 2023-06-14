using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Music.Store.Domain.Entities
{
    public class Album : EntityBase
    {
        [Required, MaxLength(160)]
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public Artist Artist { get; set; }

        public bool IsPopular { get; set; }

        public virtual List<Track> Tracks { get; set; }
    }
}