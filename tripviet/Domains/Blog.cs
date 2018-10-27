using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TripViet.Commons;

namespace TripViet.Domains
{
    public class Blog : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        
        public BlogType BlogType { get; set; }

        public virtual ICollection<Place> Places { get; set; }
    }
}
