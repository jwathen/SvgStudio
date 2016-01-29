using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class FilledTemplateMomento
    {
        public virtual string TemplateId { get; set; }
        public virtual Dictionary<string, string> Designs { get; set; }
    }
}
