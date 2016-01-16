using SQLite.Net.Attributes;
using SvgStudio.Mobile.Core.Models.Synchronization;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SvgStudio.Mobile.Core.Models
{
    public class DesignRegion : ISyncableEntity<DesignRegionDto>
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string RowVersion { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        [Indexed]
        public string TemplateId { get; set; }

        public void FillFromDto(DesignRegionDto dto)
        {
            Id = EntityId.FromServerId(dto.Id).ToString();
            RowVersion = dto.RowVersion;
            Name = dto.Name;
            X = dto.X;
            Y = dto.Y;
            Width = dto.Width;
            Height = dto.Height;
            TemplateId = EntityId.FromServerId(dto.TemplateId).ToString();
        }
    }
}