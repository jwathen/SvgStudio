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
        public string Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool AttributionRequired { get; set; }
        public string LicenseName { get; set; }
        public string LicenseUrl { get; set; }
    }
}
