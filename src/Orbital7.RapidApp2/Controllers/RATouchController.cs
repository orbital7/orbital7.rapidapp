using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orbital7.RapidApp2.Models;

namespace Orbital7.RapidApp2.Controllers
{
    [AllowAnonymous]
    public class RATouchController : Controller
    {
        public IActionResult RADialogTouchPad(string valueId, TouchPadMode mode, string postEditUpdateScript)
        {
            return PartialView(new RATouchPadModel()
            {
                ValueId = valueId,
                Mode = mode,
                PostEditUpdateScript = postEditUpdateScript,
            });
        }
    }
}