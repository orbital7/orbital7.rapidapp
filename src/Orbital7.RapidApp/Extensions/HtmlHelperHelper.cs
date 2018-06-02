using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class HtmlHelperHelper
    {
        public static IDictionary<string, object> ToAttributesDictionary(object htmlAttributes)
        {
            IDictionary<string, object> attributes = null;

            if (htmlAttributes == null)
                attributes = new RouteValueDictionary();
            else if (htmlAttributes is IDictionary<string, object>)
                attributes = (IDictionary<string, object>)htmlAttributes;
            else
                attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            return attributes;
        }

        public static string GetHtmlFieldPrefix<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return ExpressionHelper.GetExpressionText(expression);
        }
    }
}
