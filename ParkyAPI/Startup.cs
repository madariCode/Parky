using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ParkyAPI.Datos;
using ParkyAPI.ParkyMapper;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ParkyAPI
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
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IParqueNacionalRepository, ParqueNacionalRepository>();
            services.AddScoped<ISenderoRepository, SenderoRepository>();
            services.AddAutoMapper(typeof(ParkyMappings));
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("ParkyOpenAPISpecParquesNacionales",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Parques nacionales",
                        Version = "1",
                        Description = "Especificación API del curso de Bhrugen Patel https://www.dotnetmastery.com/Home/Details?courseId=7",
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                        }
                    });
                options.SwaggerDoc("ParkyOpenAPISpecSenderos",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Senderos",
                        Version = "1",
                        Description = "Especificación API del curso de Bhrugen Patel https://www.dotnetmastery.com/Home/Details?courseId=7",
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                        }
                    });
                var xmlComentariosFichero = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlComentariosRuta = Path.Combine(AppContext.BaseDirectory, xmlComentariosFichero);
                options.IncludeXmlComments(xmlComentariosRuta);

            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/ParkyOpenAPISpecParquesNacionales/swagger.json", "Parky API Parques Nacionales");
                options.SwaggerEndpoint("/swagger/ParkyOpenAPISpecSenderos/swagger.json", "Parky API Senderos");
                options.RoutePrefix = "";
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
