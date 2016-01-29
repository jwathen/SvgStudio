using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class MarkupFragment : ISyncableRecord
    {
        [PrimaryKey]
        public virtual string Id { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual string Content { get; set; }

        [Ignore, JsonIgnore]
        public virtual ICollection<Shape> BasicShapes { get; set; }
        [Ignore, JsonIgnore]
        public virtual ICollection<Shape> TemplateShapeClipPaths { get; set; }
    }
}
