using IngatlanWebAPI.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngatlanWebAPI
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
            services.AddDbContext<IngatlanContext>();

            services.AddSwaggerGen();
			services.AddCors(option =>
            {
                option.AddPolicy("EnableCORS", builder =>
                {
                    builder.SetIsOriginAllowed(origin => true)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials()
                           .Build();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
			app.UseCors("EnableCORS");

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "docs/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    var oldPaths = swagger.Paths.ToDictionary(entry => entry.Key, entry => entry.Value);
                    foreach (var path in oldPaths)
                    {
                        swagger.Paths.Remove(path.Key);
                        swagger.Paths.Add(path.Key.Replace("api", "ingatlanwebapi/api"), path.Value);
                    }
                });
            });
            app.UseSwaggerUI( c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Ingatlan Web API");
                c.RoutePrefix = "docs";
            });

            app.UseRouting();
            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<IngatlanContext>())
                context.Database.EnsureCreated();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
