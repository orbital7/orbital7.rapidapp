using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApp.Models
{
    public class RowItem
    {
        [Display(Name = "Value 0")]
        public string Value0 { get; set; }

        [Display(Name = "Value 1")]
        public int Value1 { get; set; }

        [Display(Name = "Value 2")]
        public int Value2 { get; set; }

        [Display(Name = "Value 3")]
        public DateTime Value3 { get; set; }

        public static List<RowItem> CreateRowItems(
            int numberOfRows)
        {
            var items = new List<RowItem>();

            for (int i = 0; i < numberOfRows; i++)
            {
                items.Add(new RowItem()
                {
                    Value0 = "My Value " + i,
                    Value1 = 100 + i,
                    Value2 = 2000 + i,
                    Value3 = DateTime.Now.AddDays(i),
                });
            }

            return items;
        }
    }
}
