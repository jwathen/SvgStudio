using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class License : ISyncableEntity<LicenseDto>
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool AttributionRequired { get; set; }
        public string LicenseName { get; set; }
        public string LicenseUrl { get; set; }

        public ICollection<ContentLicense> ContentLicenses { get; set; }

        public LicenseDto ToDto()
        {
            return new LicenseDto
            {
                Id = this.Id,
                RowVersion = HexHelper.ByteArrayToHexString(this.RowVersion),
                AttrributionRequired = this.AttributionRequired,
                LicenseName = this.LicenseName,
                LicenseUrl = this.LicenseUrl
            };
        }
    }
}