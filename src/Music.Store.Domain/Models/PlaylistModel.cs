using Music.Store.Domain.Entities;
using System.Collections.Generic;

namespace Music.Store.Domain.Models
{
    public class PlaylistFilterModel : FilterModel
    {
        public string Name { get; set; }
    }

    public class PlaylistTrackGetModel
    {
        public string Name { get; set; }
        public List<Track> Tracks { get; set; }
    }

    public class PlaylistTrackFilterModel : FilterModel
    {
        public int PlaylistId { get; set; }
    }
}
