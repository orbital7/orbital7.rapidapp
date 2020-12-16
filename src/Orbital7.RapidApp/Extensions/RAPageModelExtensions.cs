using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Mvc.RazorPages
{
    public static class RAPageModelExtensions
    {
        public static IActionResult RAFailurePage(
            this PageModel pageModel)
        {
            pageModel.Response.SetFailureState();
            return pageModel.Page();
        }

        public static IActionResult RASuccess(
            this PageModel pageModel)
        {
            return pageModel.Content(string.Empty);
        }

        public static IActionResult RASuccessNavigateTo(
            this PageModel pageModel, 
            string url)
        {
            return pageModel.Content($"window.location.href = '{url}';");
        }

        public static IActionResult RASuccessReplaceWindowTo(
            this PageModel pageModel,
            string url)
        {
            return pageModel.Content($"window.location.replace('{url}');");
        }

        public static IActionResult RASuccessReloadPage(
            this PageModel pageModel)
        {
            return pageModel.Content("window.location.reload();");
        }

        public static IActionResult RASuccessExecuteScript(
            this PageModel pageModel, 
            string javascript)
        {
            return pageModel.Content(javascript);
        }

        public static IActionResult RASuccessExecuteScript(
            this PageModel pageModel, 
            string javascript, 
            params object[] formatArgs)
        {
            return pageModel.Content(string.Format(javascript, formatArgs));
        }
    }
}
