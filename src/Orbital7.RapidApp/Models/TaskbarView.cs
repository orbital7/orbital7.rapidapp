using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbital7.RapidApp.Models
{
    public class TaskbarView
    {
        public List<RATaskbarItem> Items { get; private set; }

        public TaskbarView(List<RATaskbarItem> items)
        {
            this.Items = items;
        }
    }
}
