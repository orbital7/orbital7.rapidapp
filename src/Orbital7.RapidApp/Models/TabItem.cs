﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp.Models
{
    public class TabItem
    {
        public string Name { get; set; }

        public string PartialViewUrl { get; set; }

        public bool IsVisible { get; set; }

        public int Index { get; internal set; }

        public int WidthPercent { get; set; }

        public TabItem(string name, string partialViewUrl, bool isVisible = true)
        {
            this.Name = name;
            this.PartialViewUrl = partialViewUrl;
            this.IsVisible = isVisible;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
