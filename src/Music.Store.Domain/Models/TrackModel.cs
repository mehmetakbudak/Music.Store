using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Music.Store.Domain.Models
{
    public class TrackFilterModel : FilterModel
    {
        public string Name { get; set; }
        public string Composer { get; set; }
        public List<int> AlbumIds { get; set; }
        public List<int> ArtistIds { get; set; }
        public List<int> GenreIds { get; set; }
        public List<int> MediaTypeIds { get; set; }
    }

    public class TrackModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Composer { get; set; }
        public int AlbumId { get; set; }
        public int MediaTypeId { get; set; }
        public int GenreId { get; set; }
        public string Lyrics { get; set; }
        public string WebUrl { get; set; }
        public string FileUrl { get; set; }
        public IFormFile File { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
