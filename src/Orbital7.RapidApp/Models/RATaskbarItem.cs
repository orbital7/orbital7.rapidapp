using System;
using System.Collections.Generic;
using System.Text;

namespace Orbital7.RapidApp.Models
{
    public enum RATaskbarItemType
    {
        CustomAction,

        LoadPagePanel,
    }

    public class RATaskbarItem
    {
        public string Text { get; private set; }

        public RATaskbarItemType Type { get; private set; }

        public string Action { get; private set; }

        public RATaskbarItem(
            string text,
            RATaskbarItemType type,
            string action)
        {
            this.Text = text;
            this.Type = type;

            switch (this.Type)
            {
                case RATaskbarItemType.CustomAction:
                    this.Action = action;
                    break;

                case RATaskbarItemType.LoadPagePanel:
                    this.Action = "loadPagePanel('" + action + "');";
                    break;
            }
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
