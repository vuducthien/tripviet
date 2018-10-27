using AutoMapper;
using System.Web;
using TripViet.Domains;
using TripViet.Models.BlogViewModels;

namespace TripViet.Commons
{

    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration() : this("TripVietProfile")
        {
        }

        protected AutoMapperProfileConfiguration(string profileName) : base(profileName)
        {
            CreateMap<Blog, BlogViewModel>()
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.UpdatedDate.ToString("dd/MM/yyyy")));
            CreateMap<BlogViewModel, Blog>().ForMember(dest => dest.Content, opt => opt.MapFrom(src => HttpUtility.HtmlEncode(src.Content)));
            CreateMap<Place, PlaceViewModel>().ReverseMap();
        }
    }
}
