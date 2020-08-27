using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Orbital7.RapidApp.Models
{
    public class TabsView
    {
        public int SelectedIndex { get; private set; } = -1;

        public List<RATabItem> Items { get; private set; }

        public TabsView(List<RATabItem> items)
        {
            this.Items = (from x in items
                          orderby x.Index
                          select x).ToList();

            int visibleCount = (from x in this.Items 
                                where x.IsVisible 
                                select x).Count();

            if (visibleCount > 0)
            {
                foreach (var item in this.Items)
                {
                    if (item.IsVisible)
                    {
                        item.WidthPercent = 100 / visibleCount;
                        if (this.SelectedIndex < 0)
                            this.SelectedIndex = item.Index;
                    }
                }

                int difference = 100 - (from x in this.Items select x.WidthPercent).Sum();
                if (difference > 0)
                    (from x in this.Items where x.IsVisible select x).Last().WidthPercent += difference;
            }
        }

        public void SetSelectedIndex(string value)
        {
            Int32.TryParse(value, out int index);

            if (index >= 0 && index < this.Items.Count && this.Items[index].IsVisible)
                this.SelectedIndex = index;
        }
    }
}
