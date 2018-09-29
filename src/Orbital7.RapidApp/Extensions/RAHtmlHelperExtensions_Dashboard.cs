using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Orbital7.RapidApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static partial class RAHtmlHelperExtensions
    {
        public static IHtmlContent RADashboardTable(
            this IHtmlHelper htmlHelper, 
            string title,
            List<RADashboardItem> dashboardItems, 
            string noDashboardItemsText = null)
        {
            var content = new HtmlContentBuilder();
            content.AppendFormat("<div class='ra-dashboard-heading'>{0}</div>", title);
            content.AppendHtml("<table class=''><tbody>");

            if (dashboardItems.Count > 0)
            {
                content.AppendHtml("<tr>");
                foreach (var item in dashboardItems)
                    AppendDashboardItemCell(content, "upper", item.Count.ToString(), item.Url, item.State);
                content.AppendHtml("</tr>");

                content.AppendHtml("<tr>");
                foreach (var item in dashboardItems)
                    AppendDashboardItemCell(content, "lower", item.Description, item.Url, item.State);
                content.AppendHtml("</tr>");
            }
            else
            {
                content.AppendFormat("<tr><td class='ra-dashboard-cell-noitems'>{0}</td></tr>", 
                    noDashboardItemsText);
            }

            content.AppendHtml("</tbody></table>");
            return content;
        }

        private static void AppendDashboardItemCell(
            HtmlContentBuilder content, 
            string mainClass, 
            string text, 
            string url, 
            DashboardState state)
        {
            content.AppendFormat("<td class='ra-dashboard-cell-value-{0} ra-dashboard-cell-{1} {2}'",
                    mainClass, state.ToString().ToLower(), !String.IsNullOrEmpty(url) ? "ra-clickable" : null);

            if (!String.IsNullOrEmpty(url))
            {
                content.AppendFormat(" onmouseup='navigateTo(\"{0}\");'", url);
            }

            content.AppendFormat(">{0}</td><td class='ra-dashboard-cell-spacer'></td>", text);
        }
    }
}
