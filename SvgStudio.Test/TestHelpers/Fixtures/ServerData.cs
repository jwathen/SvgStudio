using SvgStudio.Shared.StorageModel;
using SvgStudio.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Test.TestHelpers.Fixtures
{
    public class ServerData
    {
        public Dictionary<string, CompatibilityTag> CompatibilityTags { get; set; }
        public Dictionary<string, ContentLicense> ContentLicenses { get; set; }
        public Dictionary<string, Design> Designs { get; set; }
        public Dictionary<string, DesignRegion> DesignRegions { get; set; }
        public Dictionary<string, DesignRegion_CompatibilityTag> DesignRegion_CompatibilityTags { get; set; }
        public Dictionary<string, Fill> Fills { get; set; }
        public Dictionary<string, License> Licenses { get; set; }
        public Dictionary<string, MarkupFragment> MarkupFragments { get; set; }
        public Dictionary<string, Palette> Palettes { get; set; }
        public Dictionary<string, Shape> Shapes { get; set; }
        public Dictionary<string, Shape_CompatibilityTag> Shape_CompatibilityTags { get; set; }
        public Dictionary<string, Stroke> Strokes { get; set; }
        public Dictionary<string, Template> Templates { get; set; }

        public static ServerData CreateFixtures(SvgStudioDataContext db)
        {
            using (var fs = File.OpenRead(@"TestHelpers\Fixtures\ServerData.yaml"))
            using (var reader = new StreamReader(fs))
            {
                var yaml = new YamlDotNet.Serialization.Deserializer();
                ServerData data = yaml.Deserialize<ServerData>(reader);

                db.Reset();

                // Insert the test data.
                db.CompatibilityTags.AddRange(data.CompatibilityTags.Values);
                db.ContentLicenses.AddRange(data.ContentLicenses.Values);
                db.Designs.AddRange(data.Designs.Values);
                db.DesignRegions.AddRange(data.DesignRegions.Values);
                db.DesignRegion_CompatibilityTags.AddRange(data.DesignRegion_CompatibilityTags.Values);
                db.Fills.AddRange(data.Fills.Values);
                db.Licenses.AddRange(data.Licenses.Values);
                db.MarkupFragments.AddRange(data.MarkupFragments.Values);
                db.Palettes.AddRange(data.Palettes.Values);
                db.Shapes.AddRange(data.Shapes.Values);
                db.Shape_CompatibilityTags.AddRange(data.Shape_CompatibilityTags.Values);
                db.Strokes.AddRange(data.Strokes.Values);
                db.Templates.AddRange(data.Templates.Values);
                db.SaveChanges();                

                return data;
            }
        }
    }
}
