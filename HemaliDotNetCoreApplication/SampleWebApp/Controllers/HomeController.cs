using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SampleWebApp.Models;

namespace SampleWebApp.Controllers
{
    public class HomeController : Controller
    {

        //To get UserInfo configuration section
        private UserInfo userInfo;
        public HomeController(IOptions<UserInfo> options)
        {
            this.userInfo = options.Value;
        }


        //To get all settings from one configuration
        private AppSettings appSettings;
        public HomeController(IOptions<AppSettings> options)
        {
            this.appSettings = options.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
