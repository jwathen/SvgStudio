using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.ServiceContracts.Responses
{
    public class JoiningTableChangeData<T>
    {
        public JoiningTableChangeData()
        {
            Added = new List<T>();
            Deleted = new List<T>();
        }

        public List<T> Added { get; set; }
        public List<T> Deleted { get; set; }
    }
}
