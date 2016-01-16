using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Entities
{
    public class ContentLicenseDto
    {
        public int Id { get; set; }
        public string RowVersion { get; set; }
        public int LicenseId { get; set; }
        public string ContentUrl { get; set; }
        public string AttributionUrl { get; set; }
        public string AttributionName { get; set; }
    }
}
