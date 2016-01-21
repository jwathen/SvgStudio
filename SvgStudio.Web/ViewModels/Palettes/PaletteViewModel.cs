using SvgStudio.Shared.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.ViewModels.Palettes
{
    public class PaletteViewModel
    {
        public List<Stroke> Strokes { get; set; }
        public List<Fill> Fills { get; set; }
    }
}