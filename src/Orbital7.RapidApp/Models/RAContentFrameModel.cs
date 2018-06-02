using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orbital7.RapidApp.Models
{
    public class RAContentFrameModel
    {
        public string Id { get; private set; }

        public string ContentUrl { get; set; }

        public string ContainerClass { get; set; }

        public string ContainerStyle { get; set; }

        public string FrameClass { get; set; }

        public string FrameStyle { get; set; }

        public bool PrintFrameOnLoad { get; set; }

        public bool ShowPrintButton { get; set; }

        public bool ShowLoadingFullHeight { get; set; }

        public string ContainerId => "ra-contentframe-container-" + this.Id;

        public string FrameId => "ra-contentframe-frame-" + this.Id;

        public string LoadingId => "ra-contentframe-loading-" + this.Id;

        public string ToolbarId => "ra-contentframe-toolbar-" + this.Id;

        public string PrintFunctionName => "Print" + this.Id + "()";

        public bool ShowToolbar => this.ShowPrintButton;

        public RAContentFrameModel()
        {
            this.Id = Guid.NewGuid().ToString().Remove("-");
        }

        public override string ToString()
        {
            return this.Id;
        }
    }
}
