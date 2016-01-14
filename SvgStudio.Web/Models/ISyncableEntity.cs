using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public interface ISyncableEntity<TDto>
    {
        SyncableEntityId SyncableEntityId { get; }

        TDto ToDto();
    }
}