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
    public class Fill : ISyncableEntity<FillDto>
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string RowVersion { get; set; }
        [Indexed]
        public string PaletteId { get; set; }

        public string SolidColorFill_Color { get; set; }

        public string PatternFill_Name { get; set; }
        public int PatternFill_X { get; set; }
        public int PatternFill_Y { get; set; }
        public double PatternFill_Width { get; set; }
        public double PatternFill_Height { get; set; }
        public string PatternFill_PatternUnits { get; set; }
        public string PatternFill_PatternContentUnits { get; set; }
        [Indexed]
        public string PatternFill_DesignId { get; set; }

        public void FillFromDto(FillDto dto)
        {
            this.Id = EntityId.FromServerId(dto.Id).ToString();
            this.RowVersion = dto.RowVersion;
            this.PaletteId = EntityId.FromServerId(dto.PaletteId).ToString();
            this.SolidColorFill_Color = dto.SolidColorFill_Color;
            this.PatternFill_Name = dto.PatternFill_Name;
            this.PatternFill_X = dto.PatternFill_X;
            this.PatternFill_Y = dto.PatternFill_Y;
            this.PatternFill_Width = dto.PatternFill_Width;
            this.PatternFill_Height = dto.PatternFill_Height;
            this.PatternFill_PatternUnits = dto.PatternFill_PatternUnits;
            this.PatternFill_PatternContentUnits = dto.PatternFill_PatternContentUnits;
            this.PatternFill_DesignId = EntityId.FromServerId(dto.PatternFill_DesignId).ToString();
        }
    }
}
