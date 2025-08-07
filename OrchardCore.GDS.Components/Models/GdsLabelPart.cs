using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchardCore.GDS.Components.Models
{
    public class GdsLabelPart : ContentPart
    {
        public string Label { get; set; }
        public string LabelStyle { get; set; }
        public string LabelSize { get; set; }
        public string LabelClass { get; set; }
    }
}
