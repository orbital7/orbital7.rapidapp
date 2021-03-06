﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
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

        Number,

        Boolean,

        Date,

        PhoneNumber,

        Email,

        ZipCode,

        Text,

        TextMultiLine,
    }

    public static partial class RAHtmlHelperExtensions
    {
        public static IHtmlContent RATwoLineDateTime<TModel>(
            this IHtmlHelper<TModel> htmlHelper,
            DateTime? dateTimeUtc)
        {
            var content = new HtmlContentBuilder();

            content.AppendFormat("<div class='ra-nowrap'>{0}</div> <div class='ra-nowrap'>{1}</div>",
                dateTimeUtc.RAFormatAsShortDateForTimeZone(),
                dateTimeUtc.RAFormatAsShortTimeForTimeZone());

            return content;
        }

        public static IHtmlContent RACheckBoxFor<TModel>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression,
            string label,
            object htmlAttributes = null,
            string wrapperClass = null,
            string wrapperStyle = null,
            string stateClass = "ra-p-theme",
            string labelClass = null,
            string labelStyle = null)
        {
            var content = new HtmlContentBuilder();

            content.AppendFormat("<div class='pretty p-svg p-curve p-smooth {0}' style='{1}'>",
                wrapperClass, wrapperStyle);
            content.AppendHtml(htmlHelper.CheckBoxFor(expression, htmlAttributes));
            content.AppendFormat("<div class='state {0}'>", stateClass);
            content.AppendHtml("<svg class='svg svg-icon' viewBox='0 0 20 20'>");
            content.AppendHtml("<path d='M7.629,14.566c0.125,0.125,0.291,0.188,0.456,0.188c0.164,0,0.329-0.062,0.456-0.188l8.219-8.221c0.252-0.252,0.252-0.659,0-0.911c-0.252-0.252-0.659-0.252-0.911,0l-7.764,7.763L4.152,9.267c-0.252-0.251-0.66-0.251-0.911,0c-0.252,0.252-0.252,0.66,0,0.911L7.629,14.566z' " +
                "style='stroke: white;fill:white;'></path>");
            content.AppendHtml("</svg>");
            content.AppendFormat("<label class='{0}' style='{1}'>", labelClass, labelStyle);
            content.AppendHtml(label);
            content.AppendHtml("</label></div></div>");

            return content;
        }

        public static IHtmlContent RARadioButtonFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object value,
            string label,
            object htmlAttributes = null,
            string wrapperClass = null,
            string wrapperStyle = null,
            string stateClass = "p-primary-o",
            string labelClass = null,
            string labelStyle = null)
        {
            var content = new HtmlContentBuilder();

            content.AppendFormat("<div class='pretty p-default p-round p-smooth {0}' style='{1}'>",
                wrapperClass, wrapperStyle);
            content.AppendHtml(htmlHelper.RadioButtonFor(expression, value, htmlAttributes));
            content.AppendFormat("<div class='state {0}'>", stateClass);
            content.AppendFormat("<label class='{0}' style='{1}'>{2}</label>", labelClass, labelStyle, label);
            content.AppendHtml("</div></div>");

            return content;
        }

        public static IHtmlContent RAEditorFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, 
            object htmlAttributes = null, 
            SelectList selectList = null, 
            RAEditorType? editorTypeOverride = null, 
            string selectListOptionLabel = null)
        {
            var modelExplorer = htmlHelper.GetModelExplorer(expression);
            return htmlHelper.GetEditorFor(expression, modelExplorer, htmlAttributes, selectList, editorTypeOverride, 
                false, selectListOptionLabel);
        }

        private static IHtmlContent GetEditorFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, 
            ModelExplorer modelExplorer, 
            object htmlAttributes = null,
            SelectList selectList = null, 
            RAEditorType? editorTypeOverride = null, 
            bool isToolbar = false, 
            string selectListOptionLabel = null)
        {
            var content = new HtmlContentBuilder();

            var attributes = HtmlHelperHelper.ToAttributesDictionary(htmlAttributes);
            attributes.AddOrInsertToExisting("class", "form-control");

            // Gather data.
            var nullableUnderlyingType = Nullable.GetUnderlyingType(modelExplorer.ModelType);
            var dataTypeAttribute = expression.GetAttribute<TModel, TProperty, DataTypeAttribute>();
            var zipCodeAttribute = expression.GetAttribute<TModel, TProperty, ZipCodeAttribute>();

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
                    {
                        var enumValue = Enum.Parse(modelExplorer.ModelType, item.Value);
                        content.AppendHtml(htmlHelper.RARadioButtonFor(expression, enumValue,
                            ((Enum)enumValue).ToDisplayString(), attributes));
                    }
                }
                // Else use a dropdown.
                else
                {
                    if (isToolbar)
                        attributes.AddOrAppendToExisting("class", "ra-toolbar-dropdown");
                    content.AppendHtml(htmlHelper.DropDownListFor(expression, selectList, selectListOptionLabel, attributes));
                }
            }
            else if (modelExplorer.ModelType == typeof(bool) || 
                (editorTypeOverride.HasValue && editorTypeOverride.Value == RAEditorType.Boolean))
            {
                const string TRUE = "Yes";
                const string FALSE = "No";
                content.AppendHtml("<div class='form=group'><span class='switch switch-sm'>");
                attributes.AddIfMissing("data-checked", TRUE);
                attributes.AddIfMissing("data-unchecked", FALSE);
                attributes.AddOrAppendToExisting("onclick", "raUpdateFlipswitchLabel(this);");
                content.AppendHtml(htmlHelper.CheckBoxFor(expression as Expression<Func<TModel, bool>>, attributes));
                content.AppendHtml(htmlHelper.LabelFor(expression,
                    (bool)modelExplorer.Model ? attributes["data-checked"].ToString() : attributes["data-unchecked"].ToString(), null));
                content.AppendHtml("</span></div>");
            }
            else if (modelExplorer.ModelType == typeof(DateTime) || modelExplorer.ModelType == typeof(DateTime?) ||
                (editorTypeOverride.HasValue && editorTypeOverride.Value == RAEditorType.Date))
            {
                var value = String.Empty;
                if (modelExplorer.ModelType == typeof(DateTime))
                    value = ((DateTime)modelExplorer.Model).ToString("yyyy-MM-dd");
                else if (modelExplorer.ModelType == typeof(DateTime?))
                    value = ((DateTime?)modelExplorer.Model)?.ToString("yyyy-MM-dd");
                attributes.Add("Value", value);
                //attributes.AddOrAppendToExisting("class", "ra-behavior-datepicker ra-clickable");
                if (isToolbar)
                    attributes.AddOrAppendToExisting("class", "ra-toolbar-datepicker");
                //attributes.AddIfMissing("readonly", "true");
                attributes.AddIfMissing("type", "date");

                // TODO: We really just need to format the date property in TextBoxFor, 
                // but supplying the format doesn't appear to be working.
                var temp = htmlHelper.TextBoxFor(expression, attributes).GetString();
                var temp2 = temp.FindFirstBetween("value=\"", "\"");
                if (temp2.HasText() && value.HasText())
                    temp = temp.Replace(temp2, value);
                content.AppendHtml(temp);
            }
            else if (modelExplorer.ModelType.IsNumeric() || 
                (editorTypeOverride.HasValue && editorTypeOverride == RAEditorType.Number))
            {
                attributes.AddIfMissing("type", "number");
                if (modelExplorer.ModelType == typeof(decimal) || modelExplorer.ModelType == typeof(decimal?))
                {
                    attributes.AddIfMissing("placeholder", "0.00");
                    attributes.AddIfMissing("min", "0");
                    attributes.AddIfMissing("oninput", "onInputCurrency(event)");
                }
                content.AppendHtml(htmlHelper.TextBoxFor(expression, attributes));
            }
            else if (dataTypeAttribute?.DataType == DataType.PostalCode || zipCodeAttribute != null ||
                (editorTypeOverride.HasValue && editorTypeOverride.Value == RAEditorType.ZipCode))
            {
                attributes.AddIfMissing("inputmode", "numeric");
                attributes.AddIfMissing("pattern", "[0-9]*");
                content.AppendHtml(htmlHelper.TextBoxFor(expression, attributes));
            }
            else if (dataTypeAttribute?.DataType == DataType.PhoneNumber || expression.HasAttribute(typeof(PhoneAttribute)) 
                || (editorTypeOverride.HasValue && editorTypeOverride.Value == RAEditorType.PhoneNumber))
            {
                attributes.AddIfMissing("inputmode", "numeric");
                attributes.AddIfMissing("pattern", "[0-9]*");
                content.AppendHtml(htmlHelper.TextBoxFor(expression, attributes));
            }
            else if (dataTypeAttribute?.DataType == DataType.EmailAddress || expression.HasAttribute(typeof(EmailAddressAttribute)) ||
                (editorTypeOverride.HasValue && editorTypeOverride.Value == RAEditorType.Email))
            {
                attributes.AddIfMissing("type", "email");
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
                else if (dataTypeAttribute?.DataType == DataType.MultilineText ||
                    (editorTypeOverride.HasValue && editorTypeOverride.Value == RAEditorType.TextMultiLine))
                {
                    //attributes.AddOrAppendToExisting("class", "ra-container-scrollable-y");   // JVE: Does not appear to work correctly.
                    attributes.AddIfMissing("rows", 4);
                    content.AppendHtml(htmlHelper.TextAreaFor(expression, attributes));
                }
                else
                {
                    if (editorTypeOverride.HasValue && editorTypeOverride.Value == RAEditorType.Number)
                        attributes.AddIfMissing("type", "number");

                    content.AppendHtml(htmlHelper.TextBoxFor(expression, attributes));
                }
            }

            return content;
        }

        public static string GetString(
            this IHtmlContent content)
        {
            using (var writer = new System.IO.StringWriter())
            {
                content.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}
