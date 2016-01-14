using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Responses
{
    public class EntityChangeData<T>
    {
        public List<T> Added { get; set; }
        public List<T> Updated { get; set; }
        public List<int> Deleted { get; set; }
    }
}
