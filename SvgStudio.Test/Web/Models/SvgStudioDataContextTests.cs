using SvgStudio.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Test.Web.Models
{
    public class SvgStudioDataContextTests : IDisposable
    {
        private SvgStudioDataContext _db = null;

        public SvgStudioDataContextTests()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SvgStudio"].ConnectionString;
            _db = new SvgStudioDataContext(connectionString);
        }

        public void CanSelectFromAllTables()
        {
            _db.CompatibilityTags.ToArray();
            _db.ContentLicenses.ToArray();
            _db.Designs.ToArray();
            _db.DesignRegions.ToArray();
            _db.DesignRegion_CompatibilityTags.ToArray();
            _db.Fills.ToArray();
            _db.Licenses.ToArray();
            _db.MarkupFragments.ToArray();
            _db.Palettes.ToArray();
            _db.Shapes.ToArray();
            _db.Shape_CompatibilityTags.ToArray();
            _db.Strokes.ToArray();
            _db.Templates.ToArray();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
