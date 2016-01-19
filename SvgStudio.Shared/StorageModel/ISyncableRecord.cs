using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public interface ISyncableRecord
    {
        string Id { get; }
        byte[] RowVersion { get; }
    }
}
