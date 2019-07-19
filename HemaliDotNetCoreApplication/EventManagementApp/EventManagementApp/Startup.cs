using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagementApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

             services.AddDistributedMemoryCache();// in memory distributed caching

            //services.AddDistributedSqlServerCache(options=>
            //{
            //    options.ConnectionString = Configuration.GetConnectionString("SqlConnection");
            //    options.SchemaName = "dbo";
            //    options.TableName = "CacheTable";
            //} );

            //RedisCache
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = Configuration.GetSection("Redis")["Configuration"];
            //    options.InstanceName = Configuration.GetSection("Redis")["InstanceName"];
            //});

            services.AddSession(config=>
            {
                config.Cookie.Name = ".MYSESSIONCOOKIE";
                config.Cookie.MaxAge = TimeSpan.FromMinutes(15);
                config.IdleTimeout = TimeSpan.FromMinutes(30);
                config.Cookie.IsEssential = true;
            }
            );

           // services.AddMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();//Show an error page with full stack trace info about error
            }
            else
            {
                //   app.UseExceptionHandler("/Home/Error");//production mode

                //to customize error
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async (context) =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.Headers["Content-Type"] = "text/html";
                        await context.Response.WriteAsync("<h2>Some error occured.</h2>");
                        var exceptionPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                        if (exceptionPathFeature?.Error is NullReferenceException)
                        {
                            await context.Response.WriteAsync("<p>Null reference exception</p>");
                        }
                        else if (exceptionPathFeature?.Error is InvalidCastException)
                        {
                            await context.Response.WriteAsync("<p>Invalid cast exception</p>");
                        }
                    });
                });
             
            }

            //app.UseStatusCodePages();
            // app.UseStatusCodePages("text/html", "<h2>Status code error:{0}.</h2><p>client error</p>");

            //app.UseStatusCodePagesWithRedirects("Home/StatusCodes/{0}");redirects the page with StatusCodes page

            app.UseStatusCodePagesWithReExecute("Home/StatusCodes/{0}");


            app.UseStaticFiles();
            app.UseCookiePolicy();// to work session properly otherwise it will throw error
            app.UseSession();
           // app.UseMvcWithDefaultRoute();

            

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name:"ProductRoute",
                //    template:"{app/{}"
                //    );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
