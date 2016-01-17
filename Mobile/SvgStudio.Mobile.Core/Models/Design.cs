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
    public class Design : ISyncableEntity<DesignDto>
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string RowVersion { get; set; }
        [Indexed]
        public string ShapeId { get; set; }
        [Indexed]
        public string PaletteId { get; set; }

        public void FillFromDto(DesignDto dto)
        {
            this.Id = EntityId.FromServerId(dto.Id).ToString();
            this.RowVersion = dto.RowVersion;
            this.ShapeId = EntityId.FromServerId(dto.ShapeId).ToString();
            this.PaletteId = EntityId.FromServerId(dto.PaletteId).ToString();
        }
    }
}
