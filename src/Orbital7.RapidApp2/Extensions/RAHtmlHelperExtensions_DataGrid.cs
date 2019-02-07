﻿using Microsoft.AspNetCore.Html;
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
    public enum RASortDirection
    {
        Ascending,

        Descending
    }

    public static partial class RAHtmlHelperExtensions
    {
        public static TagCloserX RABeginDataGrid(
            this IHtmlHelper htmlHelper, 
            string tableClass = null,
            string tableStyle = null,
            int fixedHeight = 0, 
            bool sortable = true,
            bool updateRowColors = true)
        {
            var tableId = Guid.NewGuid().ToString().Replace("-", "");

            var content = new HtmlContentBuilder();
            content.AppendFormat("<table id='{0}' class='ra-datagrid-table {1} {2}' style='{3}'>", 
                tableId, 
                sortable ? "sort" : null,
                tableClass,
                tableStyle);
            htmlHelper.ViewContext.Writer.Write(content);

            var sortCommand = String.Format("new Tablesort(document.getElementById('{0}'));", tableId);
            var closingTags = String.Format("</table><script>{0}setupDataGrid('{1}',{2},{3});</script>", 
                sortable ? sortCommand : null, 
                tableId, 
                fixedHeight,
                updateRowColors.Totruefalse());
            return new TagCloserX(htmlHelper, closingTags);
        }

        public static TagCloserX RABeginDataGridSelectableRow(
            this IHtmlHelper htmlHelper, 
            IIdObject idObject, 
            bool canSelect = true,
            string idOverride = null)
        {
            var id = !String.IsNullOrEmpty(idOverride) ? idOverride : idObject.Id.ToString();

            var content = new HtmlContentBuilder();
            content.AppendFormat("<tr class='{0}' data-row-id='{1}' data-row-name='{2}' onmouseup='{3}'", 
                canSelect ? "ra-datagrid-row-selectable" : "", id, idObject.ToString(), 
                canSelect ? "selectDataGridRow(this);" : "");
            content.AppendHtml(">");
            htmlHelper.ViewContext.Writer.Write(content);
            return new TagCloserX(htmlHelper, "</tr>");
        }

        public static IHtmlContent RADataGridHeadingCell(
            this IHtmlHelper htmlHelper, 
            string cellValueHtml = null,
            string cellClass = null, 
            string cellStyle = null, 
            bool sortable = true, 
            string sortMethod = null, 
            bool isSortDefault = false,
            RASortDirection? sortDirection = null)
        {
            var content = new HtmlContentBuilder();

            content.AppendHtml(GetDataGridHeadingCellStartHtml(
                cellClass,
                cellStyle,
                sortable,
                sortMethod,
                isSortDefault,
                sortDirection));
            content.AppendHtml(cellValueHtml);
            content.AppendHtml(GetDataGridHeadingCellEndHtml());

            return content;
        }

        private static string GetDataGridHeadingCellStartHtml(
            string cellClass = null,
            string cellStyle = null,
            bool sortable = true,
            string sortMethod = null,
            bool isSortDefault = false,
            RASortDirection? sortDirection = null)
        {
            if (!sortable)
                sortMethod = "none";

            // NB: Due to a peculiarity of how TableSort handles initial sorting, we 
            // need to specify the OPPOSITE of what we actually want here.
            string sortAria = null;
            if (sortDirection.HasValue)
            {
                // Intentionally opposite.
                if (sortDirection.Value == RASortDirection.Ascending)
                    sortAria = "descending";
                else
                    sortAria = "ascending";
            }

            return String.Format("<th class='ra-datagrid-cell-header {0} {1}' " +
                "style='{2}' {3} {4} {5}>",
                cellClass,
                sortable ? null : "ra-datagrid-notsortable",
                cellStyle,
                !String.IsNullOrEmpty(sortMethod) ? "data-sort-method='" + sortMethod + "'" : null,
                sortable && isSortDefault ? "data-sort-default" : null,
                sortable && sortDirection.HasValue ? "aria-sort='" + sortAria + "'" : null);
        }

        private static string GetDataGridHeadingCellEndHtml()
        {
            return "<sup class='ra-datagrid-sortaria' style='visibility: hidden;'>&#9650;</sup></th>";
        }

        public static IHtmlContent RADataGridHeadingCellFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, 
            string cellClass = null, 
            string cellStyle = null,
            bool sortable = true, 
            string sortMethodOverride = null, 
            bool isSortDefault = false,
            RASortDirection? sortDirection = null)
        {
            var modelExplorer = htmlHelper.GetModelExplorer(expression);
            var sortMethod = sortable && String.IsNullOrEmpty(sortMethodOverride) ? GetSortMethod(modelExplorer) : sortMethodOverride;

            return RADataGridHeadingCell(htmlHelper, modelExplorer.GetPropertyDisplayName(),
                cellClass, cellStyle, sortable, sortMethod, isSortDefault, sortDirection);
        }


        public static TagCloserX RABeginDataGridHeadingCell(
            this IHtmlHelper htmlHelper, 
            string cellClass = null, 
            string cellStyle = null,
            bool sortable = true,
            string sortMethod = null,
            bool isSortDefault = false,
            RASortDirection? sortDirection = null)
        {
            htmlHelper.ViewContext.Writer.Write(GetDataGridHeadingCellStartHtml(
                cellClass,
                cellStyle,
                sortable,
                sortMethod,
                isSortDefault,
                sortDirection));
            return new TagCloserX(htmlHelper, GetDataGridHeadingCellEndHtml());
        }

        private static string GetSortMethod(ModelExplorer modelExplorer)
        {
            if (modelExplorer != null && modelExplorer.ModelType != null)
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
            }

            return null;
        }

        public static TagCloserX RABeginDataGridBodyCell(this IHtmlHelper htmlHelper, string cellClass = null, string cellStyle = null)
        {
            htmlHelper.ViewContext.Writer.Write("<td class='ra-datagrid-cell {0}' style='{1}'>", cellClass, cellStyle);
            return new TagCloserX(htmlHelper, "</td>");
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