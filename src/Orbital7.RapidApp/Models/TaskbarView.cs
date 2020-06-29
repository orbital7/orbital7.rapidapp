using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbital7.RapidApp.Models
{
    public class TaskbarView
    {
        public string Key { get; set; }

        public List<RATaskbarItem> Items { get; private set; }

        public RATaskbarItem FirstItem =>
            (from x in this.Items
             where !x.IsHeading
             select x).FirstOrDefault();

        public TaskbarView(
            string key,
            List<RATaskbarItem> items)
        {
            this.Key = key;
            this.Items = items;
        }
    }
}
