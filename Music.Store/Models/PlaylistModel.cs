using System.Collections.Generic;
using Music.Store.Data.Entity;

namespace Music.Store.Models
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
