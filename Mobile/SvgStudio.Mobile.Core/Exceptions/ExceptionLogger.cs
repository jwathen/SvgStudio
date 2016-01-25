using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Mobile.Core.Exceptions
{
    public class ExceptionLogger : IExceptionLogger
    {
        public static IExceptionLogger Current
        {
            get
            {
                return new ExceptionLogger();
            }
        }

        public void Log(Exception ex)
        {
            // TODO
        }
    }
}
