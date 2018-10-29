using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Orbital7.RapidApp.Models;
using Orbital7.Extensions.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static partial class RAHtmlHelperExtensions
    {
        public const string LOADING = "Loading...";

        public static TimeZoneInfo TimeZone { get; set; }
        
        public static string RAFormatAsShortDateTimeForTimeZone(this DateTime? value)
        {
            if (RAHtmlHelperExtensions.TimeZone != null)
                return value.UTCToTimeZone(RAHtmlHelperExtensions.TimeZone).FormatAsShortDateTime();
            else
                return value.FormatAsShortDateTime();
        }

        public static string RAFormatAsShortDateTimeForTimeZone(this DateTime value)
        {
            return RAFormatAsShortDateTimeForTimeZone((DateTime?)value);
        }

        public static string RAFormatAsShortDateForTimeZone(this DateTime? value)
        {
            if (RAHtmlHelperExtensions.TimeZone != null)
                return value.UTCToTimeZone(RAHtmlHelperExtensions.TimeZone).FormatAsShortDate();
            else
                return value.FormatAsShortDate();
        }

        public static string RAFormatAsShortDateForTimeZone(this DateTime value)
        {
            return RAFormatAsShortDateForTimeZone((DateTime?)value);
        }

        public static string RAFormatAsShortTimeForTimeZone(this DateTime? value)
        {
            if (RAHtmlHelperExtensions.TimeZone != null)
                return value.UTCToTimeZone(RAHtmlHelperExtensions.TimeZone).FormatAsShortTime();
            else
                return value.FormatAsShortTime();
        }

        public static string RAFormatAsShortTimeForTimeZone(this DateTime value)
        {
            return RAFormatAsShortTimeForTimeZone((DateTime?)value);
        }

        private static IHtmlContent ToButton(
            this IDictionary<string, object> attributes, 
            string text)
        {
            var tagBuilder = new TagBuilder("button");
            tagBuilder.MergeAttributes(attributes);
            tagBuilder.InnerHtml.AppendHtml(text);

            return tagBuilder;
        }

        public static IHtmlContent RADisplayValueFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            string valueNull = "-", 
            string valueUrl = null)
        {
            var content = new HtmlContentBuilder();

            var modelExplorer = htmlHelper.GetModelExplorer(expression);
            content.AppendHtml(htmlHelper.GetValueDisplayHtml(modelExplorer.Model, 
                valueNull, valueUrl, null, modelExplorer.Metadata.PropertyName));

            return content;
        }

        public static IHtmlContent RADisplayValue<TModel>(
            this IHtmlHelper<TModel> htmlHelper,
            object value,
            string valueNull = "-",
            string valueUrl = null,
            string fieldName = null)
        {
            var content = new HtmlContentBuilder();

            content.AppendHtml(htmlHelper.GetValueDisplayHtml(value,
                valueNull, valueUrl, null, fieldName));

            return content;
        }

        private static string GetValueDisplayHtml<TModel>(this IHtmlHelper<TModel> htmlHelper, object value,
            string valueNull = "-", string valueUrl = null, string valueOverride = null, string fieldName = null)
        {
            var html = valueOverride;

            if (String.IsNullOrEmpty(html))
            {
                if (value is IList)
                {
                    var items = (from object x in (IList)value
                                 where x != null
                                 orderby x.ToString()
                                 select x).ToList();

                    foreach (object item in items)
                    {
                        if (item is ITagObject)
                            html += String.Format("<span class='badge badge-pill badge-info ra-badge'>{0}</span>", ((ITagObject)item).TagName);
                        else
                            html += String.Format("<div>{0}</div>", GetValueDisplayHtml(htmlHelper, item));
                    }
                }
                else
                {
                    html = (value is Guid ? String.Empty : value?.ToString());
                }
            }             

            if (String.IsNullOrEmpty(html))
            {
                html = valueNull;
            }
            else
            {
                if (value is DateTime)
                {
                    if (!String.IsNullOrEmpty(fieldName) && fieldName.ToUpper().EndsWith("UTC"))
                        html = Convert.ToDateTime(html).RAFormatAsShortDateTimeForTimeZone();
                    else
                        html = Convert.ToDateTime(html).FormatAsShortDate();
                    html = "<span class='ra-nowrap'>" + html + "</span>";
                }
                else if (value is TimeSpan)
                {
                    html = ((TimeSpan)value).FormatTimeSpan();
                }
                else if (value is bool)
                {
                    html = Convert.ToBoolean(html).ToYesNo();
                }
                else if (value is decimal)
                {
                    html = htmlHelper.DisplayCurrency(Convert.ToDecimal(html), true).ToString();
                }
                else if (value is double && !String.IsNullOrEmpty(fieldName) && fieldName.ToUpper().EndsWith("PERCENTAGE"))
                {
                    html = String.Format("{0}%", Convert.ToDouble(value).ToString("0.00"));
                }
                else if (value is Enum)
                {
                    html = ((Enum)value).ToDisplayString();
                }
                else if (value is IHtmlContent)
                {
                    using (var writer = new StringWriter())
                    {
                        ((IHtmlContent)value).WriteTo(writer, HtmlEncoder.Default);
                        html = writer.ToString();
                    }
                }
                else if (value is string && !String.IsNullOrEmpty(fieldName) && fieldName.ToUpper().EndsWith("PASSWORD"))
                {
                    html = value?.ToString().Mask(maskChar: "•");
                }
                else if (value is string && !String.IsNullOrEmpty(fieldName) && fieldName.ToUpper().EndsWith("PHONENUMBER"))
                {
                    html = value?.ToString().FormatAsPhoneNumber();
                }

                if (!String.IsNullOrEmpty(valueUrl))
                    html = String.Format("<a href='{0}'>{1}</a>", valueUrl, html);
            }

            return html;
        }

        private static string GetValueCellAlignmentClass(object value)
        {
            string classname = null;

            if (value != null)
            {
                if (value is int || value is decimal || value is double)
                    classname = "ra-align-right";
            }

            return classname;
        }

        public static IHtmlContent RAAjaxContentSection(this IHtmlHelper htmlHelper, 
            string contentKey, 
            string contentUrl = null, 
            string contentUrlScript = null, 
            string contentClass = "ra-fullheight", 
            string contentStyle = null, 
            string postUpdateScript = null)
        {
            var content = new HtmlContentBuilder();

            content.AppendFormat("<div id=\"{0}AjaxContent\" class=\"{1}\" style=\"{2}\" data-content-url=\"{3}\" data-content-url-script=\"{4}\"></div>", 
                contentKey, contentClass, contentStyle, contentUrl, contentUrlScript);

            content.AppendFormat("<script>raUpdateAjaxContentSection('{0}');{1}</script>", contentKey, postUpdateScript);

            return content;
        }

        public static TagCloser RABeginAjaxContentSection(this IHtmlHelper htmlHelper,
            string contentKey,
            string contentUrl = null,
            string contentUrlScript = null,
            string contentClass = "ra-fullheight",
            string contentStyle = null)
        {
            var content = new HtmlContentBuilder();
            content.AppendFormat("<div id=\"{0}AjaxContent\" class=\"{1}\" style=\"{2}\" data-content-url=\"{3}\" data-content-url-script=\"{4}\">",
                contentKey, contentClass, contentStyle, contentUrl, contentUrlScript);
            htmlHelper.ViewContext.Writer.Write(content);
            return new TagCloser(htmlHelper, "</div");
        }

        public static IHtmlContent RAContentFrame(
            this IHtmlHelper htmlHelper,
            string contentUrl,
            string containerClass = "ra-fullheight",
            string containerStyle = null,
            string frameClass = null,
            string frameStyle = null,
            bool showLoadingFullHeight = true,
            bool printFrameOnLoad = false,
            bool showPrintButton = false)
        {
            return htmlHelper.RAContentFrame(new RAContentFrameModel()
            {
                ContentUrl = contentUrl,
                ContainerClass = containerClass,
                ContainerStyle = containerStyle,
                FrameClass = frameClass,
                FrameStyle = frameStyle,
                ShowLoadingFullHeight = showLoadingFullHeight,
                PrintFrameOnLoad = printFrameOnLoad,
                ShowPrintButton = showPrintButton,
            });
        }

        public static IHtmlContent RAContentFrame(
            this IHtmlHelper htmlHelper,
            RAContentFrameModel contentFrame)
        {
            return htmlHelper.Partial("~/Views/RA/ContentFrame.cshtml", contentFrame);
        }

        public static string RACreatePartialViewContentFrameUrl(this IHtmlHelper htmlHelper, 
            string contentUrl,
            string containerClass = "ra-fullheight",
            string containerStyle = null,
            string frameClass = null,
            string frameStyle = null,
            bool showLoadingFullHeight = true,
            bool printFrameOnLoad = false, 
            bool showPrintButton = false)
        {
            var sb = new StringBuilder();
            sb.Append("/RA/PartialViewContentFrame");
            sb.AppendFormat("?contentUrl={0}", UrlEncoder.Default.Encode(contentUrl));
            sb.AppendFormat("&containerClass={0}", containerClass);
            sb.AppendFormat("&containerStyle={0}", containerStyle);
            sb.AppendFormat("&frameClass={0}", frameClass);
            sb.AppendFormat("&frameStyle={0}", frameStyle);
            sb.AppendFormat("&showLoadingFullHeight={0}", showLoadingFullHeight);
            sb.AppendFormat("&printFrameOnLoad={0}", printFrameOnLoad);
            sb.AppendFormat("&showPrintButton={0}", showPrintButton);
            return sb.ToString();
        }
    }
}
