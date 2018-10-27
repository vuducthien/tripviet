using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripViet.Domains
{
    public class Place: BaseEntity
    {
        public Guid BlogId { get; set; }

        public string Name { get; set; }
        
        public string NonHtmlAddress { get; set; }
        
        public string HtmlAddress { get; set; }
        
        public string Reference { get; set; }
        
        public string ApiId { get; set; }
    }
}
