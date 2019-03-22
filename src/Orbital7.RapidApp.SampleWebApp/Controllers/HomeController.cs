using Microsoft.AspNetCore.Mvc;
using Orbital7.RapidApp.SampleWebApp.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp.SampleWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new IndexModel());
        }
    }
}
