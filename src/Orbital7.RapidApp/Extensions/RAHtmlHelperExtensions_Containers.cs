using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
