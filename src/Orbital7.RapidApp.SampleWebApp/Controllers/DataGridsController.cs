using Microsoft.AspNetCore.Mvc;
using Orbital7.RapidApp.SampleWebApp.Models.DataGrids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp.SampleWebApp.Controllers
{
    public class DataGridsController : Controller
    {
        public IActionResult Index()
        {
            return View(new IndexModel());
        }

        public IActionResult _PanelOverview()
        {
            return PartialView(new _PanelOverviewModel());
        }

        public IActionResult _PanelBasic()
        {
            return PartialView(_PanelBasicModel.Create());
        }
    }
}
