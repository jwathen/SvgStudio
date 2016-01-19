using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class CompatibilityTag : ISyncableRecord
    {
        [PrimaryKey]
        public string Id { get; set; }
        public byte[] RowVersion { get; set; }
        public string Tag { get; set; }
    }
}
