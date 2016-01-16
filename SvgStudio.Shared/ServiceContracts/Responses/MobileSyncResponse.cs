using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Responses
{
    public class MobileSyncResponse
    {
        public EntityChangeData<TemplateDto> TemplateChanges { get; set; }
    }
}
