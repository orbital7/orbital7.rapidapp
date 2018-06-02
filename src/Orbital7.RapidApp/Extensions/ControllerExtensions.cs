using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Orbital7.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    // TODO: Move to Orbital7.Extensions.AspNetCore
    public static class ControllerExtensions
    {
        public static byte[] ReadFormFile(this Controller controller, IFormFile formFile)
        {
            if (formFile != null)
            {
                var stream = formFile.OpenReadStream();
                return stream.ReadAll();
            }

            return null;
        }

        public static FileContentResult File(this Controller controller, byte[] fileContents, string contentType, string filename, bool isInLine)
        {
            if (isInLine)
            {
                controller.Response.Headers.Append("Content-Disposition", "inline; filename=" + filename.Replace(",", ""));
                return controller.File(fileContents, contentType);
            }
            else
            {
                return controller.File(fileContents, contentType, filename);
            }
        }

        public static FileContentResult FileInline(this Controller controller, byte[] fileContents, string contentType, string filename)
        {
            return controller.File(fileContents, contentType, filename, true);
        }

        public static void HandleException(this Controller controller, string prefix, Exception ex)
        {
            string key = String.Empty;
            string message = ex.Message;

            if (ex is PropertyException)
            {
                var propertyEx = ex as PropertyException;
                key = !String.IsNullOrEmpty(prefix) ? prefix + "." + propertyEx.PropertyName : propertyEx.PropertyName;
            }
            else if (ex is DbUpdateException)
            {
                message = ex.InnerException?.Message ?? ex.Message;
            }

            controller.ModelState.AddModelError(key, message);
        }

        public static void HandleException(this Controller controller, Exception ex)
        {
            HandleException(controller, String.Empty, ex);
        }

        public static async Task<string> RenderPartialViewToStringAsync(this Controller controller, ICompositeViewEngine viewEngine,
            string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;

            controller.ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                ViewEngineResult viewResult =
                    viewEngine.FindView(controller.ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
