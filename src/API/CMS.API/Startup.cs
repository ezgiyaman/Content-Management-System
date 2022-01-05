using CMS.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CMS.API
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
            services.AddControllers();

            services.AddDbContext<AppDbContext>(options =>
            {

                options.UseSqlServer(Configuration.GetConnectionString("ProjectContext"));
            });

            //Swagger Resolve 
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("CMSAPI", new OpenApiInfo()
                {
                    Title = "CMS API",
                    Version = "v1.1",
                    Description = "Restful API E",
                    Contact = new OpenApiContact()
                    {
                        Email = "ezgiiymn@gmail.com",
                        Name = "Ezgi Yaman",
                        Url = new Uri("https://github.com/ezgiyaman")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT Licanse",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });


                //summary için ekledim.
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //swagger kullandýðým için endpoint'ini ekledim.

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/CMSAPI/swagger.json", "Restful API");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
