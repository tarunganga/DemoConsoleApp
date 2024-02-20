using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace DemoConsoleApp
{
    internal class PurchaseOrderClient
    {
        private readonly HttpClient _poClient;
        private readonly TokenHandler _tokenHandler;

        public PurchaseOrderClient(IConfiguration configuration)
        {
            _poClient = new HttpClient
            {
                BaseAddress = new Uri(configuration["FactoringPoAPI:ApiUrl"])
            };

            _tokenHandler = new TokenHandler(configuration);
        }


        public async Task<string> GetPurchaseOrder(Guid poId)
        {
            var tokenResponse = await _tokenHandler.GetToken();
            _poClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
            var response = await _poClient.GetAsync($"/factoringpomanagement/v1/internal/purchaseorders/{poId}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
