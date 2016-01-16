using SvgStudio.Shared.ServiceContracts.Requests;
using SvgStudio.Shared.ServiceContracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Mobile.Core.Services
{
    public interface IMobileServiceGateway
    {
        Task<string> GetVersion();
        Task<MobileSyncResponse> Sync(MobileSyncRequest request);
    }
}
