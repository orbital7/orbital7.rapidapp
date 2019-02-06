using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    // TODO: This was the original implementation of the TouchInputs. This needs to merged in with the new TouchInputs implementation.

    public static partial class RAHtmlHelperExtensions
    {
        public static IHtmlContent RATouchPropertyBlockFor<TModel, TProperty1>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> primaryValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null)
        {
            return htmlHelper.TouchPropertyBlockFor<TModel, object, TProperty1, object, object, object>(null, primaryValueExpression,
                null, null, null, touchDialogValueEditUrl, leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle,
                rightDisplayValueStyle, displayLabel);
        }

        public static IHtmlContent RATouchPropertyBlockFor<TModel, TProperty1, TProperty2>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> primaryValueExpression,
            Expression<Func<TModel, TProperty2>> primaryDisplayValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null)
        {
            return htmlHelper.TouchPropertyBlockFor<TModel, object, TProperty1, TProperty2, object, object>(null, primaryValueExpression,
                primaryDisplayValueExpression, null, null, touchDialogValueEditUrl, leftDisplayValueClass, rightDisplayValueClass,
                leftDisplayValueStyle, rightDisplayValueStyle, displayLabel);
        }

        public static IHtmlContent RATouchPropertyBlockFor<TModel, TProperty1, TProperty2, TProperty3>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> primaryValueExpression,
            Expression<Func<TModel, TProperty2>> primaryDisplayValueExpression,
            Expression<Func<TModel, TProperty3>> secondaryValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null)
        {
            return htmlHelper.TouchPropertyBlockFor<TModel, object, TProperty1, TProperty2, TProperty3, object>(null, primaryValueExpression,
                primaryDisplayValueExpression, secondaryValueExpression, null, touchDialogValueEditUrl, leftDisplayValueClass,
                rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle, displayLabel);
        }

        public static IHtmlContent RATouchPropertyBlockFor<TModel, TProperty1, TProperty2, TProperty3, TProperty4>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> primaryValueExpression,
            Expression<Func<TModel, TProperty2>> primaryDisplayValueExpression,
            Expression<Func<TModel, TProperty3>> secondaryValueExpression,
            Expression<Func<TModel, TProperty4>> secondaryDisplayValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null)
        {
            return htmlHelper.TouchPropertyBlockFor<TModel, object, TProperty1, TProperty2, TProperty3, TProperty4>(null, primaryValueExpression,
                primaryDisplayValueExpression, secondaryValueExpression, secondaryDisplayValueExpression, touchDialogValueEditUrl,
                leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle, displayLabel);
        }

        public static IHtmlContent RATouchPropertyBlockWithImageFor<TModel, TProperty1, TProperty2>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> imageUrlExpression,
            Expression<Func<TModel, TProperty2>> primaryValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null)
        {
            return htmlHelper.TouchPropertyBlockFor<TModel, TProperty1, TProperty2, object, object, object>(imageUrlExpression,
                primaryValueExpression, null, null, null, touchDialogValueEditUrl, leftDisplayValueClass, rightDisplayValueClass,
                leftDisplayValueStyle, rightDisplayValueStyle, displayLabel);
        }

        public static IHtmlContent RATouchPropertyBlockWithImageFor<TModel, TProperty1, TProperty2, TProperty3>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> imageUrlExpression,
            Expression<Func<TModel, TProperty2>> primaryValueExpression,
            Expression<Func<TModel, TProperty3>> primaryDisplayValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null)
        {
            return htmlHelper.TouchPropertyBlockFor<TModel, TProperty1, TProperty2, TProperty3, object, object>(imageUrlExpression, primaryValueExpression,
                primaryDisplayValueExpression, null, null, touchDialogValueEditUrl, leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle,
                rightDisplayValueStyle, displayLabel);
        }

        public static IHtmlContent RATouchPropertyBlockWithImageFor<TModel, TProperty1, TProperty2, TProperty3, TProperty4>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> imageUrlExpression,
            Expression<Func<TModel, TProperty2>> primaryValueExpression,
            Expression<Func<TModel, TProperty3>> primaryDisplayValueExpression,
            Expression<Func<TModel, TProperty4>> secondaryValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null)
        {
            return htmlHelper.TouchPropertyBlockFor<TModel, TProperty1, TProperty2, TProperty3, TProperty4, object>(imageUrlExpression, primaryValueExpression,
                primaryDisplayValueExpression, secondaryValueExpression, null, touchDialogValueEditUrl, leftDisplayValueClass, rightDisplayValueClass,
                leftDisplayValueStyle, rightDisplayValueStyle, displayLabel);
        }

        public static IHtmlContent RATouchPropertyBlockWithImageFor<TModel, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> imageUrlExpression,
            Expression<Func<TModel, TProperty2>> primaryValueExpression,
            Expression<Func<TModel, TProperty3>> primaryDisplayValueExpression,
            Expression<Func<TModel, TProperty4>> secondaryValueExpression,
            Expression<Func<TModel, TProperty5>> secondaryDisplayValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null)
        {
            return htmlHelper.TouchPropertyBlockFor<TModel, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5>(imageUrlExpression, primaryValueExpression,
                primaryDisplayValueExpression, secondaryValueExpression, secondaryDisplayValueExpression, touchDialogValueEditUrl,
                leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle, displayLabel);
        }

        private static IHtmlContent TouchPropertyBlockFor<TModel, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> imageUrlExpression,
            Expression<Func<TModel, TProperty2>> primaryValueExpression,
            Expression<Func<TModel, TProperty3>> primaryDisplayValueExpression,
            Expression<Func<TModel, TProperty4>> secondaryValueExpression,
            Expression<Func<TModel, TProperty5>> secondaryDisplayValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null)
        {
            var primaryValueModelExplorer = htmlHelper.GetModelExplorer(primaryValueExpression);
            var primaryDisplayValueModelExplorer = primaryDisplayValueExpression != null ?
                htmlHelper.GetModelExplorer(primaryDisplayValueExpression) : null;
            var secondaryValueModelExplorer = secondaryValueExpression != null ?
                htmlHelper.GetModelExplorer(secondaryValueExpression) : null;
            var secondaryDisplayValueModelExplorer = secondaryDisplayValueExpression != null ?
                htmlHelper.GetModelExplorer(secondaryDisplayValueExpression) : null;
            var imageUrlModelExplorer = imageUrlExpression != null ?
                htmlHelper.GetModelExplorer(imageUrlExpression) : null;

            var content = new HtmlContentBuilder();
            content.AppendTouchPropertyBlockHtmlStart(GetTouchPropertyBlockType(primaryValueModelExplorer), GetTouchPropertyBlockType(secondaryValueModelExplorer), touchDialogValueEditUrl,
                String.IsNullOrEmpty(displayLabel) ? primaryValueModelExplorer.GetPropertyDisplayName() : displayLabel,
                leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle, imageUrlExpression != null, false);
            content.AppendHtml(htmlHelper.HiddenFor(primaryValueExpression));
            if (primaryDisplayValueExpression != null)
                content.AppendHtml(htmlHelper.HiddenFor(primaryDisplayValueExpression));
            if (secondaryValueExpression != null)
                content.AppendHtml(htmlHelper.HiddenFor(secondaryValueExpression));
            if (secondaryDisplayValueExpression != null)
                content.AppendHtml(htmlHelper.HiddenFor(secondaryDisplayValueExpression));
            if (imageUrlExpression != null)
                content.AppendHtml(htmlHelper.HiddenFor(imageUrlExpression));
            content.AppendHtml(GetTouchPropertyBlockHtmlEnd(htmlHelper.IdFor(primaryValueExpression), primaryValueModelExplorer.Model,
                primaryDisplayValueExpression != null ? htmlHelper.IdFor(primaryDisplayValueExpression) : "", primaryDisplayValueModelExplorer?.Model,
                secondaryValueExpression != null ? htmlHelper.IdFor(secondaryValueExpression) : "", secondaryValueModelExplorer?.Model,
                secondaryDisplayValueExpression != null ? htmlHelper.IdFor(secondaryDisplayValueExpression) : "", secondaryDisplayValueModelExplorer?.Model,
                imageUrlExpression != null ? htmlHelper.IdFor(imageUrlExpression) : "", imageUrlModelExplorer?.Model));

            return content;
        }

        private static string GetTouchPropertyBlockType(ModelExplorer modelExplorer)
        {
            if (modelExplorer != null)
            {
                if (modelExplorer.ModelType == typeof(decimal) || modelExplorer.ModelType == typeof(decimal?))
                    return "currency";
                else if (modelExplorer.ModelType == typeof(string) ||
                    modelExplorer.ModelType == typeof(Enum) ||
                    modelExplorer.ModelType == typeof(Guid) || modelExplorer.ModelType == typeof(Guid?))
                    return "select";
            }

            return null;
        }

        public static IHtmlContent RATouchPropertyBlockPINFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            string touchDialogValueEditUrl = TOUCH_DIALOG_EDIT_URL_PIN,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null,
            string postEditUpdateScript = null,
            string staticImageUrl = null)
        {
            return htmlHelper.TouchPropertyBlockNumericFor<TModel, object, TProperty>(null, expression, touchDialogValueEditUrl,
                "pin", leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle,
                displayLabel, postEditUpdateScript, staticImageUrl);
        }

        public static IHtmlContent RATouchPropertyBlockCurrencyFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            string touchDialogValueEditUrl = TOUCH_DIALOG_EDIT_URL_CURRENCY,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null,
            string postEditUpdateScript = null,
            string staticImageUrl = null)
        {
            return htmlHelper.TouchPropertyBlockNumericFor<TModel, object, TProperty>(null, expression, touchDialogValueEditUrl,
                "currency", leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle,
                displayLabel, postEditUpdateScript, staticImageUrl);
        }

        public static IHtmlContent RATouchPropertyBlockCurrencyReadOnlyFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null,
            string staticImageUrl = null)
        {
            return htmlHelper.TouchPropertyBlockNumericFor<TModel, object, TProperty>(null, expression, null, "currency",
                leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle,
                rightDisplayValueStyle, displayLabel, null, staticImageUrl, isReadOnly: true);
        }

        public static IHtmlContent RATouchPropertyBlockPINWithImageFor<TModel, TProperty1, TProperty2>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> imageUrlExpression,
            Expression<Func<TModel, TProperty2>> valueExpression,
            string touchDialogValueEditUrl = TOUCH_DIALOG_EDIT_URL_PIN,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null,
            string postEditUpdateScript = null)
        {
            return htmlHelper.TouchPropertyBlockNumericFor<TModel, TProperty1, TProperty2>(imageUrlExpression, valueExpression,
                touchDialogValueEditUrl, "pin", leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle,
                rightDisplayValueStyle, displayLabel, postEditUpdateScript);
        }

        public static IHtmlContent RATouchPropertyBlockCurrencyWithImageFor<TModel, TProperty1, TProperty2>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> imageUrlExpression,
            Expression<Func<TModel, TProperty2>> valueExpression,
            string touchDialogValueEditUrl = TOUCH_DIALOG_EDIT_URL_CURRENCY,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null,
            string postEditUpdateScript = null)
        {
            return htmlHelper.TouchPropertyBlockNumericFor<TModel, TProperty1, TProperty2>(imageUrlExpression, valueExpression,
                touchDialogValueEditUrl, "currency", leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle,
                rightDisplayValueStyle, displayLabel, postEditUpdateScript);
        }

        public static IHtmlContent RATouchPropertyBlockCurrencyReadOnlyWithImageFor<TModel, TProperty1, TProperty2>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> imageUrlExpression,
            Expression<Func<TModel, TProperty2>> valueExpression,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null)
        {
            return htmlHelper.TouchPropertyBlockNumericFor<TModel, TProperty1, TProperty2>(imageUrlExpression, valueExpression,
                null, "currency", leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle,
                displayLabel, isReadOnly: true);
        }

        private static IHtmlContent TouchPropertyBlockNumericFor<TModel, TProperty1, TProperty2>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> imageUrlExpression,
            Expression<Func<TModel, TProperty2>> valueExpression,
            string touchDialogValueEditUrl,
            string type,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null,
            string postEditUpdateScript = null,
            string staticImageUrl = null,
            bool isReadOnly = false)
        {
            var modelExplorer = htmlHelper.GetModelExplorer(valueExpression);
            var valueId = htmlHelper.IdFor(valueExpression);

            var content = new HtmlContentBuilder();
            content.AppendTouchPropertyBlockHtmlStart(type, null, touchDialogValueEditUrl?.Replace("{0}", valueId).Replace("{1}", postEditUpdateScript),
                String.IsNullOrEmpty(displayLabel) ? modelExplorer.GetPropertyDisplayName() : displayLabel,
                leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle,
                imageUrlExpression != null || !String.IsNullOrEmpty(staticImageUrl), isReadOnly);
            content.AppendHtml(htmlHelper.HiddenFor(valueExpression));
            if (imageUrlExpression != null)
                content.AppendHtml(htmlHelper.HiddenFor(imageUrlExpression));
            content.AppendHtml(GetTouchPropertyBlockHtmlEnd(valueId, modelExplorer.Model,
                imageUrlId: imageUrlExpression != null ? htmlHelper.IdFor(imageUrlExpression) : null,
                imageUrl: imageUrlExpression != null ? htmlHelper.GetModelExplorer(imageUrlExpression).Model.ToString() : staticImageUrl));

            return content;
        }

        private static void AppendTouchPropertyBlockHtmlStart(this HtmlContentBuilder content, string touchPropertyBlockTypePrimary, string touchPropertyBlockTypeSecondary,
            string touchDialogValueEditUrl, string displayLabel, string leftDisplayValueClass, string rightDisplayValueClass,
            string leftDisplayValueStyle, string rightDisplayValueStyle, bool hasImage, bool isReadOnly)
        {
            content.AppendFormat("<div class='ra-touchinput ra-touchinput-field' data-touchinput-type-primary='{0}' data-touchinput-type-secondary='{1}' ",
                touchPropertyBlockTypePrimary, touchPropertyBlockTypeSecondary);
            if (!String.IsNullOrEmpty(touchDialogValueEditUrl))
            {
                content.AppendFormat("ontouchend='raShowTouchDialog(event, this, \"{0}\"); event.preventDefault();'", touchDialogValueEditUrl);
                content.AppendFormat("onmouseup='raShowTouchDialog(event, this, \"{0}\");'", touchDialogValueEditUrl);
            }
            content.AppendHtml(">");
            content.AppendFormat("<div class='ra-touchinput-field-label'>{0}</div>", displayLabel);
            content.AppendFormat("<div class='ra-touchinput-field-value {0}'>", isReadOnly ? "ra-touchinput-field-value-readonly" : "");
            content.AppendHtml("<table class='ra-touchinput-field-value-table'><tr>");
            if (hasImage)
                content.AppendHtml("<td class='ra-touchinput-field-value-table-cell-image'></td>");
            content.AppendFormat("<td class='ra-touchinput-field-value-table-cell-left {0}' style='{1}'></td>",
                leftDisplayValueClass, leftDisplayValueStyle);
            content.AppendFormat("<td class='ra-touchinput-field-value-table-cell-right {0}' style='{1}'></td>",
                rightDisplayValueClass, rightDisplayValueStyle);
            content.AppendHtml("</tr></table>");
        }

        private static string GetTouchPropertyBlockHtmlEnd(string primaryValueId, object primaryValue, string primaryDisplayValueId = null,
            object primaryDisplayValue = null, string secondaryValueId = null, object secondaryValue = null, string secondaryDisplayValueId = null,
            object secondaryDisplayValue = null, string imageUrlId = null, object imageUrl = null)
        {
            return String.Format("</div><script>updateTouchPropertyBlockField('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');</script></div>",
                primaryValueId, primaryValue, primaryDisplayValueId, primaryDisplayValue,
                secondaryValueId, secondaryValue, secondaryDisplayValueId, secondaryDisplayValue,
                imageUrlId, imageUrl);
        }
    }
}
