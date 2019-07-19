using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMVCApplication.Models;
using CoreMVCApplication.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoreMVCApplication.Controllers
{
    public class HomeController : Controller
    {
        private IDataManager dm;
        private DataService dservice;
        public HomeController(IDataManager manager,IConfiguration config,ILogger<HomeController> logger,DataService ds)
        {
            this.dm = manager;
            ViewBag.message = dm.GetMessage();
            this.dservice = ds;

            logger.LogInformation("Home Controller Logging");
            
        }
        public IActionResult Index([FromServices]IDataManager dm)
        {
            //if (HttpContext.Items["KeyExists"].ToString() != "False")
            //{
            //    var keyExists = (bool)HttpContext.Items["KeyExists"];

            //    if (keyExists)
            //    {

            //    }

            //    ViewBag.message = dm.GetMessage();

            //    return View();
            //}
            //return RedirectToAction("Error", "Home");
            ViewBag.message = dservice.SetMessage("This is set from Home");
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.Message = dservice.GetMessage(); //"This is set By Home";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
