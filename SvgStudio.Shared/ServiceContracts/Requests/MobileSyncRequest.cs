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
            CompatibilityTagRowVersions = new Dictionary<string, byte[]>();
            ContentLicenseRowVersions = new Dictionary<string, byte[]>();
            DesignRowVersions = new Dictionary<string, byte[]>();
            DesignRegionRowVersions = new Dictionary<string, byte[]>();
            FillRowVersions = new Dictionary<string, byte[]>();
            LicenseRowVersions = new Dictionary<string, byte[]>();
            MarkupFragmentRowVersions = new Dictionary<string, byte[]>();
            PaletteRowVersions = new Dictionary<string, byte[]>();
            ShapeRowVersions = new Dictionary<string, byte[]>();
            StrokeRowVersions = new Dictionary<string, byte[]>();
            TemplateRowVersions = new Dictionary<string, byte[]>();
            DesignRegion_CompatibilityTagRowVersions = new Dictionary<string, byte[]>();
            Shape_CompatibilityTagRowVersions = new Dictionary<string, byte[]>();
        }

        public Dictionary<string, byte[]> CompatibilityTagRowVersions { get; set; }
        public Dictionary<string, byte[]> ContentLicenseRowVersions { get; set; }
        public Dictionary<string, byte[]> DesignRowVersions { get; set; }
        public Dictionary<string, byte[]> DesignRegionRowVersions { get; set; }
        public Dictionary<string, byte[]> FillRowVersions { get; set; }
        public Dictionary<string, byte[]> LicenseRowVersions { get; set; }
        public Dictionary<string, byte[]> MarkupFragmentRowVersions { get; set; }
        public Dictionary<string, byte[]> PaletteRowVersions { get; set; }
        public Dictionary<string, byte[]> ShapeRowVersions { get; set; }
        public Dictionary<string, byte[]> StrokeRowVersions { get; set; }
        public Dictionary<string, byte[]> TemplateRowVersions { get; set; }
        public Dictionary<string, byte[]> DesignRegion_CompatibilityTagRowVersions { get; set; }
        public Dictionary<string, byte[]> Shape_CompatibilityTagRowVersions { get; set; }
    }
}
