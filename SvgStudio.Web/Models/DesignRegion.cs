using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class DesignRegion
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int TemplateId { get; set; }

        public Template Template { get; set; }

        public ICollection<Shape> CompatibleShapes { get; set; }
    }
}