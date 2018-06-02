using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Orbital7.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.AspNetCore.Mvc
{
    public enum RAEditorType
    {
        Dropdown,

        RadioButton,
    }

    public static partial class RAHtmlHelperExtensions
    {
        public static IHtmlContent RAEditorFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, SelectList selectList = null, 
            RAEditorType? editorTypeOverride = null, string selectListOptionLabel = null)
        {
            var modelExplorer = htmlHelper.GetModelExplorer(expression);
            return htmlHelper.GetEditorFor(expression, modelExplorer, htmlAttributes, selectList, editorTypeOverride, 
                false, selectListOptionLabel);
        }

        private static IHtmlContent GetEditorFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, ModelExplorer modelExplorer, object htmlAttributes = null,
            SelectList selectList = null, RAEditorType? editorTypeOverride = null, bool isToolbar = false, 
            string selectListOptionLabel = null)
        {
            var content = new HtmlContentBuilder();

            var attributes = HtmlHelperHelper.ToAttributesDictionary(htmlAttributes);
            attributes.AddOrInsertToExisting("class", "form-control");

            // Gather data.
            var nullableUnderlyingType = Nullable.GetUnderlyingType(modelExplorer.ModelType);
            var dataTypeAttribute = expression.GetAttribute<TModel, TProperty, DataTypeAttribute>();

            // Create a dynamic select list for enum types.
            if (selectList == null)
            {
                if (modelExplorer.ModelType.IsEnum)
                    selectList = SelectListHelper.EnumToSelectList(modelExplorer.ModelType);
                else if (nullableUnderlyingType != null && nullableUnderlyingType.IsEnum)
                    selectList = SelectListHelper.EnumToSelectList(nullableUnderlyingType);
            }

            if (selectList != null)
            {
                // Check for radio button specification.
                if (editorTypeOverride.HasValue && editorTypeOverride.Value == RAEditorType.RadioButton)
                {
                    foreach (var item in selectList)
                        content.AppendHtml(htmlHelper.RadioButtonFor(expression, Enum.Parse(modelExplorer.ModelType, item.Value), attributes));
                }
                // Else use a dropdown.
                else
                {
                    if (isToolbar)
                        attributes.AddOrAppendToExisting("class", "ra-toolbar-dropdown");
                    content.AppendHtml(htmlHelper.DropDownListFor(expression, selectList, selectListOptionLabel, attributes));
                }
            }
            else if (modelExplorer.ModelType == typeof(bool))
            {
                // Use a flip-switch-formatted checkbox.
                attributes.Add("data-toggle", "toggle");
                attributes.Add("data-on", "Yes");
                attributes.Add("data-off", "No");
                attributes.Add("data-size", "small");
                content.AppendHtml(htmlHelper.CheckBoxFor(expression as Expression<Func<TModel, bool>>, attributes));
            }
            else if (modelExplorer.ModelType == typeof(DateTime) || modelExplorer.ModelType == typeof(DateTime?))
            {
                var value = String.Empty;
                if (modelExplorer.ModelType == typeof(DateTime))
                    value = ((DateTime)modelExplorer.Model).FormatAsShortDate();
                else if (modelExplorer.ModelType == typeof(DateTime?))
                    value = ((DateTime?)modelExplorer.Model).FormatAsShortDate();

                attributes.AddOrAppendToExisting("class", "ra-behavior-datepicker");
                if (isToolbar)
                {
                    attributes.AddOrAppendToExisting("class", "ra-toolbar-datepicker");
                    //attributes.AddIfMissing("readonly", "true");
                }
                content.AppendHtml(htmlHelper.TextBoxFor(expression, value, attributes));
            }
            else if (modelExplorer.ModelType.IsNumeric())
            {
                attributes.AddIfMissing("type", "number");
                content.AppendHtml(htmlHelper.TextBoxFor(expression, attributes));
            }
            else if (dataTypeAttribute?.DataType == DataType.Password || expression.HasAttribute(typeof(PasswordAttribute)))
            {
                attributes.Add("value", modelExplorer.Model?.ToString());
                content.AppendHtml(htmlHelper.PasswordFor(expression, attributes));
            }
            else
            {
                if (modelExplorer.ModelType == typeof(IFormFile))
                {
                    content.AppendHtml(htmlHelper.FileFor(expression, attributes));
                }
                else if (dataTypeAttribute?.DataType == DataType.MultilineText)
                {
                    attributes.AddIfMissing("rows", 4);
                    content.AppendHtml(htmlHelper.TextAreaFor(expression, attributes));
                }
                else
                {
                    content.AppendHtml(htmlHelper.TextBoxFor(expression, attributes));
                }
            }

            return content;
        }
    }
}
