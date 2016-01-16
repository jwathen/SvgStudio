using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class Stroke : ISyncableEntity<StrokeDto>
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public string Color { get; set; }
        public int Width { get; set; }
        public string DashArray { get; set; }

        public int? PaletteId { get; set; }

        public Palette Palette { get; set; }

        public StrokeDto ToDto()
        {
            return new StrokeDto
            {
                Id = this.Id,
                RowVersion = HexHelper.ByteArrayToHexString(this.RowVersion),
                Color = this.Color,
                Width = this.Width,
                DashArray = this.DashArray,
                PaletteId = this.PaletteId
            };
        }
    }
}