using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Entities
{
    public class StrokeDto
    {
        public int Id { get; set; }
        public string RowVersion { get; set; }
        public string Color { get; set; }
        public int Width { get; set; }
        public string DashArray { get; set; }
        public int? PaletteId { get; set; }
    }
}
