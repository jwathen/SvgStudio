using System;
using System.Collections.Generic;
using System.Linq;

namespace SvgStudio.Mobile.Core.Models
{
    public abstract class Shape
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int NumberOfFillsSupported { get; set; }
        public int NumberOfStrokesSupported { get; set; }
        public string Markup { get; set; }
        public string SourceUrl { get; set; }

        public ICollection<DesignRegion> CompatibleDesignRegions { get; set; }
    }
}