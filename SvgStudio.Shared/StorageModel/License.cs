using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class License : ISyncableRecord
    {
        [PrimaryKey]
        public virtual string Id { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual bool AttributionRequired { get; set; }
        public virtual string LicenseName { get; set; }
        public virtual string LicenseUrl { get; set; }

        [Ignore, JsonIgnore]
        public virtual ICollection<ContentLicense> ContentLicenses { get; set; }
    }
}
