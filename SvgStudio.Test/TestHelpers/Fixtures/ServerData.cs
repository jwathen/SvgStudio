using SvgStudio.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Test.TestHelpers.Fixtures
{
    public class ServerData
    {
        public Dictionary<string, CompatibilityTag> CompatibilityTags { get; set; }
        public Dictionary<string, License> Licenses { get; set; }
        public Dictionary<string, Shape> Shapes { get; set; }
        public Dictionary<string, MarkupFragment> MarkupFragments { get; set; }
        public Dictionary<string, ContentLicense> ContentLicenses { get; set; }
        public Dictionary<string, Stroke> Strokes { get; set; }
        public Dictionary<string, Fill> Fills { get; set; }
        public Dictionary<string, Palette> Palettes { get; set; }
        public Dictionary<string, Design> Designs { get; set; }

        public static ServerData CreateFixtures()
        {
            using (var fs = File.OpenRead(@"TestHelpers\Fixtures\ServerData.yaml"))
            using (var reader = new StreamReader(fs))
            {
                try
                {
                    var yaml = new YamlDotNet.Serialization.Deserializer();
                    yaml.RegisterTagMapping("tag:yaml.org,2002:BasicShape", typeof(BasicShape));
                    yaml.RegisterTagMapping("tag:yaml.org,2002:SolidColorFill", typeof(SolidColorFill));
                    ServerData data = yaml.Deserialize<ServerData>(reader);
                    return data;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
