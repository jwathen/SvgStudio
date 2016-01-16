using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class Design : ISyncableEntity<DesignDto>
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public int ShapeId { get; set; }
        public int PaletteId { get; set; }

        public Shape Shape { get; set; }
        public Palette Palette { get; set; }

        public DesignDto ToDto()
        {
            return new DesignDto
            {
                Id = this.Id,
                RowVersion = HexHelper.ByteArrayToHexString(this.RowVersion),
                ShapeId = this.ShapeId,
                PaletteId = this.PaletteId
            };
        }
    }
}