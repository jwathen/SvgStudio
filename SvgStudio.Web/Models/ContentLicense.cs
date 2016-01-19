using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class ContentLicense : ISyncableEntity<ContentLicenseDto>
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public int LicenseId { get; set; }
        public int? ShapeId { get; set; }
        public string ContentUrl { get; set; }
        public string AttributionUrl { get; set; }
        public string AttributionName { get; set; }

        public License License { get; set; }
        public Shape Shape { get; set; }

        public ContentLicenseDto ToDto()
        {
            return new ContentLicenseDto
            {
                Id = this.Id,
                RowVersion = HexHelper.ByteArrayToHexString(this.RowVersion),
                LicenseId = this.LicenseId,
                ShapeId = this.ShapeId,
                ContentUrl = this.ContentUrl,
                AttributionUrl = this.AttributionUrl,
                AttributionName = this.AttributionName
            };
        }
    }
}