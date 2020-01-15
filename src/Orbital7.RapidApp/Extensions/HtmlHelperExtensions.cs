using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class HtmlHelperExtensions
    {
        public static string GetExpressionText<TModel, TResult>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            var expresionProvider = htmlHelper.ViewContext.HttpContext.RequestServices
                .GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;

            return expresionProvider.GetExpressionText(expression);
        }

        public static IHtmlContent FileFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, 
            Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var builder = new TagBuilder("input");
            var attributes = HtmlHelperHelper.ToAttributesDictionary(htmlAttributes);

            var id = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
                helper.GetExpressionText(expression));
            builder.GenerateId(id, "");
            builder.MergeAttribute("name", id);
            builder.MergeAttribute("type", "file");
            builder.MergeAttributes(attributes);
            builder.TagRenderMode = TagRenderMode.SelfClosing;

            return builder;
        }

        public static async Task<IHtmlContent> PartialForAsync<TModel, TResult>(this IHtmlHelper<TModel> helper, 
            Expression<Func<TModel, TResult>> expression, string partialViewName, string prefix = "")
        {
            var modelExplorer = helper.GetModelExplorer(expression);
            helper.ViewData.TemplateInfo.HtmlFieldPrefix += prefix;
            return await helper.PartialAsync(partialViewName, modelExplorer.Model, helper.ViewData);
        }
    }
}
