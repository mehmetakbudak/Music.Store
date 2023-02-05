using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Music.Store.Data.Entity
{
    public class MediaType
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(120)]
        public string Name { get; set; }
    }
}
