using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class ContentLicense
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public int LicenseId { get; set; }
        public string ContentUrl { get; set; }
        public string AttributionUrl { get; set; }
        public string AttributionName { get; set; }

        public License License { get; set; }
    }
}