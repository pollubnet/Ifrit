using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ifrit.Web.Data;

namespace Ifrit.Web.Controllers
{
    public class HomeController : Controller
    {
        streamresultContext context;

        public HomeController(streamresultContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(context.iotdata.OrderByDescending(x => x.Time).ToList());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
