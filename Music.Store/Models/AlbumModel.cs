using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Music.Store.Models
{
    public class AlbumFilterModel : FilterModel
    {
        public string Title { get; set; }
        public List<int> ArtistIds { get; set; }
    }



    public class AlbumModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
    }
}
