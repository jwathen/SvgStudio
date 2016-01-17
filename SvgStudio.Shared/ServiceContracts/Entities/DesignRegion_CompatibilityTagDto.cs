﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Entities
{
    public class DesignRegion_CompatibilityTagDto
    {
        public int CompatibilityTagId { get; set; }
        public int DesignRegionId { get; set; }

        public string GetUniqueId()
        {
            return string.Format("{0}-{1}", CompatibilityTagId, DesignRegionId);
        }
    }
}