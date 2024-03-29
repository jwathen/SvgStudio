﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.Materializer
{
    public interface IStorageRepository
    {
        List<StorageModel.Fill> LoadFillsByPaletteId(string paletteId);

        List<StorageModel.Stroke> LoadStrokesByPaletteId(string paletteId);

        StorageModel.Design LoadDesign(string id);

        StorageModel.Shape LoadShape(string id);

        List<StorageModel.Palette> LoadPalettes();

        List<StorageModel.Shape> LoadShapesByCompatibilityTagIds(List<string> compatibilityTagIds);

        string LoadMarkupFragmentContent(string id);

        StorageModel.Template LoadTemplate(string id);

        List<StorageModel.DesignRegion> LoadDesignRegionsByTemplateId(string templateId);

        StorageModel.DesignRegion LoadDesignRegion(string id);

        List<StorageModel.CompatibilityTag> LoadCompatibilityTagsByDesignRegionId(string designRegionId);

        StorageModel.ContentLicense LoadContentLicenceByShapeId(string shapeId);

        StorageModel.License LoadLicense(string id);
    }
}
