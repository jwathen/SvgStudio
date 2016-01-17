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
    public class Palette : ISyncableEntity<PaletteDto>
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string RowVersion { get; set; }
        public string Name { get; set; }
        public short SortOrder { get; set; }

        public void FillFromDto(PaletteDto dto)
        {
            this.Id = EntityId.FromServerId(dto.Id).ToString();
            this.RowVersion = dto.RowVersion;
            this.Name = dto.Name;
            this.SortOrder = dto.SortOrder;
        }
    }
}
