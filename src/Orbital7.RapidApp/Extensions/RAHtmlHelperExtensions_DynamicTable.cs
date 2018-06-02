using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Orbital7.RapidApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static partial class RAHtmlHelperExtensions
    {
        public static IHtmlContent RADynamicTable<TModel, TTableRowItem>(this IHtmlHelper<TModel> htmlHelper, 
            IList<TTableRowItem> tableRowItems, string ajaxCreateRowUrl, string AddRowLinkText)
            where TTableRowItem : TableRowItemBase
        {
            var content = new HtmlContentBuilder();
            string tableId = Guid.NewGuid().ToString().Replace("-", "");

            content.AppendFormat("<table id=\"{0}\" class=\"ra-dynamictable\">", tableId);
            content.AppendHtml("<tbody class=\"ra-dynamictable-body\">");

            foreach (var rowItem in tableRowItems)
                content.AppendHtml(htmlHelper.EditorFor(x => rowItem));

            content.AppendHtml("</tbody>");
            content.AppendHtml("</table>");
            content.AppendHtml("<div>");
            content.AppendHtml("<a class=\"ra-glyphbutton ra-glyphbutton-green\" ");
            content.AppendFormat("ontouchend=\"addDynamicTableRow('{0}', '{1}'); event.preventDefault();\" ", 
                tableId, ajaxCreateRowUrl);
            content.AppendFormat("onmouseup=\"addDynamicTableRow('{0}', '{1}');\" ",
                tableId, ajaxCreateRowUrl);
            content.AppendHtml("><span class=\"glyphicon glyphicon-plus-sign\" aria-hidden=\"true\"></span>");
            content.AppendFormat("<span class=\"ra-glyphbutton-text\">{0}</span>", AddRowLinkText);
            content.AppendHtml("</a>");
            content.AppendHtml("</div>");

            return content;
        }

        public static TagCloser RABeginDynamicTableRow<TModel>(this IHtmlHelper<TModel> htmlHelper)
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
            contentEnd.AppendHtml("<a class=\"ra-glyphbutton ra-glyphbutton-red\" ");
            contentEnd.AppendHtml("ontouchend=\"removeDynamicTableRow(this); event.preventDefault();\" ");
            contentEnd.AppendHtml("onmouseup=\"removeDynamicTableRow(this);\">");
            contentEnd.AppendHtml("<span class=\"glyphicon glyphicon-remove-sign\" aria-hidden=\"true\"></span>");
            contentEnd.AppendHtml("</a>");
            contentEnd.AppendHtml(htmlHelper.HiddenFor(x => x.HtmlFieldPrefix));
            contentEnd.AppendFormat("<input type=\"hidden\" id=\"{0}\" name=\"{1}\" value=\"{2}\" />",
                model.IndexId, model.IndexName, model.Index);
            contentEnd.AppendHtml("</td>");
            contentEnd.AppendHtml("</tr>");

            return new TagCloser(htmlHelper, contentEnd, existingHtmlPrefixField);
        }
    }
}
