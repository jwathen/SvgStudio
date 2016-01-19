using SQLite.Net.Attributes;
using SvgStudio.Mobile.Core.Models.Synchronization;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Mobile.Core.Models
{
    public class Stroke : ISyncableEntity<StrokeDto>
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string RowVersion { get; set; }
        public string Color { get; set; }
        public int Width { get; set; }
        public string DashArray { get; set; }
        public string PaletteId { get; set; }

        public void FillFromDto(StrokeDto dto)
        {
            this.Id = EntityId.FromServerId(dto.Id).ToString();
            this.RowVersion = dto.RowVersion;
            this.Color = dto.Color;
            this.Width = dto.Width;
            this.DashArray = dto.DashArray;
            this.PaletteId = EntityId.FromServerId(dto.PaletteId).ToString();
        }
    }
}
