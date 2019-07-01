using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MemeGenerator.Models;
using Microsoft.AspNetCore.Authorization;

namespace MemeGenerator.Controllers
{
    public class HomeController : Controller
    {
        memyContext db = new memyContext();
        // GET: /<controller>/
        public IActionResult Index()
        {
            var model = db.Memy.ToList();
            return View(model);
        
        }

        

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            var model = db.Memy.ToList();
            return View(model);
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
