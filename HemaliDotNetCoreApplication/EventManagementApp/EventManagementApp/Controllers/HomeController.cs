using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventManagementApp.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace EventManagementApp.Controllers
{
    public class HomeController : Controller
    {

        private IDistributedCache cache;
        private IMemoryCache memoryCache;

        public HomeController(IDistributedCache distributedCache, IMemoryCache memoryCache)
        {
            this.cache = distributedCache;
            this.memoryCache = memoryCache;
        }
        //[ResponseCache(Duration =120,Location =ResponseCacheLocation.Client)]
        public IActionResult Index()
        {
            ViewBag.Date = DateTime.Now;

            //In memory non distributed cache
            memoryCache.Set<string>("data", "This is from memory cache");
            string data = memoryCache.Get<string>("data");

            //Distributed cache
            if(cache.GetString("message") !=null)
            {
                ViewBag.message = cache.GetString("message");
            }
            else
            {
                cache.SetString("message", "Hello now time is " + DateTime.Now.ToString());
                ViewBag.message = cache.GetString("message");

            }



            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult StatusCodes(int id)
        {
            ViewBag.StatusCode = id;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
