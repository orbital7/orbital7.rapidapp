﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp.Models
{
    public abstract class RATableRowItemBase : IDynamicTableRowItem
    {
        public Guid Index { get; set; }

        public string HtmlFieldPrefix { get; set; }

        public string IndexId => this.HtmlFieldPrefix + "_Index";

        public string IndexName => this.IndexId.Replace("_", ".");

        public string IndexedHtmlFieldPrefix => this.HtmlFieldPrefix + "[" + this.Index + "]";

        protected RATableRowItemBase()
        {
            this.Index = Guid.NewGuid();
        }

        protected RATableRowItemBase(string htmlFieldPrefix)
            : this()
        {
            this.HtmlFieldPrefix = htmlFieldPrefix.Replace(".", "_");
        }

        public string GetHtmlFieldId(string fieldName)
        {
            return this.IndexedHtmlFieldPrefix + "_" + fieldName.Replace(".", "_");
        }

        public string GetHtmlFieldName(string fieldName)
        {
            return GetHtmlFieldId(fieldName).Replace("_", ".");
        }
    }
}
