using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class ContentLicense : ISyncableRecord
    {
        [PrimaryKey]
        public string Id { get; set; }
        public byte[] RowVersion { get; set; }
        [Indexed]
        public string LicenseId { get; set; }
        [Indexed]
        public string ShapeId { get; set; }
        public string ContentUrl { get; set; }
        public string AttributionUrl { get; set; }
        public string AttributionName { get; set; }
    }
}
