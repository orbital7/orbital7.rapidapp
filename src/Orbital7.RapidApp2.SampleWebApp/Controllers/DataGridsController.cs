using Microsoft.AspNetCore.Mvc;
using Orbital7.RapidApp2.SampleWebApp.Models.DataGrids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp2.SampleWebApp.Controllers
{
    public class DataGridsController : Controller
    {
        public IActionResult Index()
        {
            return View(new IndexModel());
        }
    }
}
