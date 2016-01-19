using SvgStudio.Shared.StorageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Responses
{
    public class MobileSyncResponse
    {
        public MobileSyncResponse()
        {
 
        }

        public EntityChangeData<CompatibilityTag> CompatibilityTagChanges { get; set; }
        public EntityChangeData<ContentLicense> ContentLicenseChanges { get; set; }
        public EntityChangeData<Design> DesignChanges { get; set; }
        public EntityChangeData<DesignRegion> DesignRegionChanges { get; set; }
        public EntityChangeData<DesignRegion_CompatibilityTag> DesignRegion_CompatibilityTagChanges { get; set; }
        public EntityChangeData<Fill> FillChanges { get; set; }
        public EntityChangeData<License> LicenseChanges { get; set; }
        public EntityChangeData<MarkupFragment> MarkupFragmentChanges { get; set; }
        public EntityChangeData<Palette> PaletteChanges { get; set; }
        public EntityChangeData<Shape> ShapeChanges { get; set; }
        public EntityChangeData<Shape_CompatibilityTag> Shape_CompatibilityTagChanges { get; set; }
        public EntityChangeData<Stroke> StrokeChanges { get; set; }
        public EntityChangeData<Template> TemplateChanges { get; set; }
    }
}