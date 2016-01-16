using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Entities
{
    public class LicenseDto
    {
        public int Id { get; set; }
        public string RowVersion { get; set; }        
        public bool AttrributionRequired { get; set; }
        public string LicenseName { get; set; }
        public string LicenseUrl { get; set; }
    }
}
