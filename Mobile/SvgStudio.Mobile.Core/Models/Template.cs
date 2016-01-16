using SQLite.Net.Attributes;
using SvgStudio.Mobile.Core.Models;
using SvgStudio.Mobile.Core.Models.Synchronization;
using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SvgStudio.Mobile.Core.Models
{
    public class Template : ISyncableEntity<TemplateDto>
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string RowVersion { get; set; }
        public string Name { get; set; }

        public ServerEntityVersion GetServerEntityVersion()
        {
            int serverId = int.Parse(EntityId.Parse(Id).SourceId);
            return new ServerEntityVersion
            {
                ServerId = serverId,
                RowVersion = RowVersion
            };
        }

        public void FillFromDto(TemplateDto dto)
        {
            Id = EntityId.FromServerId(dto.Id).ToString();
            RowVersion = dto.RowVersion;
            Name = dto.Name;
        }
    }
}