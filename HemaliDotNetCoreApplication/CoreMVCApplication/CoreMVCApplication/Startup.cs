using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexinea.Autofac.Extensions.DependencyInjection;
using Autofac;
using CoreMVCApplication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoreMVCApplication
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

            services.AddSingleton<IDataManager, EntityDataManager>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<DataService, DataService>();

            services.AddTransient<IDataManager, DataManager>();
            services.AddMvc()
                .AddCookieTempDataProvider()
                .AddSessionStateTempDataProvider()
                
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        //public IServiceProvider ConfigureServices(IServiceCollection services)
        //{

        //    services.Configure<CookiePolicyOptions>(options =>
        //    {
        //        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        //        options.CheckConsentNeeded = context => true;
        //        options.MinimumSameSitePolicy = SameSiteMode.None;
        //    });
        //    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        //    var containerBuilder = new ContainerBuilder();
        //    containerBuilder.RegisterModule<AutofacModule>();
        //    containerBuilder.Populate(services);
        //    var container = containerBuilder.Build();
        //    return new AutofacServiceProvider(container);


        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.Use(async (context, next) =>
            {
                if (context.Request.Headers.ContainsKey("Subscription-Key"))
                {
                    context.Items["KeyExists"] = true;
                }
                else
                {
                    context.Items["KeyExists"] = false;

                }
                await next.Invoke();
            });

            logger.LogInformation("Configure executing with set of services.");
            logger.LogWarning("Be carefully");

            app.UseStaticFiles();
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
