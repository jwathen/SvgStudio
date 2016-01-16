using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SvgStudio.Shared.ServiceContracts.Entities;

namespace SvgStudio.Web.Models
{
    public class TemplateShape : Shape
    {
        public int TemplateId { get; set; }
        public int? ClipPathMarkupFragmentId { get; set; }

        public Template Template { get; set; }
        public MarkupFragment ClipPathMarkupFragment { get; set; }

        public override ShapeDto ToDto()
        {
            ShapeDto dto = base.ToDto();
            dto.TemplateShape_TemplateId = this.TemplateId;
            dto.TemplateShape_ClipPathMarkupFragmentId = this.ClipPathMarkupFragmentId;
            return dto;
        }
    }
}