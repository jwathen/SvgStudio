using SQLite.Net.Attributes;
using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Mobile.Core.Models.Synchronization
{
    public interface ISyncableEntity<TDto>
    {
        string Id { get; }

        string RowVersion { get; }

        void FillFromDto(TDto dto);
    }
}
