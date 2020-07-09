using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Orbital7.RapidApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public enum ContainerScrolling
    {
        None,

        X,

        Y,

        Both,
    }

    public static partial class RAHtmlHelperExtensions
    {
        public static IHtmlContent RAPagePanel<TModel>(
            this IHtmlHelper<TModel> htmlHelper,
            string contentUrl = null,
            string containerClass = "ra-container ra-container-vertical",
            string containerStyle = null)
        {
            var content = new HtmlContentBuilder();

            content.AppendFormat(
                "<div id='ra-pagePanel' data-content-url='{0}' class='{1}' style='{2}'></div>",
                contentUrl,
                containerClass,
                containerStyle);

            if (!string.IsNullOrEmpty(contentUrl))
                content.AppendHtml("<script>$(document).ready(function() { raRefreshPagePanel(); });</script>");

            return content;
        }

        public static TagCloser RABeginContainer(
            this IHtmlHelper htmlHelper,
            ContainerScrolling scrolling = ContainerScrolling.None,
            string containerClass = "ra-container",
            string containerStyle = null)
        {
            var content = new HtmlContentBuilder();
            content.AppendFormat("<div class='{0} {1}' style='{2}'>",
                containerClass,
                GetScrollingClass(scrolling), 
                containerStyle);

            htmlHelper.ViewContext.Writer.Write(content);
            return new TagCloser(htmlHelper, "</div>");
        }

        private static string GetScrollingClass(
            ContainerScrolling scrolling)
        {
            switch (scrolling)
            {
                case ContainerScrolling.Y:
                    return "ra-container-scrollable-y";

                case ContainerScrolling.Both:
                    return "ra-container-scrollable-both";

                case ContainerScrolling.X:
                    return "ra-container-scrollable-x";

                default:
                    return null;
            }
        }

        public static IHtmlContent RAAjaxContentSection(this IHtmlHelper htmlHelper,
            string contentKey,
            string contentUrl = null,
            string contentUrlScript = null,
            string contentClass = "ra-container ra-container-vertica",
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
            string contentClass = "ra-container ra-container-vertica",
            string contentStyle = null)
        {
            var content = new HtmlContentBuilder();
            content.AppendFormat("<div id=\"{0}AjaxContent\" class=\"{1}\" style=\"{2}\" data-content-url=\"{3}\" data-content-url-script=\"{4}\">",
                contentKey, contentClass, contentStyle, contentUrl, contentUrlScript);
            htmlHelper.ViewContext.Writer.Write(content);
            return new TagCloser(htmlHelper, "</div");
        }

        public static async Task<IHtmlContent> RAContentFrameAsync(
            this IHtmlHelper htmlHelper,
            string contentUrl,
            string containerClass = "ra-fullsize",
            string containerStyle = null,
            string frameClass = null,
            string frameStyle = null,
            bool showLoadingFullHeight = true,
            bool printFrameOnLoad = false,
            bool showPrintButton = false)
        {
            return await htmlHelper.RAContentFrameAsync(new RAContentFrameModel()
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

        public static async Task<IHtmlContent> RAContentFrameAsync(
            this IHtmlHelper htmlHelper,
            RAContentFrameModel contentFrame)
        {
            return await htmlHelper.PartialAsync("~/Views/RA/ContentFrame.cshtml", contentFrame);
        }

        public static string RACreatePartialViewContentFrameUrl(this IHtmlHelper htmlHelper,
            string contentUrl,
            string containerClass = "ra-fullsize",
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
