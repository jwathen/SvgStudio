using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvgStudio.Shared.StorageModel;

namespace SvgStudio.Shared.Materializer
{
    public class DrawingFactory
    {
        private readonly IStorageRepository _db;

        public DrawingFactory(IStorageRepository db)
        {
            _db = db;
        }

        public Drawing.Palette BuildPalette(string id)
        {
            var result = new Drawing.Palette();
            result.StorageId = id;
            result.Strokes.AddRange(_db.LoadStrokesByPaletteId(id).Select(x => BuildStroke(x)));
            result.Fills.AddRange(_db.LoadFillsByPaletteId(id).Select(x => BuildFill(x)));
            
            if (!result.Strokes.Any() && !result.Fills.Any())
            {
                return null;
            }

            return result;
        }

        public Drawing.Stroke BuildStroke(StorageModel.Stroke storageStroke)
        {
            var result = new Drawing.Stroke();
            result.StorageId = storageStroke.Id;
            result.Color = Drawing.Color.FromName(storageStroke.Color);
            result.Width = storageStroke.Width;
            result.DashArray = storageStroke.DashArray;
            return result;
        }

        public Drawing.Fill BuildFill(StorageModel.Fill storageFill)
        {
            Drawing.Fill result = null;

            if (storageFill.FillType == FillType.SolidColor)
            {
                result = new Drawing.SolidColorFill(Drawing.Color.FromName(storageFill.SolidColorFill_Color));
            }
            else if (storageFill.FillType == FillType.Pattern)
            {
                result = new Drawing.PatternFill();
                var patternFill = (Drawing.PatternFill)result;
                patternFill.Name = storageFill.PatternFill_Name;
                patternFill.X = storageFill.PatternFill_X ?? 0;
                patternFill.Y = storageFill.PatternFill_Y ?? 0;
                patternFill.Width = storageFill.PatternFill_Width ?? 0;
                patternFill.Height = storageFill.PatternFill_Height ?? 0;
                patternFill.PatternUnits = storageFill.PatternFill_PatternUnits;
                patternFill.PatternContentUnits = storageFill.PatternFill_PatternContentUnits;
                patternFill.Design = BuildDesign(storageFill.PatternFill_DesignId);
            }

            result.StorageId = storageFill.Id;

            return result;
        }

        public Drawing.Design BuildDesign(string id)
        {
            Drawing.Design result = new Drawing.Design();

            StorageModel.Design storageDesign = _db.LoadDesign(id);
            result.Palette = BuildPalette(storageDesign.PaletteId);
            result.Shape = BuildShape(storageDesign.ShapeId);
            result.StorageId = id;

            return result;
        }

        public Drawing.Shape BuildShape(string id)
        {
            Drawing.Shape result = null;

            StorageModel.Shape storageShape = _db.LoadShape(id);
            if (storageShape.ShapeType == ShapeType.Basic)
            {
                result = new Drawing.BasicShape(
                    storageShape.Width, 
                    storageShape.Height,
                    storageShape.BasicShape_MarkupFragmentId,
                    _db.LoadMarkupFragmentContent);
            }
            else if (storageShape.ShapeType == ShapeType.Template)
            {
                var template = BuildTemplate(storageShape.TemplateShape_TemplateId);
                result = new Drawing.TemplateShape(template, storageShape.TemplateShape_ClipPathMarkupFragmentId, _db.LoadMarkupFragmentContent);
                Drawing.TemplateShape templateShape = (Drawing.TemplateShape)result;
            }

            result.StorageId = id;
            result.Name = storageShape.Name;
            result.Width = storageShape.Width;
            result.Height = storageShape.Height;

            return result;
        }

        public Drawing.Template BuildTemplate(string id)
        {
            Drawing.Template result = new Drawing.Template();
            StorageModel.Template storageTemplate = _db.LoadTemplate(id);

            result.Name = storageTemplate.Name;
            result.StorageId = id;
            result.DesignRegions.AddRange(_db.LoadDesignRegionsByTemplateId(id).Select(x => BuildDesignRegion(x)));

            return result;
        }

        public Drawing.DesignRegion BuildDesignRegion(StorageModel.DesignRegion storageDesignRegion)
        {
            Drawing.DesignRegion result = new Drawing.DesignRegion(
                storageDesignRegion.Name,
                storageDesignRegion.X,
                storageDesignRegion.Y,
                storageDesignRegion.Width,
                storageDesignRegion.Height);
            result.StorageId = storageDesignRegion.Id;
            result.SortOrder = storageDesignRegion.SortOrder;

            return result;
        }
    }
}
