using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripViet.Commons;

namespace TripViet.Models.BlogViewModels
{
    public class BlogViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vui lòng viết bài")]
        public string Content { get; set; }

        public string Author { get; set; }

        public BlogType BlogType { get; set; }

        public string Time { get; set; }

        public Guid CreatedById { get; set; }

        public List<PlaceViewModel> Places { get; set; }
    }
}
