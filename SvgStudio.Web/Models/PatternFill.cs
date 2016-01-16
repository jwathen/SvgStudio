using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SvgStudio.Shared.ServiceContracts.Entities;

namespace SvgStudio.Web.Models
{
    public class PatternFill : Fill
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string PatternUnits { get; set; }
        public string PatternContentUnits { get; set; }
        public int DesignId { get; set; }

        public Design Design { get; set; }

        public override FillDto ToDto()
        {
            FillDto dto = base.ToDto();
            dto.PatternFill_Name = this.Name;
            dto.PatternFill_X = this.X;
            dto.PatternFill_Y = this.Y;
            dto.PatternFill_Width = this.Width;
            dto.PatternFill_Height = this.Height;
            dto.PatternFill_PatternUnits = this.PatternUnits;
            dto.PatternFill_PatternContentUnits = this.PatternContentUnits;
            dto.PatternFill_DesignId = this.DesignId;
            return dto;
        }
    }
}