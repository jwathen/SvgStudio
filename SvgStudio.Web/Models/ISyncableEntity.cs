using SvgStudio.Shared.ServiceContracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public interface ISyncableEntity<TDto>
    {
        ServerEntityVersion SyncableEntityId { get; }

        TDto ToDto();
    }
}