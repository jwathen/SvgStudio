using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvgStudio.Shared.ServiceContracts.Requests;
using SvgStudio.Shared.ServiceContracts.Responses;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SvgStudio.Mobile.Core
{
    public class MobileServiceGateway
    {
        private readonly Uri _baseUrl;

        public MobileServiceGateway(string baseUrl)
        {
            _baseUrl = new Uri(baseUrl);
        }

        public async Task<string> GetVersion()
        {
            using (var http = CreateHttpClient())
            {
                var response = await http.GetAsync("mobile/getversion");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<string>();
                }
            }

            return null;
        }

        public async Task<MobileSyncResponse> Sync(MobileSyncRequest request)
        {
            using (var http = CreateHttpClient())
            {
                var dict = new Dictionary<string, object>();
                dict["request"] = request;
                var json = JsonConvert.SerializeObject(dict);
                var response = await http.PostAsync("mobile/sync", new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<MobileSyncResponse>();
                }
            }

            return null;
        }

        private HttpClient CreateHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = _baseUrl;
            return client;
        }
    }
}
