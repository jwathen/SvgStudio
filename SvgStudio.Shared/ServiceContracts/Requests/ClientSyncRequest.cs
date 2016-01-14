using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Requests
{
    public class ClientSyncRequest
    {
        public Dictionary<int, string> DesignRowVersions { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> DesignRegionRowVersions { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> FillRowVersions { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> PaletteRowVersions { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> ShapeRowVersions { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> StrokeRowVersions { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> TemplateRowVersions { get; set; } = new Dictionary<int, string>();
    }
}
