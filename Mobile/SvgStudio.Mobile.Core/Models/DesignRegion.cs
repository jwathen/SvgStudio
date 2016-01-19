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
        public short SortOrder { get; set; }

        public void FillFromDto(DesignRegionDto dto)
        {
            this.Id = EntityId.FromServerId(dto.Id).ToString();
            this.RowVersion = dto.RowVersion;
            this.Name = dto.Name;
            this.X = dto.X;
            this.Y = dto.Y;
            this.Width = dto.Width;
            this.Height = dto.Height;
            this.TemplateId = EntityId.FromServerId(dto.TemplateId).ToString();
            this.SortOrder = dto.SortOrder;
        }
    }
}
