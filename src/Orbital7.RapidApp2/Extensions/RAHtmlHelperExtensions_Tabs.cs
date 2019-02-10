using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Orbital7.RapidApp2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Mvc
{
    public static partial class RAHtmlHelperExtensions
    {
        public static IHtmlContent RATabs<TModel>(
            this IHtmlHelper<TModel> htmlHelper,
            List<RATabItem> tabItems)
        {
            var tabs = new TabsView(tabItems);
            return htmlHelper.EditorFor(x => tabs);
        }
    }
}
