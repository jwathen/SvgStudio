using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class CompatibilityTag : ISyncableEntity<CompatibilityTagDto>
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public string Tag { get; set; }

        public ICollection<Shape> Shapes { get; set; }
        public ICollection<DesignRegion> DesignRegions { get; set; }

        public CompatibilityTagDto ToDto()
        {
            return new CompatibilityTagDto
            {
                Id = this.Id,
                RowVersion = HexHelper.ByteArrayToHexString(this.RowVersion),
                Tag = this.Tag
            };
        }
    }
}