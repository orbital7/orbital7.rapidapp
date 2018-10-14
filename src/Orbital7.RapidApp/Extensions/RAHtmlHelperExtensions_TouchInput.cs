using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Orbital7.RapidApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static partial class RAHtmlHelperExtensions
    {
        public const string TOUCH_DIALOG_EDIT_URL_PIN = @"/RATouch/RADialogTouchPad?valueId={0}&mode=PIN&postEditUpdateScript={1}";

        public const string TOUCH_DIALOG_EDIT_URL_CURRENCY = @"/RATouch/RADialogTouchPad?valueId={0}&mode=Currency&postEditUpdateScript={1}";

        public const string TOUCH_DIALOG_EDIT_URL_INTEGER = @"/RATouch/RADialogTouchPad?valueId={0}&mode=Integer&postEditUpdateScript={1}";

        public const string TOUCH_DIALOG_EDIT_URL_DOUBLE = @"/RATouch/RADialogTouchPad?valueId={0}&mode=Double&postEditUpdateScript={1}";

        public const string TOUCH_DIALOG_EDIT_URL_PHONENUMBER = @"/RATouch/RADialogTouchPad?valueId={0}&mode=PhoneNumber&postEditUpdateScript={1}";

        public static IHtmlContent RATouchPadFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> touchPadExpression) 
            where TProperty : RATouchPadModel
        {
            return htmlHelper.EditorFor(touchPadExpression, "~/Views/RATouch/EditorTemplates/RATouchPadModel.cshtml");
        }

        public static IHtmlContent RATouchInputFor<TModel, TProperty1>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> primaryValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string onMouseDownMethod = "raShowTouchDialog")
        {
            return htmlHelper.TouchInputFor<TModel, object, TProperty1, object, object, object>(null, primaryValueExpression, 
                null, null, null, touchDialogValueEditUrl, leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, 
                rightDisplayValueStyle, onMouseDownMethod: onMouseDownMethod);
        }

        public static IHtmlContent RATouchInputFor<TModel, TProperty1, TProperty2>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> primaryValueExpression,
            Expression<Func<TModel, TProperty2>> primaryDisplayValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            bool readOnly = false,
            string onMouseDownMethod = "raShowTouchDialog")
        {
            return htmlHelper.TouchInputFor<TModel, object, TProperty1, TProperty2, object, object>(null, primaryValueExpression,
                primaryDisplayValueExpression, null, null, touchDialogValueEditUrl, leftDisplayValueClass, rightDisplayValueClass, 
                leftDisplayValueStyle, rightDisplayValueStyle, readOnly, onMouseDownMethod: onMouseDownMethod);
        }

        public static IHtmlContent RATouchInputFor<TModel, TProperty1, TProperty2, TProperty3>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> primaryValueExpression,
            Expression<Func<TModel, TProperty2>> primaryDisplayValueExpression,
            Expression<Func<TModel, TProperty3>> secondaryValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string onMouseDownMethod = "raShowTouchDialog")
        {
            return htmlHelper.TouchInputFor<TModel, object, TProperty1, TProperty2, TProperty3, object>(null, primaryValueExpression,
                primaryDisplayValueExpression, secondaryValueExpression, null, touchDialogValueEditUrl, leftDisplayValueClass, 
                rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle, onMouseDownMethod: onMouseDownMethod);
        }

        public static IHtmlContent RATouchInputFor<TModel, TProperty1, TProperty2, TProperty3, TProperty4>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> primaryValueExpression,
            Expression<Func<TModel, TProperty2>> primaryDisplayValueExpression,
            Expression<Func<TModel, TProperty3>> secondaryValueExpression,
            Expression<Func<TModel, TProperty4>> secondaryDisplayValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string onMouseDownMethod = "raShowTouchDialog")
        {
            return htmlHelper.TouchInputFor<TModel, object, TProperty1, TProperty2, TProperty3, TProperty4>(null, primaryValueExpression,
                primaryDisplayValueExpression, secondaryValueExpression, secondaryDisplayValueExpression, touchDialogValueEditUrl,
                leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle, 
                onMouseDownMethod: onMouseDownMethod);
        }

        public static IHtmlContent RATouchInputWithImageFor<TModel, TProperty1, TProperty2>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> imageUrlExpression,
            Expression<Func<TModel, TProperty2>> primaryValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string onMouseDownMethod = "raShowTouchDialog")
        {
            return htmlHelper.TouchInputFor<TModel, TProperty1, TProperty2, object, object, object>(imageUrlExpression, 
                primaryValueExpression, null, null, null, touchDialogValueEditUrl, leftDisplayValueClass, rightDisplayValueClass, 
                leftDisplayValueStyle, rightDisplayValueStyle, onMouseDownMethod: onMouseDownMethod);
        }

        public static IHtmlContent RATouchInputWithImageFor<TModel,TProperty1, TProperty2, TProperty3>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> imageUrlExpression,
            Expression<Func<TModel, TProperty2>> primaryValueExpression,
            Expression<Func<TModel, TProperty3>> primaryDisplayValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string onMouseDownMethod = "raShowTouchDialog")
        {
            return htmlHelper.TouchInputFor<TModel, TProperty1, TProperty2, TProperty3, object, object>(imageUrlExpression, primaryValueExpression,
                primaryDisplayValueExpression, null, null, touchDialogValueEditUrl, leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, 
                rightDisplayValueStyle, onMouseDownMethod: onMouseDownMethod);
        }

        public static IHtmlContent RATouchInputReadOnlyWithImageFor<TModel, TProperty1, TProperty2, TProperty3>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> imageUrlExpression,
            Expression<Func<TModel, TProperty2>> primaryValueExpression,
            Expression<Func<TModel, TProperty3>> primaryDisplayValueExpression,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null)
        {
            return htmlHelper.TouchInputFor<TModel, TProperty1, TProperty2, TProperty3, object, object>(imageUrlExpression, primaryValueExpression,
                primaryDisplayValueExpression, null, null, null, leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle,
                rightDisplayValueStyle, true);
        }

        public static IHtmlContent RATouchInputWithImageFor<TModel, TProperty1, TProperty2, TProperty3, TProperty4>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> imageUrlExpression,
            Expression<Func<TModel, TProperty2>> primaryValueExpression,
            Expression<Func<TModel, TProperty3>> primaryDisplayValueExpression,
            Expression<Func<TModel, TProperty4>> secondaryValueExpression,
            string touchDialogValueEditUrl,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string onMouseDownMethod = "raShowTouchDialog")
        {
            return htmlHelper.TouchInputFor<TModel, TProperty1, TProperty2, TProperty3, TProperty4, object>(imageUrlExpression, primaryValueExpression,
                primaryDisplayValueExpression, secondaryValueExpression, null, touchDialogValueEditUrl, leftDisplayValueClass, rightDisplayValueClass, 
                leftDisplayValueStyle, rightDisplayValueStyle, onMouseDownMethod: onMouseDownMethod);
        }

        public static IHtmlContent RATouchInputWithImageFor<TModel, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5>(this IHtmlHelper<TModel> htmlHelper,
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
            string onMouseDownMethod = "raShowTouchDialog")
        {
            return htmlHelper.TouchInputFor<TModel, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5>(imageUrlExpression, primaryValueExpression,
                primaryDisplayValueExpression, secondaryValueExpression, secondaryDisplayValueExpression, touchDialogValueEditUrl,
                leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle, onMouseDownMethod: onMouseDownMethod);
        }

        private static IHtmlContent TouchInputFor<TModel, TProperty1, TProperty2, TProperty3, TProperty4, TProperty5>(this IHtmlHelper<TModel> htmlHelper,
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
            bool readOnly = false,
            string onMouseDownMethod = "raShowTouchDialog")
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

            var primaryValueId = htmlHelper.IdFor(primaryValueExpression);
            var primaryDisplayValueId = primaryDisplayValueExpression != null ? 
                htmlHelper.IdFor(primaryDisplayValueExpression) : "";
            var secondaryValueId = secondaryValueExpression != null ?
                htmlHelper.IdFor(secondaryValueExpression) : "";
            var secondaryDisplayValueId = secondaryDisplayValueExpression != null ? 
                htmlHelper.IdFor(secondaryDisplayValueExpression) : "";
            var imageUrlId = imageUrlExpression != null ? htmlHelper.IdFor(imageUrlExpression) : "";

            var content = new HtmlContentBuilder();
            content.AppendTouchInputHtmlStart(
                GetTouchInputType(primaryValueModelExplorer), 
                GetTouchInputType(secondaryValueModelExplorer), 
                touchDialogValueEditUrl,
                primaryValueId, 
                secondaryValueId, 
                imageUrlId, 
                null, 
                null,
                leftDisplayValueClass, 
                rightDisplayValueClass, 
                leftDisplayValueStyle, 
                rightDisplayValueStyle,
                !String.IsNullOrEmpty(imageUrlId),
                readOnly,
                null,
                null,
                onMouseDownMethod);
            content.AppendHtml(htmlHelper.HiddenFor(primaryValueExpression));

            if (primaryDisplayValueExpression != null)
                content.AppendHtml(htmlHelper.HiddenFor(primaryDisplayValueExpression));
            if (secondaryValueExpression != null)
                content.AppendHtml(htmlHelper.HiddenFor(secondaryValueExpression));
            if (secondaryDisplayValueExpression != null)
                content.AppendHtml(htmlHelper.HiddenFor(secondaryDisplayValueExpression));
            if (imageUrlExpression != null)
                content.AppendHtml(htmlHelper.HiddenFor(imageUrlExpression));

            content.AppendHtml(GetTouchInputHtmlEnd(
                primaryValueId, 
                primaryValueModelExplorer.Model,
                primaryDisplayValueId, 
                primaryDisplayValueModelExplorer?.Model,
                secondaryValueId, 
                secondaryValueModelExplorer?.Model,
                secondaryDisplayValueId, 
                secondaryDisplayValueModelExplorer?.Model, 
                imageUrlId, 
                imageUrlModelExplorer?.Model));

            return content;
        }

        private static string GetTouchInputType(ModelExplorer modelExplorer)
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

        public static IHtmlContent RATouchInputPINFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, 
            string touchDialogValueEditUrl = TOUCH_DIALOG_EDIT_URL_PIN,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string postEditUpdateScript = null,
            string staticImageUrl = null)
        {
            return htmlHelper.TouchInputNumericFor<TModel, object, TProperty>(null, expression, touchDialogValueEditUrl, 
                "pin", null, null, leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle, 
                postEditUpdateScript, staticImageUrl);
        }

        public static IHtmlContent RATouchInputCurrencyFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, 
            string touchDialogValueEditUrl = TOUCH_DIALOG_EDIT_URL_CURRENCY,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string postEditUpdateScript = null, 
            string staticImageUrl = null)
        {
            return htmlHelper.TouchInputNumericFor<TModel, object, TProperty>(null, expression, touchDialogValueEditUrl, 
                "currency", null, null, leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle, 
                postEditUpdateScript, staticImageUrl);
        }

        public static IHtmlContent RATouchInputIntegerFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            string touchDialogValueEditUrl = TOUCH_DIALOG_EDIT_URL_INTEGER,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string postEditUpdateScript = null,
            string staticImageUrl = null)
        {
            return htmlHelper.TouchInputNumericFor<TModel, object, TProperty>(null, expression, touchDialogValueEditUrl,
                "integer", null, null, leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle,
                postEditUpdateScript, staticImageUrl);
        }

        public static IHtmlContent RATouchInputDoubleFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            string touchDialogValueEditUrl = TOUCH_DIALOG_EDIT_URL_DOUBLE,
            string touchInputClass = null,
            string touchInputStyle = null,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string postEditUpdateScript = null,
            string staticImageUrl = null,
            double? minValue = null,
            double? maxValue = null)
        {
            return htmlHelper.TouchInputNumericFor<TModel, object, TProperty>(null, expression, touchDialogValueEditUrl,
                "double", touchInputClass, touchInputStyle, leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle,
                postEditUpdateScript, staticImageUrl, false, minValue, maxValue);
        }

        public static IHtmlContent RATouchInputPhoneNumberFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            string touchDialogValueEditUrl = TOUCH_DIALOG_EDIT_URL_PHONENUMBER,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string postEditUpdateScript = null,
            string staticImageUrl = null)
        {
            return htmlHelper.TouchInputNumericFor<TModel, object, TProperty>(null, expression, touchDialogValueEditUrl,
                "phone", null, null, leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle,
                postEditUpdateScript, staticImageUrl);
        }

        public static IHtmlContent RATouchInputIntegerReadOnlyFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string staticImageUrl = null)
        {
            return htmlHelper.TouchInputNumericFor<TModel, object, TProperty>(null, expression, null, "integer", null, null,
                leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle,
                rightDisplayValueStyle, null, staticImageUrl, isReadOnly: true);
        }

        public static IHtmlContent RATouchInputDoubleReadOnlyFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string staticImageUrl = null)
        {
            return htmlHelper.TouchInputNumericFor<TModel, object, TProperty>(null, expression, null, "double", null, null,
                leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle,
                rightDisplayValueStyle, null, staticImageUrl, isReadOnly: true);
        }

        public static IHtmlContent RATouchInputCurrencyReadOnlyFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string staticImageUrl = null)
        {
            return htmlHelper.TouchInputNumericFor<TModel, object, TProperty>(null, expression, null, "currency", null, null, 
                leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, 
                rightDisplayValueStyle, null, staticImageUrl, isReadOnly: true);
        }

        public static IHtmlContent RATouchInputPINWithImageFor<TModel, TProperty1, TProperty2>(this IHtmlHelper<TModel> htmlHelper,
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
            return htmlHelper.TouchInputNumericFor<TModel, TProperty1, TProperty2>(imageUrlExpression, valueExpression, 
                touchDialogValueEditUrl, "pin", null, null, leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, 
                rightDisplayValueStyle, displayLabel, postEditUpdateScript);
        }

        public static IHtmlContent RATouchInputCurrencyWithImageFor<TModel, TProperty1, TProperty2>(this IHtmlHelper<TModel> htmlHelper,
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
            return htmlHelper.TouchInputNumericFor<TModel, TProperty1, TProperty2>(imageUrlExpression, valueExpression, 
                touchDialogValueEditUrl, "currency", null, null, leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, 
                rightDisplayValueStyle, displayLabel, postEditUpdateScript);
        }

        public static IHtmlContent RATouchInputCurrencyReadOnlyWithImageFor<TModel, TProperty1, TProperty2>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> imageUrlExpression,
            Expression<Func<TModel, TProperty2>> valueExpression,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string displayLabel = null)
        {
            return htmlHelper.TouchInputNumericFor<TModel, TProperty1, TProperty2>(imageUrlExpression, valueExpression, 
                null, "currency", null, null, leftDisplayValueClass, rightDisplayValueClass, leftDisplayValueStyle, rightDisplayValueStyle, 
                displayLabel, isReadOnly: true);
        }

        private static IHtmlContent TouchInputNumericFor<TModel, TProperty1, TProperty2>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty1>> imageUrlExpression,
            Expression<Func<TModel, TProperty2>> valueExpression, 
            string touchDialogValueEditUrl, 
            string type,
            string touchInputClass = null,
            string touchInputStyle = null,
            string leftDisplayValueClass = null,
            string rightDisplayValueClass = null,
            string leftDisplayValueStyle = null,
            string rightDisplayValueStyle = null,
            string postEditUpdateScript = null, 
            string staticImageUrl = null,
            bool isReadOnly = false,
            double? minValue = null,
            double? maxValue = null,
            string onMouseDownMethod = "raShowTouchDialog")
        {
            var modelExplorer = htmlHelper.GetModelExplorer(valueExpression);
            var valueId = htmlHelper.IdFor(valueExpression);
            var imageUrlId = imageUrlExpression != null ? htmlHelper.IdFor(imageUrlExpression) : null;

            var content = new HtmlContentBuilder();

            content.AppendTouchInputHtmlStart(
                type, 
                null, 
                touchDialogValueEditUrl?.Replace("{0}", valueId).Replace("{1}", postEditUpdateScript),
                null,
                valueId,
                imageUrlId,
                touchInputClass,
                touchInputStyle,
                leftDisplayValueClass, 
                rightDisplayValueClass, 
                leftDisplayValueStyle, 
                rightDisplayValueStyle, 
                imageUrlExpression != null || !String.IsNullOrEmpty(staticImageUrl), 
                isReadOnly,
                minValue,
                maxValue,
                onMouseDownMethod);

            content.AppendHtml(htmlHelper.HiddenFor(valueExpression));
            if (imageUrlExpression != null)
                content.AppendHtml(htmlHelper.HiddenFor(imageUrlExpression));

            content.AppendHtml(GetTouchInputHtmlEnd(
                valueId, 
                modelExplorer.Model,
                imageUrlId: imageUrlId,
                imageUrl: imageUrlExpression != null ? htmlHelper.GetModelExplorer(imageUrlExpression).Model.ToString() : staticImageUrl));

            return content;
        }

        private static void AppendTouchInputHtmlStart(
            this HtmlContentBuilder content, 
            string touchInputTypePrimary, 
            string touchInputTypeSecondary,
            string touchDialogValueEditUrl, 
            string leftValueId,
            string rightValueId,
            string imageUrlId,
            string touchInputClass,
            string touchInputStyle,
            string leftDisplayValueClass, 
            string rightDisplayValueClass, 
            string leftDisplayValueStyle, 
            string rightDisplayValueStyle, 
            bool hasImage,
            bool isReadOnly,
            double? minValue,
            double? maxValue,
            string onMouseDownMethod)
        {
            content.AppendFormat("<div class='ra-touchinput ra-touchinput-field {0}' style='{1}'", touchInputClass, touchInputStyle);
            if (!String.IsNullOrEmpty(touchDialogValueEditUrl) || !isReadOnly)
                content.AppendFormat("onmousedown='{0}(event, this, \"{1}\");'",
                    onMouseDownMethod, touchDialogValueEditUrl);
            content.AppendHtml(">");
            content.AppendFormat("<div class='ra-touchinput-field-value {0}' data-touchinput-type-primary='{1}' data-touchinput-type-secondary='{2}' {3} {4}>", 
                isReadOnly ? "ra-touchinput-field-value-readonly" : "", 
                touchInputTypePrimary, 
                touchInputTypeSecondary,
                minValue.HasValue ? "data-touchinput-minvalue=" + minValue.Value : "",
                maxValue.HasValue ? "data-touchinput-maxvalue=" + maxValue.Value : "");
            content.AppendHtml("<table class='ra-touchinput-field-value-table'><tr>");
            content.AppendFormat("<td id='{0}_ImageCell' class='ra-touchinput-field-value-table-cell-image'></td>", imageUrlId);
            content.AppendFormat("<td id='{0}_LeftCell' class='ra-touchinput-field-value-table-cell-left {1}' style='{2}'></td>", 
                leftValueId, leftDisplayValueClass, leftDisplayValueStyle);
            content.AppendFormat("<td id='{0}_RightCell' class='ra-touchinput-field-value-table-cell-right {1}' style='{2}'></td>", 
                rightValueId, rightDisplayValueClass, rightDisplayValueStyle);
            content.AppendHtml("</tr></table>");
        }

        private static string GetTouchInputHtmlEnd(string primaryValueId, object primaryValue, string primaryDisplayValueId = null, 
            object primaryDisplayValue = null, string secondaryValueId = null, object secondaryValue = null, string secondaryDisplayValueId = null, 
            object secondaryDisplayValue = null, string imageUrlId = null, object imageUrl = null)
        {
            return String.Format("</div><script>raUpdateTouchInputField('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');</script></div>", 
                primaryValueId, primaryValue, primaryDisplayValueId, primaryDisplayValue,
                secondaryValueId, secondaryValue, secondaryDisplayValueId, secondaryDisplayValue,
                imageUrlId, imageUrl);
        }
    }
}
