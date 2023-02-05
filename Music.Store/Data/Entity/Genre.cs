using System.ComponentModel.DataAnnotations;

namespace Music.Store.Data.Entity
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(120)]
        public string Name { get; set; }
    }
}
