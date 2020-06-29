using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static partial class RAHtmlHelperExtensions
    {
        public static TagCloser RABeginToolbar(this IHtmlHelper htmlHelper,
            string toolbarClass = null, string toolbarStyle = null)
        {
            var content = new HtmlContentBuilder();

            content.AppendFormat("<nav class='navbar ra-toolbar {0}' style='{1}'>", toolbarClass, toolbarStyle);

            content.AppendFormat("<form class='form-inline' type='{0}' src='{1}'>", "POST",
                htmlHelper.ViewContext.HttpContext.Request.Path);

            htmlHelper.ViewContext.Writer.Write(content);
            return new TagCloser(htmlHelper, "</form></nav>");
        }

        public static TagCloser RABeginToolbarMultiSelectedRowEditor(this IHtmlHelper htmlHelper)
        {
            var content = new HtmlContentBuilder();
            content.AppendHtml("</div>");   // Close off the primary form group from RABeginToolbar.
            content.AppendHtml("<div class='ra-datagrid-multiselect-toolbar-editor' style='display: none;'>");
            htmlHelper.ViewContext.Writer.Write(content);
            return new TagCloser(htmlHelper, "");
        }

        public static IHtmlContent RAToolbarEditorFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, SelectList selectList = null,
            RAEditorType? editorTypeOverride = null, string displayLabel = null, string displayLabelClass = null,
            string selectListOptionLabel = null)
        {
            var modelExplorer = htmlHelper.GetModelExplorer(expression);

            var content = new HtmlContentBuilder();

            var label = String.IsNullOrEmpty(displayLabel) ? modelExplorer.GetPropertyDisplayName() : displayLabel;
            if (!String.IsNullOrEmpty(label))
            {
                var id = htmlHelper.GenerateIdFromName(
                    htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlHelper.GetExpressionText(expression)));
                content.AppendFormat("<label for='{0}' class='ra-toolbar-label {1}'>{2}:</label>", id,
                    displayLabelClass, label);
            }

            content.AppendHtml(htmlHelper.GetEditorFor(expression, modelExplorer, htmlAttributes, selectList,
                editorTypeOverride, true, selectListOptionLabel));

            return content;
        }

        public static IHtmlContent RAToolbarButton(
            this IHtmlHelper htmlHelper, 
            string buttonText, 
            object htmlAttributes = null,
            string navigateToUrl = null,
            string buttonClass = "btn-secondary",
            string buttonStyle = null)
        {
            var attributes = HtmlHelperHelper.ToAttributesDictionary(htmlAttributes);
            attributes.AddButtonAttributes(buttonClass, buttonStyle);
            attributes.AddToolbarButtonAttributes();
            if (!String.IsNullOrEmpty(navigateToUrl))
                attributes.AddOrAppendToExisting("onmouseup", String.Format("navigateTo('{0}')", navigateToUrl), "; ");

            return attributes.ToButton(buttonText);
        }

        public static IHtmlContent RAToolbarShowDialogButton(
            this IHtmlHelper htmlHelper, 
            string buttonText, 
            string contentUrl,
            string returnUrl = null, 
            object htmlAttributes = null, 
            string dialogTitle = null, 
            bool showActionButton = true,
            string actionButtonCaption = "Save", 
            bool showCancelButton = true, 
            string cancelButtonCaption = "Cancel",
            string buttonClass = "btn-secondary",
            string buttonStyle = null)
        {
            var attributes = HtmlHelperHelper.ToAttributesDictionary(htmlAttributes);
            attributes.AddToolbarButtonAttributes();

            return htmlHelper.RAShowModalDialogButton(buttonText, contentUrl, returnUrl, attributes, dialogTitle, showActionButton,
                actionButtonCaption, showCancelButton, cancelButtonCaption, buttonClass, buttonStyle);
        }

        private static void AddToolbarButtonAttributes(
            this IDictionary<string, object> attributes)
        {
            attributes.AddOrAppendToExisting("class", "btn-sm");
            attributes.AddOrAppendToExisting("class", "ra-toolbar-button");
        }
    }
}
