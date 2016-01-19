using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class Palette : ISyncableEntity<PaletteDto>
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public short SortOrder { get; set; }

        public ICollection<Stroke> Strokes { get; set; }
        public ICollection<Fill> Fills { get; set; }

        public PaletteDto ToDto()
        {
            return new PaletteDto
            {
                Id = this.Id,
                RowVersion = HexHelper.ByteArrayToHexString(this.RowVersion),
                Name = this.Name,
                SortOrder = this.SortOrder
            };
        }
    }
}