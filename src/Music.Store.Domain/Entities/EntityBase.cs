using System.ComponentModel.DataAnnotations;

namespace Music.Store.Domain.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
