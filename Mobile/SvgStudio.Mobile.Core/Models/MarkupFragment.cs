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
    public class MarkupFragment : ISyncableEntity<MarkupFragmentDto>
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string RowVersion { get; set; }
        public string Content { get; set; }

        public void FillFromDto(MarkupFragmentDto dto)
        {
            this.Id = EntityId.FromServerId(dto.Id).ToString();
            this.RowVersion = dto.RowVersion;
            this.Content = dto.Content;
        }
    }
}
