using SvgStudio.Shared.ServiceContracts.Entities;
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
            DesignRegion_CompatibilityTagChanges = new JoiningTableChangeData<DesignRegion_CompatibilityTagDto>();
            Shape_CompatibilityTagChanges = new JoiningTableChangeData<Shape_CompatibilityTagDto>();
        }

        public EntityChangeData<CompatibilityTagDto> CompatibilityTagChanges { get; set; }
        public EntityChangeData<ContentLicenseDto> ContentLicenseChanges { get; set; }
        public EntityChangeData<DesignDto> DesignChanges { get; set; }
        public EntityChangeData<DesignRegionDto> DesignRegionChanges { get; set; }
        public EntityChangeData<FillDto> FillChanges { get; set; }
        public EntityChangeData<LicenseDto> LicenseChanges { get; set; }
        public EntityChangeData<MarkupFragmentDto> MarkupFragmentChanges { get; set; }
        public EntityChangeData<PaletteDto> PaletteChanges { get; set; }
        public EntityChangeData<ShapeDto> ShapeChanges { get; set; }
        public EntityChangeData<StrokeDto> StrokeChanges { get; set; }
        public EntityChangeData<TemplateDto> TemplateChanges { get; set; }
        public JoiningTableChangeData<DesignRegion_CompatibilityTagDto> DesignRegion_CompatibilityTagChanges { get; set; }
        public JoiningTableChangeData<Shape_CompatibilityTagDto> Shape_CompatibilityTagChanges { get; set; }
    }
}