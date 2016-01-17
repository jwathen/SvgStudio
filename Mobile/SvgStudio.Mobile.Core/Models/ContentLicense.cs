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
    public class ContentLicense : ISyncableEntity<ContentLicenseDto>
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string RowVersion { get; set; }
        public string LicenseId { get; set; }
        public string ContentUrl { get; set; }
        public string AttributionUrl { get; set; }
        public string AttributionName { get; set; }

        public void FillFromDto(ContentLicenseDto dto)
        {
            this.Id = EntityId.FromServerId(dto.Id).ToString();
            this.RowVersion = dto.RowVersion;
            this.LicenseId = EntityId.FromServerId(dto.LicenseId).ToString();
            this.ContentUrl = dto.ContentUrl;
            this.AttributionUrl = dto.AttributionUrl;
            this.AttributionName = dto.AttributionName;
        }
    }
}
