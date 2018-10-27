using Microsoft.EntityFrameworkCore;
using TripViet.Domains;

namespace TripViet.Data
{
    public interface ITripVietContext
    {
        DbSet<Blog> Blogs { get; set; }
        DbSet<Place> Places { get; set; }
        int SaveChanges();
    }
}
