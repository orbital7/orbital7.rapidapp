using Microsoft.AspNetCore.Mvc;
using Orbital7.RapidApp2.SampleWebApp.Models.ModalDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp2.SampleWebApp.Controllers
{
    public class ModalDialogsController : Controller
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
            return PartialView(new _PanelBasicModel());
        }

        public IActionResult _DialogMessage(
            string message)
        {
            return PartialView(_DialogMessageModel.Create(message));
        }
    }
}
