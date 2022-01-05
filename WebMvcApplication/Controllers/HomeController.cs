using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebMvcApplication.Models;
using WebMvcApplication.Models.Home;

namespace WebMvcApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.JavaScriptFunction = "Javascriptfunction()";
            ViewBag.JavaScriptFunction = string.Format("ShowGreetings('{0}');", "aashish");
            List<Users> usr = new List<Users>() {
                new Users() {id = "1", name = "aashish"},
                new Users() {id = "2", name = "ak"}
            };

            return View(usr);
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
