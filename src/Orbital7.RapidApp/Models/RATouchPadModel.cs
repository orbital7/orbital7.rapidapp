using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp.Models
{
    public enum TouchPadMode
    {
        Currency,

        PIN,

        Integer,

        PhoneNumber,

        Double,
    }

    public class RATouchPadModel
    {
        public string ValueId { get; set; }

        public TouchPadMode Mode { get; set; }

        public string PostEditUpdateScript { get; set; }

        public bool ShowDoneButton { get; set; } = true;
    }
}
