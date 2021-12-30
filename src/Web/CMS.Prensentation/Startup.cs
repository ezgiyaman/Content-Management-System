using CMS.Domain.Entities.Concrete;
using CMS.Infrastructure.Context;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Prensentation
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
            //Fluent validation için ;

            services.AddControllersWithViews().AddFluentValidation();

            //Automapper burdada çaðýrýlabilir bu projede IoC'de ;

            //services.AddAutoMapper(typeof(Mapping)); 


            services.AddSession(options =>
            {
                //Sepette kalan ürünlerin sepetten ne zaman kaldýrlacaðýný yada tüm sension ile ilgili ayarlamalar burada yapýlýr.

            });

            //Sql ile baðlantý 

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ProjectContext"));
            });

            //Identity Resolve ediliyor.

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            //Session

            app.UseSession();

            //LogIn iþlemi için kimlik doðrulama

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
               "page",
              "{slug}",
               defaults: new { controller = "Page", action = "Index" });

                //Category'sine göre product getirmek
                endpoints.MapControllerRoute(
                   "product",
                   "product/{categorySlug}",
                   defaults: new { controller = "Product", action = "ProductsByCategory" });

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
