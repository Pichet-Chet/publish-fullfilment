using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT_H4U.Helper;
using FULFILLMENT_H4U.Repositories.Interfaces;
using Newtonsoft.Json;

namespace FULFILLMENT_H4U.Repositories
{
    public class PurchaseOrderItemRepository : IPurchaseOrderItemRepository
    {

        IConfiguration _configuration;

        private string _domain = string.Empty;

        HttpClientHandler clientHandler = new HttpClientHandler();

        FilterHelper _filter = new FilterHelper();

        public PurchaseOrderItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            _domain = _configuration["API"];
        }

        public async Task<Response> GET_ALL_BY_HEADER(int headerId)
        {
            Response resp = new Response();

            List<ViewPurchaseOrderItemModel> listData = new List<ViewPurchaseOrderItemModel>();

            try
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(_domain + $"api/apiPurchaseOrderItem/GET_ALL_BY_HEADER?headerId={headerId}");

                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;

                if (response.IsSuccessStatusCode)
                {
                    string respData = response.Content.ReadAsStringAsync().Result;

                    var apiResponse = JsonConvert.DeserializeObject<Response>(respData);

                    resp = apiResponse;

                    if (resp.status == true)
                    {
                        if (resp.message != Constants.DATA_NOT_FOUND)
                        {
                            listData = JsonConvert.DeserializeObject<List<ViewPurchaseOrderItemModel>>(apiResponse.output_data.ToString());

                            if (listData.Count > 0)
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

        public async Task<Response> GET_DETAIL(int id)
        {
            Response resp = new Response();

            ViewPurchaseOrderItemModel listData = new ViewPurchaseOrderItemModel();

            try
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(_domain + $"api/apiPurchaseOrderItem/detail?id={id}");

                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;

                if (response.IsSuccessStatusCode)
                {
                    string respData = response.Content.ReadAsStringAsync().Result;

                    var apiResponse = JsonConvert.DeserializeObject<Response>(respData);

                    resp = apiResponse;

                    if (resp.status == true)
                    {
                        listData = JsonConvert.DeserializeObject<ViewPurchaseOrderItemModel>(apiResponse.output_data.ToString());

                        if (listData != null)
                        {
                            resp.output_data = listData;
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
