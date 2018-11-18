using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Orbital7.RapidApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Mvc
{
    public static partial class RAHtmlHelperExtensions
    {
        public static IHtmlContent RAInfrastructurePageComponents(
            this IHtmlHelper htmlHelper,
            string layoutMinimumSizeTitle = "Whoops!",
            string layoutMinimumSizeMessageHtml = 
                "Please <span class='ra-font-bold'>rotate</span> your device or " +
                "<span class='ra-font-bold'>resize</span> your browser window")
        {
            var model = new InfrastructurePageComponentsModel()
            {
                LayoutMinimumSizeTitle = layoutMinimumSizeTitle,
                LayoutMinimumSizeMessageHtml = layoutMinimumSizeMessageHtml,
            };

            return htmlHelper.Partial("~/Views/RA/InfrastructurePageComponents.cshtml", model);
        }

        public static IHtmlContent RAInfrastructureBodyComponents(
            this IHtmlHelper htmlHelper,
            bool includeTouch = false)
        {
            return htmlHelper.Partial("~/Views/RA/InfrastructureBodyComponents.cshtml",
                includeTouch);
        }

        public static IHtmlContent RAInfrastructureScripts(
            this IHtmlHelper htmlHelper,
            bool includeTouch = false)
        {
            return htmlHelper.Partial("~/Views/RA/InfrastructureScripts.cshtml",
                includeTouch);
        }

        public static IHtmlContent RAInfrastructureStylesheets(
            this IHtmlHelper htmlHelper)
        {
            return htmlHelper.Partial("~/Views/RA/InfrastructureStylesheets.cshtml");
        }

        public static IHtmlContent RAInfrastructureSignaturePad(
            this IHtmlHelper htmlHelper)
        {
            return htmlHelper.Partial("~/Views/RA/InfrastructureSignaturePad.cshtml");
        }
    }
}
