using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public enum RAModalDialogSize
    {
        Small,
        Medium,
        Large,
        Full,
    }

    public static partial class RAHtmlHelperExtensions
    {
        public static TagCloser RABeginDropdownButton(
            this IHtmlHelper htmlHelper,
            string buttonHtml,
            string buttonId,
            string buttonClass = "btn-secondary",
            string buttonStyle = null,
            string dropdownClass = "dropdown",
            string dropdownStyle = null)
        {
            var content = new HtmlContentBuilder();
            content.AppendFormat("<div class='{0}' style='{1}'>",
                dropdownClass, dropdownStyle);
            content.AppendHtml(htmlHelper.RAButton(buttonHtml, null, new
            {
                @data_toggle = "dropdown",
                @aria_haspopup = "true",
                @aria_expanded = "false",

            }, buttonId, buttonClass + " dropdown-toggle", buttonStyle));
            content.AppendFormat("<div class='dropdown-menu' aria-labelledby='{0}'>",
                buttonId);

            htmlHelper.ViewContext.Writer.Write(content);
            return new TagCloser(htmlHelper, "</div></div>");
        }

        public static IHtmlContent RADropdownItem(
            this IHtmlHelper htmlHelper, 
            string itemHtml,
            string actionScript,
            object htmlAttributes = null)
        {
            var attributes = HtmlHelperHelper.ToAttributesDictionary(htmlAttributes);
            attributes.AddOrInsertToExisting("class", "dropdown-item ra-dropdown-item");
            attributes.AddOrInsertToExisting("onmousedown", actionScript);

            var tagBuilder = new TagBuilder("a")
            {
                TagRenderMode = TagRenderMode.Normal
            };
            tagBuilder.MergeAttributes(attributes);
            tagBuilder.InnerHtml.AppendHtml(itemHtml);

            return tagBuilder;
        }

        public static IHtmlContent RADropdownItemDivider(
            this IHtmlHelper htmlHelper)
        {
            var content = new HtmlContentBuilder();
            content.AppendHtml("<div class='dropdown-divider'></div>");

            return content;
        }

        public static IHtmlContent RAButton(
            this IHtmlHelper htmlHelper,
            string buttonHtml,
            string actionScript,
            object htmlAttributes = null,
            string buttonId = null,
            string buttonClass = "btn-secondary",
            string buttonStyle = null,
            string buttonType = "button")
        {
            var attributes = HtmlHelperHelper.ToAttributesDictionary(htmlAttributes);
            attributes.AddButtonAttributes(buttonClass, buttonStyle, buttonType);
            if (!string.IsNullOrEmpty(buttonId))
                attributes.AddIfMissing("id", buttonId);
            attributes.Add("onmouseup", actionScript);

            return attributes.ToButton(buttonHtml);
        }

        public static IHtmlContent RAShowModalDialogButton(
            this IHtmlHelper htmlHelper, 
            string buttonHtml, 
            string contentUrl,
            string returnAction = null, 
            object htmlAttributes = null, 
            string dialogTitle = null, 
            bool showActionButton = true,
            string actionButtonCaption = "Save", 
            bool showCancelButton = true, 
            string cancelButtonCaption = "Cancel",
            string buttonClass = "btn-secondary",
            string buttonStyle = null,
            RAModalDialogSize dialogSize = RAModalDialogSize.Medium)
        {
            var attributes = HtmlHelperHelper.ToAttributesDictionary(htmlAttributes);
            attributes.AddButtonAttributes(buttonClass, buttonStyle);
            attributes.Add("onmouseup", htmlHelper.RAShowModalDialogScript(contentUrl, returnAction, dialogTitle ?? buttonHtml,
                showActionButton, actionButtonCaption, showCancelButton, cancelButtonCaption, dialogSize));

            return attributes.ToButton(buttonHtml);
        }

        public static IHtmlContent RAShowDialogLink(
            this IHtmlHelper htmlHelper, 
            string linkText, 
            string contentUrl,
            string returnAction = null, 
            object htmlAttributes = null, 
            string dialogTitle = null, 
            bool showActionButton = true,
            string actionButtonCaption = "Save", 
            bool showCancelButton = true, 
            string cancelButtonCaption = "Cancel",
            RAModalDialogSize dialogSize = RAModalDialogSize.Medium)
        {
            var attributes = HtmlHelperHelper.ToAttributesDictionary(htmlAttributes);
            attributes.AddOrInsertToExisting("class", "ra-clickable");
            attributes.Add("href", "#");
            attributes.Add("onmouseup", htmlHelper.RAShowModalDialogScript(contentUrl, returnAction, dialogTitle ?? linkText,
                showActionButton, actionButtonCaption, showCancelButton, cancelButtonCaption, dialogSize));

            var tagBuilder = new TagBuilder("a")
            {
                TagRenderMode = TagRenderMode.Normal
            };
            tagBuilder.MergeAttributes(attributes);
            tagBuilder.InnerHtml.AppendHtml(linkText);

            return tagBuilder;
        }

        public static string RAShowModalDialogScript(
            this IHtmlHelper htmlHelper, 
            string contentUrl, 
            string returnAction, 
            string dialogTitle,
            bool showActionButton = true, 
            string actionButtonCaption = "Save", 
            bool showCancelButton = true, 
            string cancelButtonCaption = "Cancel",
            RAModalDialogSize dialogSize = RAModalDialogSize.Medium)
        {
            return String.Format("raShowModalDialog('{0}', '{1}', '{2}', {3}, '{4}', {5}, '{6}', '{7}', this);",
                contentUrl, 
                returnAction, 
                dialogTitle, 
                showActionButton.ToString().ToLower(), 
                actionButtonCaption, 
                showCancelButton.ToString().ToLower(), 
                cancelButtonCaption,
                dialogSize.ToString().ToLower());
        }

        private static void AddButtonAttributes(
            this IDictionary<string, object> attributes,
            string buttonClass,
            string buttonStyle = null,
            string buttonType = "button")
        {
            attributes.AddOrInsertToExisting("class", "btn " + buttonClass);
            attributes.AddOrInsertToExisting("style", buttonStyle);
            attributes.Add("type", buttonType);
        }
    }
}
