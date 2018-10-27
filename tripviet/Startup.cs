using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using TripViet.Commons;
using TripViet.Data;
using TripViet.Models;
using TripViet.Services;

namespace TripViet
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
        public IHostingEnvironment Env { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DependencyInjectionConfig.AddScope(services);
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Env.IsProduction() ? Configuration.GetConnectionString("ProdIdentityConnection") : Configuration.GetConnectionString("IdentityConnection")));

            services.AddDbContext<TripVietContext>(options =>
            options.UseSqlServer(Env.IsProduction() ? Configuration.GetConnectionString("ProdTripVietConnection") : Configuration.GetConnectionString("TripVietConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/Login";
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<ApplicationConfigurations>(Configuration.GetSection("ApplicationConfigurations"));

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var environment = services.GetRequiredService<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
                        var tripvietContext = services.GetRequiredService<TripVietContext>();
                        tripvietContext.Database.Migrate(); // apply all migrations
                        var identityContext = services.GetRequiredService<ApplicationDbContext>();
                        identityContext.Database.Migrate(); // apply all migrations
                        //SeedData.Initialize(services); // Insert default data
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred while seeding the database.");
                    }
                }
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
