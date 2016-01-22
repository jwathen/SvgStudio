using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class Shape : ISyncableRecord
    { 
        [PrimaryKey]
        public string Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }
        public ShapeType ShapeType { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int NumberOfFillsSupported { get; set; }
        public int NumberOfStrokesSupported { get; set; }
        public short SortOrder { get; set; }

        [Indexed]
        public string BasicShape_MarkupFragmentId { get; set; }

        [Indexed]
        public string TemplateShape_TemplateId { get; set; }
        [Indexed]
        public string TemplateShape_ClipPathMarkupFragmentId { get; set; }

        [Ignore, JsonIgnore]
        public ICollection<Shape_CompatibilityTag> Shape_CompatibilityTags { get; set; }
        [Ignore, JsonIgnore]
        public ICollection<ContentLicense> ContentLicenses { get; set; }
        [Ignore, JsonIgnore]
        public ICollection<Design> Designs { get; set; }
        [Ignore, JsonIgnore]
        public MarkupFragment BasicShape_MarkupFragment { get; set; }
        [Ignore, JsonIgnore]
        public MarkupFragment TemplateShape_ClipPathMarkupFragment { get; set; }
        [Ignore, JsonIgnore]
        public Template TemplateShape_Template { get; set; }
    }
}
