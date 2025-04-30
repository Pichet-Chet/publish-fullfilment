using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT_H4U.Repositories.Interfaces;
using Newtonsoft.Json;

namespace FULFILLMENT_H4U.Repositories
{
    public class SysAccessLogRepository : ISysAccessLogRepository
    {
        IConfiguration _configuration;

        private string _domain = string.Empty;

        HttpClientHandler clientHandler = new HttpClientHandler();

        public SysAccessLogRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            _domain = _configuration["API"];
        }

        public async Task<Response> INSERT(SysAccess param)
        {
            Response resp = new Response();

            try
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(_domain + $"api/apiSysAccessLog/INSERT");

                HttpResponseMessage response = await Task.Run(() => client.PostAsJsonAsync(client.BaseAddress, param));

                if (response.IsSuccessStatusCode)
                {
                    string respData = response.Content.ReadAsStringAsync().Result;

                    var apiResponse = JsonConvert.DeserializeObject<Response>(respData);

                    resp = apiResponse;
                }
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
                resp.inner_exception = ex.InnerException == null ? "" : ex.InnerException.ToString();
            }

            return resp;
        }

    }
}
