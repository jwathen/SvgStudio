using Newtonsoft.Json;
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
        public virtual string Id { get; set; }
        public virtual byte[] RowVersion { get; set; }
        [Indexed]
        public virtual string LicenseId { get; set; }
        [Indexed]
        public virtual string ShapeId { get; set; }
        public virtual string ContentUrl { get; set; }
        public virtual string AttributionUrl { get; set; }
        public virtual string AttributionName { get; set; }

        [Ignore, JsonIgnore]
        public virtual License License { get; set; }
        [Ignore, JsonIgnore]
        public virtual Shape Shape { get; set; }
    }
}
