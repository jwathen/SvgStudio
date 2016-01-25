using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Mobile.Core.Exceptions
{
    public interface IExceptionLogger
    {
        void Log(Exception ex);
    }
}
