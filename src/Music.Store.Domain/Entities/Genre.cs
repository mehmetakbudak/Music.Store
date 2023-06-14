using System.ComponentModel.DataAnnotations;

namespace Music.Store.Domain.Entities
{
    public class Genre : EntityBase
    {
        [MaxLength(120)]
        public string Name { get; set; }
    }
}
