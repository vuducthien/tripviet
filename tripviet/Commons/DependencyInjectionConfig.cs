using Microsoft.Extensions.DependencyInjection;

namespace TripViet.Commons
{
    public class DependencyInjectionConfig
    {
        public static void AddScope(IServiceCollection services)
        { 
            services.AddScoped<ITripVietContext, TripVietContext>();
        }
    }
}
