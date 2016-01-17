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
    public class License : ISyncableEntity<LicenseDto>
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string RowVersion { get; set; }
        public bool AttrributionRequired { get; set; }
        public string LicenseName { get; set; }
        public string LicenseUrl { get; set; }

        public void FillFromDto(LicenseDto dto)
        {
            Id = EntityId.FromServerId(dto.Id).ToString();
            RowVersion = dto.RowVersion;
            AttrributionRequired = dto.AttrributionRequired;
            LicenseName = dto.LicenseName;
            LicenseUrl = dto.LicenseUrl;
        }
    }
}
