using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Orbital7.RapidApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Mvc
{
    public static partial class RAHtmlHelperExtensions
    {
        public static IHtmlContent RATaskbar<TModel>(
            this IHtmlHelper<TModel> htmlHelper,
            string key,
            List<RATaskbarItem> taskbarItems)
        {
            var taskbar = new TaskbarView(key, taskbarItems);
            return htmlHelper.EditorFor(x => taskbar);
        }
    }
}
