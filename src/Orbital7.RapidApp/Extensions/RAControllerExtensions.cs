using Microsoft.AspNetCore.Http;
using Orbital7.RapidApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    // TODO: Move to Orbital7.RapidApp.Models
    public static class RAControllerExtensions
    {
        public static IActionResult RASuccess(this Controller controller)
        {
            return controller.Content(String.Empty);
        }

        public static IActionResult RASuccessNavigateTo(this Controller controller, string url)
        {
            return controller.Content(String.Format("window.location.href = '{0}';", url));
        }

        public static IActionResult RASuccessReplaceWindowTo(
            this Controller controller,
            string url)
        {
            return controller.Content($"window.location.replace('{url}');");
        }

        public static IActionResult RASuccessReloadPage(this Controller controller)
        {
            return controller.Content("window.location.reload();");
        }

        public static IActionResult RASuccessExecuteScript(this Controller controller, string javascript)
        {
            return controller.Content(javascript);
        }

        public static IActionResult RASuccessExecuteScript(this Controller controller, string javascript, params object[] formatArgs)
        {
            return controller.Content(String.Format(javascript, formatArgs));
        }

        public static void RAFailure(this Controller controller)
        {
            controller.Response.SetFailureState();
        }

        public static IActionResult RAFailureView(this Controller controller, object model)
        {
            controller.Response.SetFailureState();
            return controller.View(model);
        }

        public static IActionResult RAFailureView(this Controller controller, string viewName, object model)
        {
            controller.Response.SetFailureState();
            return controller.View(viewName, model);
        }

        public static IActionResult RAFailurePartialView(this Controller controller, object model)
        {
            controller.Response.SetFailureState();
            return controller.PartialView(model);
        }

        public static IActionResult RAFailurePartialView(this Controller controller, string viewName, object model)
        {
            controller.Response.SetFailureState();
            return controller.PartialView(viewName, model);
        }

        public static IActionResult RARawHtml(this Controller controller, string html)
        {
            return controller.PartialView("~/Views/RA/RawHtmlContent.cshtml", new RARawHtmlContentModel()
            {
                Html = html,
            });
        }

        public static IActionResult RAPartialViewContentFrame(
            this Controller controller,
            string contentUrl,
            string containerClass = "ra-fullsize",
            string containerStyle = null,
            string frameClass = null,
            string frameStyle = null,
            bool showLoadingFullHeight = true,
            bool printFrameOnLoad = false,
            bool showPrintButton = false)
        {
            return controller.RedirectToAction("PartialViewContentFrame", "RA", new
            {
                contentUrl,
                containerClass,
                containerStyle,
                frameClass,
                frameStyle,
                printFrameOnLoad,
                showPrintButton,
            });
        }

        public static IActionResult RADialogViewContentFrame(
            this Controller controller,
            string contentUrl,
            string containerClass = null,
            string containerStyle = "width: 100%; height: 500px;",
            string frameClass = null,
            string frameStyle = null,
            bool printFrameOnLoad = false, 
            bool showPrintButton = false)
        {
            return controller.RedirectToAction("DialogViewContentFrame", "RA", new
            {
                contentUrl,
                containerClass,
                containerStyle,
                frameClass,
                frameStyle,
                printFrameOnLoad,
                showPrintButton,
            });
        }
    }
}
