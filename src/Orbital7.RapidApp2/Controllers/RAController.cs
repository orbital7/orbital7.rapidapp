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
    public class RAController : Controller
    {
        public IActionResult JumpToUrl(string url)
        {
            return PartialView("JumpToUrl", url);
        }

        public IActionResult RawHtmlContent(string html)
        {
            return PartialView(new RARawHtmlContentModel()
            {
                Html = html,
            });
        }

        public IActionResult PartialViewContentFrame(string contentUrl,
            string containerClass = null,
            string containerStyle = null,
            string frameClass = null,
            string frameStyle = null,
            bool showLoadingFullHeight = false,
            bool printFrameOnLoad = false,
            bool showPrintButton = false)
        {
            return PartialView(new RAContentFrameModel()
            {
                ContentUrl = contentUrl,
                ContainerClass = containerClass,
                ContainerStyle = containerStyle,
                FrameClass = frameClass,
                FrameStyle = frameStyle,
                ShowLoadingFullHeight = showLoadingFullHeight,
                PrintFrameOnLoad = printFrameOnLoad,
                ShowPrintButton = showPrintButton,
            });
        }

    public IActionResult DialogViewContentFrame(string contentUrl, 
            string containerClass = null,
            string containerStyle = null,
            string frameClass = null,
            string frameStyle = null,
            bool showLoadingFullHeight = false,
            bool printFrameOnLoad = false,
            bool showPrintButton = false)
        {
            return PartialView(new RAContentFrameModel()
            {
                ContentUrl = contentUrl,
                ContainerClass = containerClass,
                ContainerStyle = containerStyle,
                FrameClass = frameClass,
                FrameStyle = frameStyle,
                ShowLoadingFullHeight = showLoadingFullHeight,
                PrintFrameOnLoad = printFrameOnLoad,
                ShowPrintButton = showPrintButton,
            });
        }
    }
}