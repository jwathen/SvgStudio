using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.Drawing
{
    public class Template
    {
        public string Name { get; set; }
        public List<DesignRegion> DesignRegions { get; private set; } = new List<DesignRegion>();
    }
}
