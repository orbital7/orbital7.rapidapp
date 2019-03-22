using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp.SampleWebApp.Models.ModalDialogs
{
    public class _DialogMessageModel
    {
        public string Message { get; set; }

        public static _DialogMessageModel Create(
            string message)
        {
            return new _DialogMessageModel()
            {
                Message = message,
            };
        }
    }
}
