using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertismentPlatform.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Identity;
using AdvertismentPlatform.Models.MySqlRepository;

namespace AdvertismentPlatform
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

      
        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContextPool<AppDbContext>(options =>
               options.UseMySql(Configuration.GetConnectionString("adplatform")));

            services.AddIdentity<ApplicationUser, IdentityRole>( options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 3;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();


            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "824631466201-rsmg37ukppkgvr2i5d9r4tgjljpsc5gi.apps.googleusercontent.com";
                    options.ClientSecret = "aaRirFRgJSIbJlkpYmWTYeix";
                })
                .AddFacebook(options =>
                {
                    options.AppId = "872704303179040";
                    options.AppSecret = "e09b7365a46ec7b5f6c2fb4f1dc91b77";
                });

            services.AddScoped<IItemRepository<ItemCategory>, BaseItemRepository>();
            services.AddScoped<IAutoItemRepository, AutoRepository>();
            services.AddScoped<IBikeItemRepository, BikeRepository>();
            services.AddScoped<IAdvertismentRepository, AdvertismentRepository>();
        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
