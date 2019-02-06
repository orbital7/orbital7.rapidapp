using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp2.Models
{
    public enum DashboardState
    {
        Good,

        Warning,

        Bad,
    }

    public class RADashboardItem
    {
        public int Count { get; set; }

        public string Description { get; set; }

        public DashboardState State { get; set; }

        public string Url { get; set; }
    }
}
