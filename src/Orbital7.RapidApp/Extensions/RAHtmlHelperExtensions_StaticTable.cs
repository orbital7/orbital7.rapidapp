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
        public static IHtmlContent RAStaticTable<TModel, TTableRowItem>(this IHtmlHelper<TModel> htmlHelper,
            IList<TTableRowItem> dynamicTableRowItems, string ajaxCreateRowUrl, string AddRowLinkText)
            where TTableRowItem : TableRowItemBase
        {
            var content = new HtmlContentBuilder();
            content.AppendHtml("<table class=\"ra-statictable\">");
            content.AppendHtml("<tbody class=\"ra-statictable-body\">");

            foreach (var rowItem in dynamicTableRowItems)
                content.AppendHtml(htmlHelper.EditorFor(x => rowItem));

            content.AppendHtml("</tbody>");
            content.AppendHtml("</table>");

            return content;
        }

        public static TagCloser RABeginStaticTableRow<TModel>(this IHtmlHelper<TModel> htmlHelper)
            where TModel : TableRowItemBase
        {
            var model = htmlHelper.ViewData.Model;
            string existingHtmlPrefixField = htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix;
            htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix = model.IndexedHtmlFieldPrefix;

            var contentStart = new HtmlContentBuilder();
            contentStart.AppendHtml("<tr>");
            htmlHelper.ViewContext.Writer.Write(contentStart);

            var contentEnd = new HtmlContentBuilder();
            contentEnd.AppendHtml("<td>");
            contentEnd.AppendHtml(htmlHelper.HiddenFor(x => x.HtmlFieldPrefix));
            contentEnd.AppendFormat("<input type=\"hidden\" id=\"{0}\" name=\"{1}\" value=\"{2}\" />",
                model.IndexId, model.IndexName, model.Index);
            contentEnd.AppendHtml("</td>");
            contentEnd.AppendHtml("</tr>");

            return new TagCloser(htmlHelper, contentEnd, existingHtmlPrefixField);
        }
    }
}
