using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp2.Models
{
    public class RASubTabsView : RATabsView
    {
        public string SubTabsName { get; private set; }

        public string HeaderId
        {
            get { return this.Id + "_Header"; }
        }

        public RASubTabsView(string subTabsName, List<RATabItem> items) 
            : base(items)
        {
            this.SubTabsName = subTabsName;
        }
    }
}
