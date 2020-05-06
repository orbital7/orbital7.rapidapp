using System;
using System.Collections.Generic;
using System.Text;

namespace Orbital7.RapidApp.Models
{
    public interface IDynamicTableRowItem
    {
        Guid Index { get; set; }

        string HtmlFieldPrefix { get; set; }

        string IndexId { get; }

        string IndexName { get; }

        string IndexedHtmlFieldPrefix { get; }

        string GetHtmlFieldId(string fieldName);

        string GetHtmlFieldName(string fieldName);
    }
}
