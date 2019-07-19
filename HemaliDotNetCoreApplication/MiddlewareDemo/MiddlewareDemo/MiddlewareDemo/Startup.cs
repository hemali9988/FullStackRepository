using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MiddlewareDemo.Middleware;

namespace MiddlewareDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                context.Response.Headers["content-type"] = "text/html";
               // await context.Response.WriteAsync("<br/>Process request");

                await next.Invoke();
                //await context.Response.WriteAsync("<br/>Process response");
            });


            //        app.Use(async (context, next) =>
            //        {
            //            await context.Response.WriteAsync("<br/>Second middleware Process request");

            //            await next.Invoke();
            //            await context.Response.WriteAsync("<br/>Second middleware Process response");
            //        });

            //    app.MapWhen((context)=>context.Quer)

            //    app.Map("/about",(context)=>
            //        {
            //        context.Use(async (appBuilder, next) =>
            //        {
            //            await appBuilder.Response.WriteAsync("<br/>Middleware for about req");
            //            await next.Invoke();
            //            await appBuilder.Response.WriteAsync("<br/>Middleware for about resp");


            //        });

            //        context.Run(async (appBuilder)
            //        async (context, next) =>
            //    {
            //        await context.Response.WriteAsync("<br/>Second middleware Process request");

            //        await next.Invoke();
            //        await context.Response.WriteAsync("<br/>Second middleware Process response");
            //    });


            // app.UseMiddleware<SecurityMiddleware>();

            app.UserSecurity();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("<br/>Hello World!");
            });
        }
    }
}
