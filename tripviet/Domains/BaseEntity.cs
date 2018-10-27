using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripViet.Domains
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid UpdatedById { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
