using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Mobile.Core.Models.Synchronization
{
    public class TableSynchronizationSummary
    {
        public string TableName { get; set; }
        public int RowsAdded { get; set; }
        public int RowsUpdated { get; set; }
        public int RowsDeleted { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1} added, {2} updated, {3} deleted", TableName, RowsAdded, RowsUpdated, RowsDeleted);
        }
    }
}
