using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static partial class RAHtmlHelperExtensions
    {
        public static IHtmlContent RAShowDialogButton(this IHtmlHelper htmlHelper, string buttonText, string contentUrl,
            string returnAction = null, object htmlAttributes = null, string dialogTitle = null, bool showActionButton = true,
            string actionButtonCaption = "Save", bool showCancelButton = true, string cancelButtonCaption = "Cancel")
        {
            var attributes = HtmlHelperHelper.ToAttributesDictionary(htmlAttributes);
            attributes.AddButtonAttributes(buttonText);
            attributes.Add("ontouchend", htmlHelper.RAShowDialogOnClickScript(contentUrl, returnAction, dialogTitle ?? buttonText,
                showActionButton, actionButtonCaption, showCancelButton, cancelButtonCaption) + " event.preventDefault();");
            attributes.Add("onmouseup", htmlHelper.RAShowDialogOnClickScript(contentUrl, returnAction, dialogTitle ?? buttonText,
                showActionButton, actionButtonCaption, showCancelButton, cancelButtonCaption));

            return attributes.ToInput();
        }

        public static IHtmlContent RAShowDialogLink(this IHtmlHelper htmlHelper, string linkText, string contentUrl,
            string returnAction = null, object htmlAttributes = null, string dialogTitle = null, bool showActionButton = true,
            string actionButtonCaption = "Save", bool showCancelButton = true, string cancelButtonCaption = "Cancel")
        {
            var attributes = HtmlHelperHelper.ToAttributesDictionary(htmlAttributes);
            attributes.AddOrInsertToExisting("class", "clickable");
            attributes.Add("ontouchend", htmlHelper.RAShowDialogOnClickScript(contentUrl, returnAction, dialogTitle ?? linkText,
                showActionButton, actionButtonCaption, showCancelButton, cancelButtonCaption) + " event.preventDefault();");
            attributes.Add("onmouseup", htmlHelper.RAShowDialogOnClickScript(contentUrl, returnAction, dialogTitle ?? linkText,
                showActionButton, actionButtonCaption, showCancelButton, cancelButtonCaption));

            var tagBuilder = new TagBuilder("a")
            {
                TagRenderMode = TagRenderMode.Normal
            };
            tagBuilder.MergeAttributes(attributes);
            tagBuilder.InnerHtml.AppendHtml(linkText);

            return tagBuilder;
        }

        public static string RAShowDialogOnClickScript(this IHtmlHelper htmlHelper, string contentUrl, string returnAction, string dialogTitle,
            bool showActionButton = true, string actionButtonCaption = "Save", bool showCancelButton = true, string cancelButtonCaption = "Cancel")
        {
            return String.Format("showModalDialog('{0}', '{1}', '{2}', {3}, '{4}', {5}, '{6}', this);",
                contentUrl, returnAction, dialogTitle, showActionButton.ToString().ToLower(), actionButtonCaption, showCancelButton.ToString().ToLower(), 
                cancelButtonCaption);
        }

        private static void AddButtonAttributes(this IDictionary<string, object> attributes, string buttonText)
        {
            attributes.AddOrInsertToExisting("class", "btn");
            attributes.Add("type", "button");
            attributes.Add("value", buttonText);
        }
    }
}
