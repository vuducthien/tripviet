using Microsoft.AspNetCore.Identity;
using System;

namespace TripViet.Domains
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser<Guid>
    {
        public string Hobby { get; set; }
        public string Autobiograpy { get; set; }
        public decimal Rate { get; set; }
        public DateTime Birthday { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }
}
