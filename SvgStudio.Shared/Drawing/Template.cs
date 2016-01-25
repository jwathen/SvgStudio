using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.Drawing
{
    public class Template
    {
        public Template()
        {
            DesignRegions = new List<DesignRegion>();
        }

        public string StorageId { get; set; }
        public string Name { get; set; }
        public List<DesignRegion> DesignRegions { get; private set; }

        public int CalculateWidth()
        {
            DesignRegion farLeft = DesignRegions.OrderBy(x => x.X).First();
            DesignRegion farRight = DesignRegions.OrderByDescending(x => x.X + x.Width).First();

            int min = farLeft.X;
            int padding = min;

            int max = farRight.X + padding + farRight.Width;

            return max;
        }

        public int CalculateHeight()
        {
            DesignRegion top = DesignRegions.OrderBy(x => x.Y).First();
            DesignRegion bottom = DesignRegions.OrderByDescending(x => x.Y + x.Height).First();

            int min = top.Y;
            int padding = min;

            int max = bottom.Y + padding + bottom.Height;

            return max;
        }
    }
}
