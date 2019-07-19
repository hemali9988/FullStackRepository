using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SampleWebApp
{
    public class Program
    {

        static Dictionary<string, string> configSettings = new Dictionary<string, string>();

        public static void Main(string[] args)
        {
            configSettings.Add("SqlConnection", "Some valid connectionstring data");
            configSettings.Add("Username", "Sonu");

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory())
                //json,env,cmd (already configured in default webhost)(1,2,3)
                .AddInMemoryCollection(configSettings)//4
               
                .AddXmlFile("mysettings.xml");//5
               
            })
           .UseStartup<Startup>();

        //json,env,cmd //this are in built +memory+xml
        
    }
}
