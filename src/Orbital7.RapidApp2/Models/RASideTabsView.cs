using System;
using System.Collections.Generic;
using System.Text;

namespace Orbital7.RapidApp2.Models
{
    public class RASideTabsView : RATabsView
    {
        public string HeaderId
        {
            get { return this.Id + "_Header"; }
        }

        public RASideTabsView(List<RATabItem> items)
            : base(items)
        {
            
        }
    }
}
