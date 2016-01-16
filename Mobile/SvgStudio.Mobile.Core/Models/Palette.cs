using SQLite.Net.Attributes;
using SvgStudio.Mobile.Core.Models.Synchronization;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SvgStudio.Mobile.Core.Models
{
    public class Palette : ISyncableEntity<PaletteDto>
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string RowVersion { get; set; }
        public string Name { get; set; }

        public void FillFromDto(PaletteDto dto)
        {
            Id = EntityId.FromServerId(dto.Id).ToString();
            RowVersion = dto.RowVersion;
            Name = dto.Name;
        }
    }
}