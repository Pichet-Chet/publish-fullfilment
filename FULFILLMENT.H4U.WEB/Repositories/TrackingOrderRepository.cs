using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT_H4U.Helper;
using FULFILLMENT_H4U.Repositories.Interfaces;
using Newtonsoft.Json;


namespace FULFILLMENT_H4U.Repositories
{
    public class TrackingOrderRepository : ITrackingOrderRepository
    {
        IConfiguration _configuration;

        private string _domain = string.Empty;

        HttpClientHandler clientHandler = new HttpClientHandler();

        FilterHelper _filter = new FilterHelper();

        public TrackingOrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            _domain = _configuration["API"];
        }


        public async Task<Response> GET_ALL(FilterModel param)
        {
            Response resp = new Response();

            ViewTrackingOrderModel listData = new ViewTrackingOrderModel();

            try
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                string filterString = await Task.Run(() => _filter.filter(param));

                client.BaseAddress = new Uri(_domain + $"api/apitrackingorder/get_all?{filterString}");

                HttpResponseMessage response = await Task.Run(() => client.GetAsync(client.BaseAddress));

                if (response.IsSuccessStatusCode)
                {
                    string respData = await Task.Run(() => response.Content.ReadAsStringAsync());

                    resp = JsonConvert.DeserializeObject<Response>(respData);

                    if (resp.status == true)
                    {
                        if (resp.message != Constants.DATA_NOT_FOUND)
                        {
                            listData = JsonConvert.DeserializeObject<ViewTrackingOrderModel>(resp.output_data.ToString());

                            if (listData != null)
                            {
                                resp.output_data = listData;
                            }
                        }
                    }
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
