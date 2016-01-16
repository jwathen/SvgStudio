using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Entities
{
    public class ServerEntityVersion
    {
        public int ServerId { get; set; }
        public string RowVersion { get; set; }
    }
}
