using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class TemplateShape : Shape
    {
        public int TemplateId { get; set; }
        public int? ClipPathMarkupFragmentId { get; set; }

        public Template Template { get; set; }
        public MarkupFragment ClipPathMarkupFragment { get; set; }
    }
}