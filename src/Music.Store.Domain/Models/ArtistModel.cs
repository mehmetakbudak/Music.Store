using Microsoft.AspNetCore.Http;

namespace Music.Store.Domain.Models
{
    public class ArtistFilterModel : FilterModel
    {
        public string Text { get; set; }
        public bool? IsPopular { get; set; }
    }

    public class ArtistModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public bool IsPopular { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
    }
}
