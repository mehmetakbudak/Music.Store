using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Music.Store.Data.Entity
{
    public class Playlist
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; }
    }
}
