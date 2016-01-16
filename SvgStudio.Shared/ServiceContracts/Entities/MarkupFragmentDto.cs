using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Entities
{
    public class MarkupFragmentDto
    {
        public int Id { get; set; }
        public string RowVersion { get; set; }
        public string Content { get; set; }
    }
}
