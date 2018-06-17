using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp.Models
{
    public class RATabsView
    {
        public string Id { get; private set; }

        public int SelectedIndex { get; private set; } = -1;

        public List<RATabItem> Items { get; private set; }
        
        public RATabsView(List<RATabItem> items)
        {
            this.Id = Guid.NewGuid().ToString().Replace("-", "");

            this.Items = items;

            int visibleCount = (from x in this.Items where x.IsVisible select x).Count();
            if (visibleCount > 0)
            {
                int index = 0;
                foreach (var item in this.Items)
                {
                    item.Index = index;

                    if (item.IsVisible)
                    {
                        item.WidthPercent = 100 / visibleCount;
                        if (this.SelectedIndex < 0)
                            this.SelectedIndex = index;
                    }

                    index++;
                }

                int difference = 100 - (from x in this.Items select x.WidthPercent).Sum();
                if (difference > 0)
                    (from x in this.Items where x.IsVisible select x).Last().WidthPercent += difference;
            }
        }

        public void SetSelectedIndex(string value)
        {
            int index = -1;
            Int32.TryParse(value, out index);

            if (index >= 0 && index < this.Items.Count && this.Items[index].IsVisible)
                this.SelectedIndex = index;
        }

        public string GetClassName(RATabItem item)
        {
            string className = String.Empty;

            if (!item.IsVisible)
            {
                className = "invisible";
            }
            else if (item.Index == this.SelectedIndex)
            {
                className = "active";
            }

            return className;
        }
    }
}
