using SvgStudio.Shared.ServiceContracts.Entities;
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
            CompatibilityTagRowVersions = new Dictionary<string, string>();
            ContentLicenseRowVersions = new Dictionary<string, string>();
            DesignRowVersions = new Dictionary<string, string>();
            DesignRegionRowVersions = new Dictionary<string, string>();
            FillRowVersions = new Dictionary<string, string>();
            LicenseRowVersions = new Dictionary<string, string>();
            MarkupFragmentRowVersions = new Dictionary<string, string>();
            PaletteRowVersions = new Dictionary<string, string>();
            ShapeRowVersions = new Dictionary<string, string>();
            StrokeRowVersions = new Dictionary<string, string>();
            TemplateRowVersions = new Dictionary<string, string>();
        }

        public Dictionary<string, string> CompatibilityTagRowVersions { get; set; }
        public Dictionary<string, string> ContentLicenseRowVersions { get; set; }
        public Dictionary<string, string> DesignRowVersions { get; set; }
        public Dictionary<string, string> DesignRegionRowVersions { get; set; }
        public Dictionary<string, string> FillRowVersions { get; set; }
        public Dictionary<string, string> LicenseRowVersions { get; set; }
        public Dictionary<string, string> MarkupFragmentRowVersions { get; set; }
        public Dictionary<string, string> PaletteRowVersions { get; set; }
        public Dictionary<string, string> ShapeRowVersions { get; set; }
        public Dictionary<string, string> StrokeRowVersions { get; set; }
        public Dictionary<string, string> TemplateRowVersions { get; set; }
        public DesignRegion_CompatibilityTagDto[] DesignRegion_CompatibilityTags { get; set; }
        public Shape_CompatibilityTagDto[] Shape_CompatibilityTags { get; set; }
    }
}
