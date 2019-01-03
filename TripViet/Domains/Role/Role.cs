using Microsoft.AspNetCore.Identity;
using System;

namespace TripViet.Domains
{
    // Add profile data for application users by adding properties to the User class
    public class Role : IdentityRole<Guid>
    {
        public Role() : base() { }
        public Role(string roleName) : base(roleName) { }
    }
}
