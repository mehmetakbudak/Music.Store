using Microsoft.EntityFrameworkCore;
using Music.Store.Domain.Entities;

namespace Music.Store.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistTrack> PlaylistTracks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WebsiteParameter> WebsiteParameters { get; set; }
        public DbSet<MailTemplate> MailTemplates { get; set; }
        public DbSet<AccessRight> AccessRights { get; set; }
        public DbSet<AccessRightEndpoint> AccessRightEndpoints { get; set; }
        public DbSet<UserAccessRight> UserAccessRights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
