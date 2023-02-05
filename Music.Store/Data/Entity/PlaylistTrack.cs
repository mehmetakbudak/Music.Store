using System.ComponentModel.DataAnnotations;

namespace Music.Store.Data.Entity
{
    public class PlaylistTrack
    {
        [Key]
        public int PlaylistId { get; set; }

        [Key]
        public int TrackId { get; set; }

        public Playlist Playlist { get; set; }

        public Track Track { get; set; }
    }
}
