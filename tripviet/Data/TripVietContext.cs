using Microsoft.EntityFrameworkCore;
using TripViet.Domains;

namespace TripViet.Data
{
    public class TripVietContext : DbContext, ITripVietContext
    {
        public TripVietContext(DbContextOptions<TripVietContext> options)
            : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Place> Places { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
