﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Orbital7.RapidApp2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static partial class RAHtmlHelperExtensions
    {
        public static IHtmlContent RADynamicTable<TModel, TTableRowItem>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, IList<TTableRowItem>>> tableRowItemsExpression,
            string ajaxAddRowUrl,
            string addRowLinkText,
            string tHeadInnerHtml = null)
            where TTableRowItem : RATableRowItemBase
        {
            var content = new HtmlContentBuilder();
            string tableId = Guid.NewGuid().ToString().Replace("-", "");

            content.AppendFormat("<table id=\"{0}\" class=\"ra-dynamictable\">", tableId);
            if (!String.IsNullOrEmpty(tHeadInnerHtml))
                content.AppendHtml("<thead>" + tHeadInnerHtml + "</thead>");
            content.AppendHtml("<tbody class=\"ra-dynamictable-body\">");

            var htmlFieldPrefix = ExpressionHelper.GetExpressionText(tableRowItemsExpression);
            var modelExplorer = htmlHelper.GetModelExplorer(tableRowItemsExpression);
            foreach (var rowItem in (IList<TTableRowItem>)modelExplorer.Model)
            {
                rowItem.HtmlFieldPrefix = htmlFieldPrefix;
                content.AppendHtml(htmlHelper.EditorFor(x => rowItem));
            }

            var completeAjaxAddRowUrl = ajaxAddRowUrl += "&htmlFieldPrefix=" + htmlFieldPrefix;
            content.AppendHtml("</tbody>");
            content.AppendHtml("</table>");
            content.AppendHtml("<div>");
            content.AppendHtml(htmlHelper.RAButton("<i class='fas fa-plus'></i> " + addRowLinkText,
                string.Format("raAddDynamicTableRow('{0}', '{1}');", tableId, completeAjaxAddRowUrl),
                buttonClass: "btn-primary ra-btn-xs"));
            content.AppendHtml("</button>");
            content.AppendHtml("</div>");

            return content;
        }

        public static TagCloser RABeginDynamicTableRow<TModel>(
            this IHtmlHelper<TModel> htmlHelper)
            where TModel : RATableRowItemBase
        {
            var model = htmlHelper.ViewData.Model;
            string existingHtmlPrefixField = htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix;
            htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix = model.IndexedHtmlFieldPrefix;

            var contentStart = new HtmlContentBuilder();
            contentStart.AppendHtml("<tr>");
            htmlHelper.ViewContext.Writer.Write(contentStart);

            var contentEnd = new HtmlContentBuilder();
            contentEnd.AppendHtml("<td>");
            contentEnd.AppendHtml(htmlHelper.RAButton("<i class='fas fa-times'></i>",
                "raRemoveDynamicTableRow(this);",
                buttonClass: "btn-secondary ra-btn-xs ra-btn-delete"));
            contentEnd.AppendHtml(htmlHelper.HiddenFor(x => x.HtmlFieldPrefix));
            contentEnd.AppendFormat("<input type=\"hidden\" id=\"{0}\" name=\"{1}\" value=\"{2}\" />",
                model.IndexId, model.IndexName, model.Index);
            contentEnd.AppendHtml("</td>");
            contentEnd.AppendHtml("</tr>");

            return new TagCloser(htmlHelper, contentEnd, existingHtmlPrefixField);
        }
    }
}
