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
    public class Shape : ISyncableEntity<ShapeDto>
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string RowVersion { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int NumberOfFillsSupported { get; set; }
        public int NumberOfStrokesSupported { get; set; }
        public short SortOrder { get; set; }

        [Indexed]
        public string BasicShape_MarkupFragmentId { get; set; }

        [Indexed]
        public string TemplateShape_TemplateId { get; set; }
        [Indexed]
        public string TemplateShape_ClipPathMarkupFragmentId { get; set; }

        [Ignore]
        public int[] CompatibilityTagIds { get; set; }

        public void FillFromDto(ShapeDto dto)
        {
            this.Id = EntityId.FromServerId(dto.Id).ToString();
            this.RowVersion = dto.RowVersion;
            this.Name = dto.Name;
            this.Width = dto.Width;
            this.Height = dto.Height;
            this.NumberOfFillsSupported = dto.NumberOfFillsSupported;
            this.NumberOfStrokesSupported = dto.NumberOfStrokesSupported;
            this.SortOrder = dto.SortOrder;
            this.BasicShape_MarkupFragmentId = EntityId.FromServerId(dto.BasicShape_MarkupFragmentId).ToString();
            this.TemplateShape_TemplateId = EntityId.FromServerId(dto.TemplateShape_TemplateId).ToString();
            this.TemplateShape_ClipPathMarkupFragmentId = EntityId.FromServerId(dto.TemplateShape_ClipPathMarkupFragmentId).ToString();
            this.CompatibilityTagIds = dto.CompatibilityTagIds;
        }
    }
}
