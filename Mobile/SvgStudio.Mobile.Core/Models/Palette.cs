using System;
using System.Collections.Generic;
using System.Linq;

namespace SvgStudio.Mobile.Core.Models
{
    public class Palette
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }

        public ICollection<Stroke> Strokes { get; set; }
        public ICollection<Fill> Fill { get; set; }
    }
}