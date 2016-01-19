using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Entities
{
    public class ShapeDto
    {
        public int Id { get; set; }
        public string RowVersion { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int NumberOfFillsSupported { get; set; }
        public int NumberOfStrokesSupported { get; set; }
        public short SortOrder { get; set; }

        public int BasicShape_MarkupFragmentId { get; set; }

        public int TemplateShape_TemplateId { get; set; }
        public int? TemplateShape_ClipPathMarkupFragmentId { get; set; }
    }
}
