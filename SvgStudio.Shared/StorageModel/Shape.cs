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
        public virtual string Id { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual ShapeType ShapeType { get; set; }
        public virtual string Name { get; set; }
        public virtual double Width { get; set; }
        public virtual double Height { get; set; }
        public virtual int NumberOfFillsSupported { get; set; }
        public virtual int NumberOfStrokesSupported { get; set; }
        public virtual short SortOrder { get; set; }

        [Indexed]
        public virtual string BasicShape_MarkupFragmentId { get; set; }

        [Indexed]
        public virtual string TemplateShape_TemplateId { get; set; }
        [Indexed]
        public virtual string TemplateShape_ClipPathMarkupFragmentId { get; set; }

        [Ignore, JsonIgnore]
        public virtual ICollection<Shape_CompatibilityTag> Shape_CompatibilityTags { get; set; }
        [Ignore, JsonIgnore]
        public virtual ICollection<ContentLicense> ContentLicenses { get; set; }
        [Ignore, JsonIgnore]
        public virtual ICollection<Design> Designs { get; set; }
        [Ignore, JsonIgnore]
        public virtual MarkupFragment BasicShape_MarkupFragment { get; set; }
        [Ignore, JsonIgnore]
        public virtual MarkupFragment TemplateShape_ClipPathMarkupFragment { get; set; }
        [Ignore, JsonIgnore]
        public virtual Template TemplateShape_Template { get; set; }
    }
}
