using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripViet.Models.BlogViewModels
{
    public class PlaceViewModel
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string NonHtmlAddress { get; set; }
        
        public string HtmlAddress { get; set; }
        
        public string Reference { get; set; }
        
        public string GoogleApiId { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
