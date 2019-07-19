using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace SampleWebApp
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}
        static Dictionary<string, string> configSettings = new Dictionary<string, string>();

        public Startup(IConfiguration configuration)
        {
            configSettings.Add("SqlConnection", "Some valid connectionstring data");
            configSettings.Add("Username", "Sonu");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddInMemoryCollection(configSettings)
                .AddXmlFile("mysettings.xml")
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //this all read only particular key value
            var username = Configuration.GetValue<string>("Username");
            var connString = Configuration.GetValue<string>("SqlConnection");
            var company= Configuration.GetValue<string>("Company");
            var name = Configuration.GetValue<string>("UserInfo:Name");
            var version = Configuration.GetValue<string>("AppData:Version");

            //to read whole configuration
            services.Configure<AppSettings>(Configuration);

            //to read group of values
            services.Configure<UserInfo>(Configuration.GetSection("UserInfo"));//mapping section to class and made it available DI

            //to add mvc services
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");


            app.UseDefaultFiles(options);

            app.UseStaticFiles(); //default static files folder wwwroot

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    RequestPath = "/files",
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Data"))
            //});

            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            //{
            //    RequestPath = "/files",
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Data"))

            //});

            //DefaultFilesOptions options = new DefaultFilesOptions();
            //options.DefaultFileNames.Clear();
            //options.DefaultFileNames.Add("index.html");
            //options.RequestPath = "/files";
            //options.FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Data"));
            //options.enable

            app.UseFileServer(new FileServerOptions()
            {
                RequestPath = "/files",
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "files")),
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = true
            });

           // app.UseDefaultFiles(options);

            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
