using System.ComponentModel.DataAnnotations;

namespace Music.Store.Domain.Entities
{
    public class Playlist : EntityBase
    {       
        [Required, MaxLength(120)]
        public string Name { get; set; }
    }
}
