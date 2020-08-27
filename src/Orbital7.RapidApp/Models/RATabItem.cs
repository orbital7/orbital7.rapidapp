using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp.Models
{
    public class RATabItem
    {
        public string Name { get; private set; }

        public string PartialViewUrl { get; private set; }

        public bool IsVisible { get; private set; }

        public int Index { get; private set; }

        public int WidthPercent { get; internal set; }

        public RATabItem(
            string name, 
            string partialViewUrl,
            int tabIndex,
            bool isVisible = true)
        {
            this.Name = name;
            this.PartialViewUrl = partialViewUrl;
            this.IsVisible = isVisible;
            this.Index = tabIndex;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
