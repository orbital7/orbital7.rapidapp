using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp.SampleWebApp.Models.DataGrids
{
    public class _PanelBasicModel
    {
        public List<RowItem> Items { get; set; }

        public static _PanelBasicModel Create()
        {
            return new _PanelBasicModel()
            {
                Items = RowItem.CreateRowItems(100),
            };
        }
    }
}
