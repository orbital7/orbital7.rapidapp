using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static partial class RAHtmlHelperExtensions
    {
        public static TagCloser RABeginTouchTable<TModel>(this IHtmlHelper<TModel> htmlHelper)
        {
            htmlHelper.ViewContext.Writer.Write("<table class='ra-touchtable'>");
            return new TagCloser(htmlHelper, "</table>");
        }

        public static TagCloser RABeginTouchTableClickableRow<TModel>(this IHtmlHelper<TModel> htmlHelper, string onClickUpScript, 
            string supplimentalRowHtml = null)
        {
            htmlHelper.ViewContext.Writer.Write(
                "<tr class=\"ra-clickable\" onmouseup=\"{0}\" {1}>", 
                onClickUpScript, supplimentalRowHtml);
            return new TagCloser(htmlHelper, "</tr>");
        }

        public static IHtmlContent RATouchTableHeaderRow<TModel>(this IHtmlHelper<TModel> htmlHelper, string headerText)
        {
            var content = new HtmlContentBuilder();
            content.AppendFormat("<tr><td colspan='3' class='ra-touchtable-cell ra-touchtable-cell-header'>{0}</td></tr>", headerText);
            return content;
        }

        public static IHtmlContent RATouchTableCells<TModel>(this IHtmlHelper<TModel> htmlHelper, 
            string leftText, string rightText = null, string leftImageUrl = null)
        {
            var content = new HtmlContentBuilder();

            if (!String.IsNullOrEmpty(leftImageUrl))
                content.AppendFormat("<td class='ra-touchtable-cell-image'><img src='{0}' class='ra-touchinput-image' /></td>", leftImageUrl);
            else
                content.AppendHtml("<td></td>");
            content.AppendFormat("<td class='ra-touchtable-cell ra-font-bold'>{0}</td>", leftText);
            content.AppendFormat("<td class='ra-touchtable-cell ra-align-right'>{0}</td>", rightText);

            return content;
        }
    }
}
