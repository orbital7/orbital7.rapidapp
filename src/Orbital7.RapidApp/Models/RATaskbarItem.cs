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

        public bool IsVisible { get; private set; } = true;

        public bool IsHeading { get; set; }

        public RATaskbarItem(
            string text,
            bool isHeading)
        {
            this.Text = text;
            this.IsHeading = isHeading;
        }

        public RATaskbarItem(
            string text,
            RATaskbarItemType type,
            string action,
            bool isVisible = true)
        {
            this.Text = text;
            this.Type = type;
            this.IsVisible = isVisible;

            switch (this.Type)
            {
                case RATaskbarItemType.CustomAction:
                    this.Action = action;
                    break;

                case RATaskbarItemType.LoadPagePanel:
                    this.Action = $"loadPagePanel({(action.StartsWith("'") ? action : "'" + action + "'")});";
                    break;
            }
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
