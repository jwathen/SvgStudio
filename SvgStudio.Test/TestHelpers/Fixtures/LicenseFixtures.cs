using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Test.TestHelpers.Fixtures
{
    public class LicenseFixtures
    {
        public static SvgStudio.Web.Models.License Server_EezyPremiumLicense()
        {
            return new SvgStudio.Web.Models.License
            {
                LicenseName = "Eezy Premium",
                AttributionRequired = false,
                LicenseUrl = "http://www.vecteezy.com/frequently-asked-questions"
            };
        }
    }
}
