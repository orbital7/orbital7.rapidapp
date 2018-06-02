using Orbital7.Extensions.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp.Models
{
    public class RAReportView
    {
        public ReportNativeType ReportNativeType { get; set; }

        public string ReportNativeDownloadUrl { get; set; }

        public string ReportPdfDownloadUrl { get; set; }

        public string ReportPdfInlineDisplayUrl { get; set; }
    }
}
