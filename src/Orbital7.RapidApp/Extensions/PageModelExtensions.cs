using Microsoft.EntityFrameworkCore;
using Orbital7.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Mvc.RazorPages
{
    public static class PageModelExtensions
    {
        public static void HandleException(
            this PageModel pageModel,
            string prefix,
            Exception ex)
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

            pageModel.ModelState.AddModelError(key, message);
        }

        public static void HandleException(
            this PageModel pageModel,
            Exception ex)
        {
            HandleException(pageModel, String.Empty, ex);
        }
    }
}
