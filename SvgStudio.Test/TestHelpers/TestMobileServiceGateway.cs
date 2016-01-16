using SvgStudio.Mobile.Core.Services;
using SvgStudio.Shared.ServiceContracts.Requests;
using SvgStudio.Shared.ServiceContracts.Responses;
using SvgStudio.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Test.TestHelpers
{
    public class TestMobileServiceGateway : IMobileServiceGateway
    {
        public Task<string> GetVersion()
        {
            throw new NotImplementedException();
        }

        public async Task<MobileSyncResponse> Sync(MobileSyncRequest request)
        {
            var mobileController = new MobileController();
            var jsonResult = await mobileController.Sync(request);
            return (MobileSyncResponse)jsonResult.Data;
        }
    }
}
