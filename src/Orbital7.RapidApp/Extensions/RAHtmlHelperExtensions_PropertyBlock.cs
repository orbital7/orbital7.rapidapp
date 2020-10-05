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
        public static TagCloser RABeginPropertyBlockDisplayCustom(
            this IHtmlHelper htmlHelper, 
            string displayLabel,
            string displayValueClass = null, 
            string displayValueStyle = null)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            var content = new HtmlContentBuilder();
            content.AppendPropertyBlockDisplayHtmlStart(displayLabel, displayValueClass, displayValueStyle);
            htmlHelper.ViewContext.Writer.Write(content);
            return new TagCloser(htmlHelper, GetPropertyBlockDisplayHtmlEnd());
        }

        private static void AppendPropertyBlockDisplayHtmlStart(
            this HtmlContentBuilder content, 
            string displayLabel,
            string displayValueClass, 
            string displayValueStyle)
        {
            content.AppendHtml("<div class='ra-propertyblock-display'>");
            content.AppendFormat("<div class='ra-propertyblock-field-label'>{0}</div>", displayLabel);
            content.AppendFormat("<div class='ra-propertyblock-field-value {0}' style='{1}'>", displayValueClass, displayValueStyle);
        }

        private static string GetPropertyBlockDisplayHtmlEnd()
        {
            return "</div></div>";
        }

        public static TagCloser RABeginPropertyBlockDisplayCustomFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, 
            string displayValueClass = null, 
            string displayValueStyle = null,
            string displayLabel = null)
        {
            var modelExplorer = htmlHelper.GetModelExplorer(expression);

            var content = new HtmlContentBuilder();
            content.AppendPropertyBlockDisplayHtmlStart(String.IsNullOrEmpty(displayLabel) ? modelExplorer.GetPropertyDisplayName() : displayLabel,
                displayValueClass, displayValueStyle);
            htmlHelper.ViewContext.Writer.Write(content);
            return new TagCloser(htmlHelper, GetPropertyBlockDisplayHtmlEnd());
        }

        public static IHtmlContent RAPropertyBlockDisplay<TModel>(
            this IHtmlHelper<TModel> htmlHelper,
            string displayLabel, 
            object displayValue, 
            string displayValueNull = "-",
            string displayValueUrl = null, 
            string displayValueClass = null, 
            string displayValueStyle = null)
        {
            var content = new HtmlContentBuilder();
            content.AppendPropertyBlockDisplayHtmlStart(displayLabel, displayValueClass, displayValueStyle);
            content.AppendHtml(htmlHelper.GetValueDisplayHtml(displayValue, displayValueNull, displayValueUrl));
            content.AppendHtml(GetPropertyBlockDisplayHtmlEnd());
            return content;
        }

        public static IHtmlContent RAPropertyBlockDisplayFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, 
            string displayValueOverride = null, 
            string displayValueNull = "-",
            string displayValueUrl = null, 
            string displayValueClass = null, 
            string displayValueStyle = null, 
            string displayLabel = null,
            string fieldNameOverride = null)
        {
            var modelExplorer = htmlHelper.GetModelExplorer(expression);

            var content = new HtmlContentBuilder();
            content.AppendPropertyBlockDisplayHtmlStart(String.IsNullOrEmpty(displayLabel) ? modelExplorer.GetPropertyDisplayName() : displayLabel,
                displayValueClass, displayValueStyle);
            content.AppendHtml(
                htmlHelper.GetValueDisplayHtml(
                    modelExplorer.Model, 
                    displayValueNull, 
                    displayValueUrl, 
                    displayValueOverride,
                    fieldNameOverride ?? modelExplorer.Metadata.PropertyName));
            content.AppendHtml(GetPropertyBlockDisplayHtmlEnd());
            return content;
        }

        public static IHtmlContent RAPropertyBlockEditorFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, 
            object htmlAttributes = null, 
            SelectList selectList = null,
            RAEditorType? editorTypeOverride = null, 
            string displayLabel = null, 
            string selectListOptionLabel = null,
            string descriptionOverride = null)
        {
            var modelExplorer = htmlHelper.GetModelExplorer(expression);

            var content = new HtmlContentBuilder();
            content.AppendHtml("<div class='ra-propertyblock-editor'>");
            content.AppendFormat("<div class='ra-propertyblock-field-label'>{0}</div>",
                String.IsNullOrEmpty(displayLabel) ? modelExplorer.GetPropertyDisplayName() : displayLabel);
            content.AppendHtml("<div class='ra-propertyblock-field-editor'>");

            content.AppendHtml(htmlHelper.GetEditorFor(expression, modelExplorer, htmlAttributes,
                selectList, editorTypeOverride, false, selectListOptionLabel));

            string description = !String.IsNullOrEmpty(descriptionOverride) ? descriptionOverride :
                modelExplorer.Metadata?.Description;
            content.AppendFormat("<div class='ra-description-info'>{0}</div>", description);

            content.AppendHtml("<div class='ra-validation-error'>");
            content.AppendHtml(htmlHelper.ValidationMessageFor(expression));
            content.AppendHtml("</div></div></div>");

            return content;
        }
    }
}
