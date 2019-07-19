using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EventManagementApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging(options=>
            {
                options.SetMinimumLevel(LogLevel.Warning);//Min warning should be logged
            })
            .ConfigureAppConfiguration(options=>
            {
                //appsettings.json
                //appsettings.{Environment}.json
                //Environment variables
                //Command line arguments
                options.SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("mysettings.xml", optional: true, reloadOnChange: true);
                //optional:file exists or not if not exists,should no display error
                //reloadOnChange:to reload on changes 
            })
            .ConfigureKestrel((context,options)=>
            {
                options.Limits.MaxRequestBodySize = 5000000;//to customize sizelimits on kestrel
            })
            .UseUrls("http://*.7000","https://*.7001")//use two port nos onlt
            .CaptureStartupErrors(true)//configure  errors while start up
                .UseStartup<Startup>();
    }
}
