using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Orbital7.Extensions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static partial class RAHtmlHelperExtensions
    {
        // Data grid source: https://codepen.io/daveoncode/pen/LNomBE
        public static TagCloser RABeginDataGrid(
            this IHtmlHelper htmlHelper, 
            bool fullHeight = true, 
            bool sortable = true,
            int fullHeightBottomOffset = 2)
        {
            var tableId = Guid.NewGuid().ToString().Replace("-", "");

            var content = new HtmlContentBuilder();
            content.AppendHtml("<div class='ra-datagrid-wrapper'>");
            content.AppendFormat("<div class='ra-datagrid-container {0}' data-fullheight-offset='{1}'>", 
                fullHeight ? "ra-fullheight" : "", fullHeightBottomOffset);
            content.AppendFormat("<table id='{0}' class='ra-datagrid-table {1}'>", tableId, sortable ? "sort" : null);

            htmlHelper.ViewContext.Writer.Write(content);
            var sortCommand = String.Format("new Tablesort(document.getElementById('{0}'));", tableId);
            var closingTags = String.Format("</table></div></div><script>{0}setupDataGrid();resizeAll();</script>", 
                sortable ? sortCommand : null);
            return new TagCloser(htmlHelper, closingTags);
        }

        public static TagCloser RABeginDataGridSelectableRow(this IHtmlHelper htmlHelper, IIdObject idObject, bool canSelect = true)
        {
            var content = new HtmlContentBuilder();
            content.AppendFormat("<tr class='{0}' data-row-id='{1}' data-row-name='{2}' onmouseup='{3}'", 
                canSelect ? "ra-datagrid-row-selectable" : "", idObject.Id, idObject.ToString(), 
                canSelect ? "selectDataGridRow(this);" : "");
            content.AppendHtml(">");
            htmlHelper.ViewContext.Writer.Write(content);
            return new TagCloser(htmlHelper, "</tr>");
        }

        public static IHtmlContent RADataGridHeadingCell(this IHtmlHelper htmlHelper, string cellValueHtml,
            string cellClass = null, string cellStyle = null, bool sortable = true, string sortMethod = null, 
            bool isSortDefault = false)
        {
            if (!sortable)
                sortMethod = "none";

            return new HtmlString(String.Format("<th class='ra-datagrid-cell-header {0}' style='{1}' {2} {3}>{4}<div>{4}</div></th>",
                cellClass, 
                cellStyle, 
                !String.IsNullOrEmpty(sortMethod) ? "data-sort-method='" + sortMethod + "'" : null, 
                sortable && isSortDefault ? "data-sort-default" : null,
                cellValueHtml));
        }

        public static IHtmlContent RADataGridHeadingCellFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string cellClass = null, string cellStyle = null,
            bool sortable = true, string sortMethodOverride = null, bool isSortDefault = false)
        {
            var modelExplorer = htmlHelper.GetModelExplorer(expression);
            var sortMethod = sortable && String.IsNullOrEmpty(sortMethodOverride) ? GetSortMethod(modelExplorer) : sortMethodOverride;

            return RADataGridHeadingCell(htmlHelper, modelExplorer.GetPropertyDisplayName(),
                cellClass, cellStyle, sortable, sortMethod, isSortDefault);
        }

        private static string GetSortMethod(ModelExplorer modelExplorer)
        {
            if (modelExplorer.ModelType == typeof(double) ||
                modelExplorer.ModelType == typeof(double?) ||
                modelExplorer.ModelType == typeof(decimal) ||
                modelExplorer.ModelType == typeof(decimal?) ||
                modelExplorer.ModelType == typeof(int) ||
                modelExplorer.ModelType == typeof(int?) ||
                modelExplorer.ModelType == typeof(short) ||
                modelExplorer.ModelType == typeof(short?))
            {
                return "number";
            }
            else if (modelExplorer.ModelType == typeof(DateTime) ||
                modelExplorer.ModelType == typeof(DateTime?))
            {
                return "date";
            }

            return null;
        }

        public static TagCloser RABeginDataGridBodyCell(this IHtmlHelper htmlHelper, string cellClass = null, string cellStyle = null)
        {
            htmlHelper.ViewContext.Writer.Write("<td class='ra-datagrid-cell {0}' style='{1}'>", cellClass, cellStyle);
            return new TagCloser(htmlHelper, "</td>");
        }

        public static IHtmlContent RADataGridBodyCell<TModel>(this IHtmlHelper<TModel> htmlHelper, object cellValue,
            string cellValueNull = "-", string cellValueUrl = null, string cellClass = null, string cellStyle = null, 
            string fieldName = null)
        {
            string cellAlignmentStyle = GetValueCellAlignmentClass(cellValue);
            return new HtmlString(String.Format("<td class='ra-datagrid-cell {0} {1}' style='{2}'>{3}</td>", cellAlignmentStyle,
                cellClass, cellStyle, GetValueDisplayHtml(htmlHelper, cellValue, cellValueNull, cellValueUrl, fieldName: fieldName)));
        }

        public static IHtmlContent RADataGridBodyCellFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string cellValueNull = "-", string cellValueUrl = null,
            string cellClass = null, string cellStyle = null)
        {
            var modelExplorer = htmlHelper.GetModelExplorer(expression);
            return RADataGridBodyCell(htmlHelper, modelExplorer.Model, cellValueNull, cellValueUrl, cellClass, cellStyle, 
                modelExplorer.Metadata.PropertyName);
        }

        public static IHtmlContent RADataGridSelectedRowList<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string dataGridId, bool includeRowNames = true, string rowNameClass = null)
        {
            var content = new HtmlContentBuilder();
            content.AppendHtml("<div id=\"selectedDataGridItemsList\"></div>");
            content.AppendFormat("<script>createSelectedDataGridItemsList('{0}', '{1}', '{2}', '{3}');</script>", 
                dataGridId, ExpressionHelper.GetExpressionText(expression), includeRowNames.ToString().ToLower(), rowNameClass);

            return content;
        }
    }
}
