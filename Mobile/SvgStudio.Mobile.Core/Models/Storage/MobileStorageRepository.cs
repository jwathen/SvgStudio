using SvgStudio.Shared.Materializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvgStudio.Shared.StorageModel;

namespace SvgStudio.Mobile.Core.Models.Storage
{
    public class MobileStorageRepository : IStorageRepository
    {
        private readonly SQLite.Net.SQLiteConnectionWithLock _db;
        private static object locker = new object();

        public MobileStorageRepository(SQLite.Net.SQLiteConnectionWithLock db)
        {
            _db = db;
        }

        public List<CompatibilityTag> LoadCompatibilityTagsByDesignRegionId(string designRegionId)
        {
            string sql = @"select ct.*
                           from DesignRegion_CompatibilityTag drct
                           inner join CompatibilityTag ct on ct.Id = drct.CompatibilityTagId
                           where drct.DesignRegionId = ?";
            return _db.Query<CompatibilityTag>(sql, designRegionId);
        }

        public ContentLicense LoadContentLicenceByShapeId(string shapeId)
        {
            return _db.Query<ContentLicense>("select * from ContentLicense where ShapeId = ? limit 1", shapeId).FirstOrDefault();
        }

        public Design LoadDesign(string id)
        {
            return _db.Find<Design>(id);
        }

        public DesignRegion LoadDesignRegion(string id)
        {
            return _db.Find<DesignRegion>(id);
        }

        public List<DesignRegion> LoadDesignRegionsByTemplateId(string templateId)
        {
            return _db.Query<DesignRegion>("select * from DesignRegion where TemplateId = ?", templateId);
        }

        public List<Fill> LoadFillsByPaletteId(string paletteId)
        {
            return _db.Query<Fill>("select * from Fill where PaletteId = ?", paletteId);
        }

        public License LoadLicense(string id)
        {
            return _db.Find<License>(id);
        }

        public string LoadMarkupFragmentContent(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            lock(locker)
            {
                return _db.ExecuteScalar<string>("select Content from MarkupFragment where Id = ? limit 1", id);
            }
        }

        public Shape LoadShape(string id)
        {
            return _db.Find<Shape>(id);
        }

        public List<Shape> LoadShapesByCompatibilityTagIds(List<string> compatibilityTagIds)
        {
            if (!compatibilityTagIds.Any())
            {
                return new List<Shape>();
            }

            string inClause = string.Concat(Enumerable.Repeat(", ?", compatibilityTagIds.Count - 1));
            string sql = @"select s.*
                           from Shape_CompatibilityTag ct
                           inner join Shape s on s.Id = ct.ShapeId
                           where ct.CompatibilityTagId in (?" + inClause + ")";
            
            return _db.Query<Shape>(sql, compatibilityTagIds.ToArray());
        }

        public List<Stroke> LoadStrokesByPaletteId(string paletteId)
        {
            return _db.Query<Stroke>("select * from Stroke where PaletteId = ?", paletteId);
        }

        public Template LoadTemplate(string id)
        {
            return _db.Find<Template>(id);
        }

        public List<Palette> LoadPalettes()
        {
            return _db.Table<Palette>().ToList();
        }
    }
}
