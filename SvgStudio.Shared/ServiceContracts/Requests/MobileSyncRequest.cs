using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Requests
{
    public class MobileSyncRequest
    {
        public MobileSyncRequest()
        {
            LicenseRowVersions = new Dictionary<string, string>();
        }

        public Dictionary<string, string> LicenseRowVersions { get; set; }
    }
}
