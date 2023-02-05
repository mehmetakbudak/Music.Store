using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Music.Store.Data.Entity
{
    public class Track
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        public int AlbumId { get; set; }

        public int MediaTypeId { get; set; }

        public int GenreId { get; set; }

        [MaxLength(220)]
        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public long Bytes { get; set; }

        public string Lyrics { get; set; }

        public string WebUrl { get; set; }

        public string FileUrl { get; set; }

        public decimal? UnitPrice { get; set; }

        [ForeignKey("AlbumId")]
        public Album Album { get; set; }

        [ForeignKey("MediaTypeId")]
        public MediaType MediaType { get; set; }

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
    }
}
