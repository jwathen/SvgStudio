using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SvgStudio.Shared.ServiceContracts.Entities;

namespace SvgStudio.Web.Models
{
    public class BasicShape : Shape
    {
        public int MarkupFragmentId { get; set; }

        public MarkupFragment MarkupFragment { get; set; }

        public override ShapeDto ToDto()
        {
            ShapeDto dto = base.ToDto();
            dto.BasicShape_MarkupFragmentId = this.MarkupFragmentId;
            return dto;
        }
    }
}