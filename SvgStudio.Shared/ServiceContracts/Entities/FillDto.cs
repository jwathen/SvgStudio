using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Entities
{
    public class FillDto
    {
        public int Id { get; set; }
        public string RowVersion { get; set; }
        public int? PaletteId { get; set; }

        public string SolidColorFill_Color { get; set; }

        public string PatternFill_Name { get; set; }
        public int PatternFill_X { get; set; }
        public int PatternFill_Y { get; set; }
        public double PatternFill_Width { get; set; }
        public double PatternFill_Height { get; set; }
        public string PatternFill_PatternUnits { get; set; }
        public string PatternFill_PatternContentUnits { get; set; }
        public int PatternFill_DesignId { get; set; }
    }
}
