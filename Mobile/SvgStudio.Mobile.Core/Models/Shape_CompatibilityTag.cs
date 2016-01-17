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
    public class Shape_CompatibilityTag
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [Indexed(Name = "IX_Shape_CompatibilityTag_X", Order = 1)]
        public string CompatibilityTagId { get; set; }
        [Indexed(Name = "IX_Shape_CompatibilityTag_X", Order = 2)]
        public string ShapeId { get; set; }

        public void FillFromDto(Shape_CompatibilityTagDto dto)
        {
            this.CompatibilityTagId = EntityId.FromServerId(dto.CompatibilityTagId).ToString();
            this.ShapeId = EntityId.FromServerId(dto.ShapeId).ToString();
        }
    }
}
