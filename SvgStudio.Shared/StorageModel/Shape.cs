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
        public int Width { get; set; }
        public int Height { get; set; }
        public int NumberOfFillsSupported { get; set; }
        public int NumberOfStrokesSupported { get; set; }
        public short SortOrder { get; set; }

        [Indexed]
        public string BasicShape_MarkupFragmentId { get; set; }

        [Indexed]
        public string TemplateShape_TemplateId { get; set; }
        [Indexed]
        public string TemplateShape_ClipPathMarkupFragmentId { get; set; }
    }
}
