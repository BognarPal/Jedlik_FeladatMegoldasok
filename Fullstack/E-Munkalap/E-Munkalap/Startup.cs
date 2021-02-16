using E_Munkalap.SQL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace E_Munkalap
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
            services.AddLogging(builder => builder.AddConsole());
            if (string.IsNullOrWhiteSpace(Configuration["DBHOST"]))
            {
                services.Configure<DatabaseProvider>(Configuration.GetSection("ConnectionStrings"));
                DatabaseProvider.SetDbContext(services, Configuration.GetConnectionString("Munkalap"));
                System.Console.WriteLine("Connection string = " + Configuration.GetConnectionString("Munkalap"));
            }
            else
            {
                var host = Configuration["DBHOST"];
                var port = Configuration["DBPORT"] ?? "3306";
                var user = Configuration["DBUSER"] ?? "root";
                var pwd = Configuration["DBPASSWORD"] ?? "secret";
                var dbname = Configuration["DBNAME"] ?? "Munkalap";
                var constr = $"Server={host}; Database={dbname}; Uid={user}; pwd={pwd}; port={port};";
                services.Configure<DatabaseProvider>(options =>
                {
                    options.Munkalap = constr;
                });
                DatabaseProvider.SetDbContext(services, constr);
                System.Console.WriteLine("Connection string = " + constr);
            }
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews();

            // In production, the Angular files will be served from this directory
#if !DEBUG
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
#endif

#if DEBUG
            //CORS engedélyezése
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build();
                });
            });
#endif
            //authentication middleware
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "SessionBasedAuthentication";
            }).AddScheme<AuthenticationSchemeOptions, AuthHandler>("SessionBasedAuthentication", options =>
            {
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
            });

            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DatabaseProvider.MigrateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

#if DEBUG
            app.UseCors("EnableCORS");
            //app.UseHttpsRedirection();
#endif
            app.UseStaticFiles();
#if !DEBUG
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
#endif

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

#if DEBUG
            return;
#else
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
#endif
        }
    }
}
