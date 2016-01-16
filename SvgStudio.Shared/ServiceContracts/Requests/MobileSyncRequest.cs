using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Requests
{
    public class MobileSyncRequest
    {
        public MobileSyncRequest()
        {
            TemplateRowVersions = new Dictionary<string, string>();
        }

        public Dictionary<string, string> DesignRowVersions { get; set; }
        public Dictionary<string, string> DesignRegionRowVersions { get; set; }
        public Dictionary<string, string> FillRowVersions { get; set; }
        public Dictionary<string, string> PaletteRowVersions { get; set; }
        public Dictionary<string, string> ShapeRowVersions { get; set; }
        public Dictionary<string, string> StrokeRowVersions { get; set; }
        public Dictionary<string, string> TemplateRowVersions { get; set; }
    }
}
