using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Orbital7.RapidApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static partial class RAHtmlHelperExtensions
    {
        public static async Task<IHtmlContent> RAInfrastructurePageComponentsAsync(
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

            return await htmlHelper.PartialAsync("~/Views/RA/InfrastructurePageComponents.cshtml", model);
        }

        public static async Task<IHtmlContent> RAInfrastructureScriptsAsync(
            this IHtmlHelper htmlHelper)
        {
            return await htmlHelper.PartialAsync("~/Views/RA/InfrastructureScripts.cshtml");
        }

        public static async Task<IHtmlContent> RAInfrastructureNoTaskbarAsync(
            this IHtmlHelper htmlHelper)
        {
            return await htmlHelper.PartialAsync("~/Views/RA/InfrastructureNoTaskbar.cshtml");
        }

        public static async Task<IHtmlContent> RAInfrastructureStylesheetsAsync(
            this IHtmlHelper htmlHelper,
            string mainCSSFileName = "site.css",
            string fontAwesomeCSSLink = "<link rel='stylesheet' href='https://use.fontawesome.com/releases/v5.7.2/css/all.css' integrity='sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr' crossorigin='anonymous'>")
        {
            return await htmlHelper.PartialAsync(
                "~/Views/RA/InfrastructureStylesheets.cshtml",
                new InfrastructureStylesheetsModel()
                {
                    MainCSSFilename = mainCSSFileName,
                    FontAwesomeCSSLink = fontAwesomeCSSLink,
                }); ;
        }

        public static async Task<IHtmlContent> RAInfrastructureSignaturePadAsync(
            this IHtmlHelper htmlHelper)
        {
            return await htmlHelper.PartialAsync("~/Views/RA/InfrastructureSignaturePad.cshtml");
        }
    }
}
