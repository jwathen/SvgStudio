using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public abstract class Fill : ISyncableEntity<FillDto>
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }

        public int? PaletteId { get; set; }

        public Palette Palette { get; set; }

        public virtual FillDto ToDto()
        {
            return new FillDto
            {
                Id = this.Id,
                RowVersion = HexHelper.ByteArrayToHexString(this.RowVersion),
                PaletteId = this.PaletteId
            };
        }
    }
}