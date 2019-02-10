using Microsoft.AspNetCore.Mvc;
using Orbital7.RapidApp2.SampleWebApp.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp2.SampleWebApp.Controllers
{
    public class PagesController : Controller
    {
        public IActionResult Index()
        {
            return View(new IndexModel());
        }

        public IActionResult _PanelOverview()
        {
            return PartialView(new _PanelOverviewModel());
        }

        public IActionResult _PanelScrollNone()
        {
            return PartialView(new _PanelScrollNoneModel());
        }

        public IActionResult _PanelScrollX()
        {
            return PartialView(new _PanelScrollXModel());
        }

        public IActionResult _PanelScrollY()
        {
            return PartialView(new _PanelScrollYModel());
        }

        public IActionResult _PanelScrollBoth()
        {
            return PartialView(new _PanelScrollBothModel());
        }

        public IActionResult _PanelTabs()
        {
            return PartialView(new _PanelTabsModel());
        }
    }
}
