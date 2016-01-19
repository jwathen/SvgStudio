using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public static class UniqueId
    {
        public static string Generate()
        {
            DateTime start = new DateTime(2016, 1, 1);
            string ticks = (DateTime.UtcNow.Ticks - start.Ticks).ToString();
            string guid = Guid.NewGuid().ToString("n").Substring(0, 31 - ticks.Length);
            return ticks + "-" + guid;
        }
    }
}