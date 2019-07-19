using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EventManagementApp.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
               ViewBag.username= HttpContext.Session.GetString("username");
            }
            else
            {
                HttpContext.Session.SetString("username", "SonuSathyadas");
                ViewBag.username = HttpContext.Session.GetString("username");

            }
            return View();
        }

        public IActionResult List()
        {
            ViewBag.username = HttpContext.Session.GetString("username");
            return View();

        }
    }
}