using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public class RenderDesignResult : IDefProvider
    {
        public DefinitionCollection Defs { get; set; }

        public RenderDesignResult()
        {
            Defs = new DefinitionCollection();
        }

        public XElement Xml { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string ClipPath { get; set; }

        public DefinitionCollection GetDefs()
        {
            return Defs;
        }
    }
}
