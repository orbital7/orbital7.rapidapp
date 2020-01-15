using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbital7.RapidApp.Models
{
    public class TaskbarView
    {
        public string Key { get; set; }

        public List<RATaskbarItem> Items { get; private set; }

        public TaskbarView(
            string key,
            List<RATaskbarItem> items)
        {
            this.Key = key;
            this.Items = items;
        }
    }
}
