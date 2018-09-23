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
        public static TagCloser RABeginContainer(
            this IHtmlHelper htmlHelper,
            bool isPadded,
            ContainerScrolling scrolling = ContainerScrolling.None,
            string containerClass = null,
            string containerStyle = null)
        {
            var content = new HtmlContentBuilder();
            content.AppendFormat("<div class='ra-container {0} {1} {2}' style='{3}'>",
                isPadded ? "ra-container-padded" : null,
                GetScrollingClass(scrolling), 
                containerClass, 
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
                    return "ra-container-scrollable";

                case ContainerScrolling.X:
                    return "ra-container-scrollable-x";

                default:
                    return null;
            }
        }
    }
}
