using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class DesignRegion : ISyncableEntity<DesignRegionDto>
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public short SortOrder { get; set; }

        public int TemplateId { get; set; }

        public Template Template { get; set; }

        public ICollection<CompatibilityTag> CompatibilityTags { get; set; }

        public DesignRegionDto ToDto()
        {
            return new DesignRegionDto
            {
                Id = this.Id,
                RowVersion = HexHelper.ByteArrayToHexString(this.RowVersion),
                Name = this.Name,
                X = this.X,
                Y = this.Y,
                Width = this.Width,
                Height = this.Height,
                TemplateId = this.TemplateId,
                SortOrder = this.SortOrder,
                CompatibilityTagIds = this.CompatibilityTags.Select(x => x.Id).ToArray()
            };
        }
    }
}