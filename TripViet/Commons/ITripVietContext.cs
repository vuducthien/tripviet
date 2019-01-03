using Microsoft.EntityFrameworkCore;
using TripViet.Domains;

namespace TripViet.Commons
{
    public interface ITripVietContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Blog> Blogs { get; set; }
        DbSet<Place> Places { get; set; }
        int SaveChanges();
    }
}
