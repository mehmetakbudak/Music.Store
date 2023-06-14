using System.ComponentModel.DataAnnotations;

namespace Music.Store.Domain.Entities
{
    public class PlaylistTrack : EntityBase
    {
        public int PlaylistId { get; set; }

        public int TrackId { get; set; }

        public Playlist Playlist { get; set; }

        public Track Track { get; set; }
    }
}
