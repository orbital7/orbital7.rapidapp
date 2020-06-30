using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Orbital7.RapidApp.Models;
using SampleWebApp.Models;

namespace SampleWebApp.Pages.DataGrids
{
    public class PanelToolbarModel 
        : PartialPageModel
    {
        public List<RowItem> Items { get; set; }

        public void OnGet()
        {
            this.Items = RowItem.CreateRowItems(200);
        }
    }
}
