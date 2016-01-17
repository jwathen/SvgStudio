using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public abstract class Shape : ISyncableEntity<ShapeDto>
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int NumberOfFillsSupported { get; set; } = 1;
        public int NumberOfStrokesSupported { get; set; } = 1;
        public short SortOrder { get; set; }

        public ICollection<CompatibilityTag> CompatibilityTags { get; set; }

        public virtual ShapeDto ToDto()
        {
            return new ShapeDto
            {
                Id = this.Id,
                RowVersion = HexHelper.ByteArrayToHexString(this.RowVersion),
                Name = this.Name,
                Width = this.Width,
                Height =this.Height,
                NumberOfFillsSupported = this.NumberOfFillsSupported,
                NumberOfStrokesSupported = this.NumberOfStrokesSupported,
                SortOrder = this.SortOrder,
                CompatibilityTagIds = this.CompatibilityTags.Select(x => x.Id).ToArray()
            };
        }
    }
}