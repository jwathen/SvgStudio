using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Entities
{
    public class TemplateDto
    {
        public int Id { get; set; }
        public string RowVersion { get; set; }
        public bool IsMaster { get; set; }
        public string Name { get; set; }
    }
}
