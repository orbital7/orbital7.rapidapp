using System;
using System.Xml.Serialization;
using Orbital7.Extensions.Reporting;

namespace Orbital7.RapidApp.Models
{
    public class RAReportScope
    {
        [XmlIgnore]
        public IReport Report { get; private set; }

        public bool ShowOptions { get; set; }

        public string AllOptionText { get; set; }

        public string SpecificOptionText { get; set; }

        public RAReportScope()
        {

        }

        public RAReportScope(IReport report)
            : this()
        {
            this.Report = report;
        }

        public string GetSelectableEnabledClass(SelectableTuple<string, string> selectable, string disabledClass)
        {
            if (!selectable.CanSelect)
                return disabledClass;
            else
                return null;
        }

        public string GetSelectableCheckedValue(SelectableTuple<string, string> selectable)
        {
            if (selectable.IsSelected)
                return "checked";
            else
                return "";
        }
    }
}
