using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SvgStudio.Shared.ServiceContracts.Entities;

namespace SvgStudio.Web.Models
{
    public class SolidColorFill : Fill
    {
        public string Color { get; set; }

        public override FillDto ToDto()
        {
            FillDto dto = base.ToDto();
            dto.SolidColorFill_Color = Color;
            return dto;
        }
    }
}