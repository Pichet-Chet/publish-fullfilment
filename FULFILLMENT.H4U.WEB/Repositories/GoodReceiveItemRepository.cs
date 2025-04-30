using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT_H4U.Helper;
using FULFILLMENT_H4U.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
namespace FULFILLMENT_H4U.Repositories
{
    public class GoodReceiveItemRepository : IGoodReceiveItemRepository
    {
        IConfiguration _configuration;

        private string _domain = string.Empty;

        HttpClientHandler clientHandler = new HttpClientHandler();

        FilterHelper _filter = new FilterHelper();

        public GoodReceiveItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            _domain = _configuration["API"];
        }

        public async Task<Response> GET_ALL_BY_HEADER(int headerId)
        {
            Response resp = new Response();

            List<ViewGoodReceiveItemModel> listData = new List<ViewGoodReceiveItemModel>();

            try
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(_domain + $"api/apiGoodReceiveItem/GET_ALL_BY_HEADER?headerId={headerId}");

                HttpResponseMessage response = await Task.Run(() => client.GetAsync(client.BaseAddress));

                if (response.IsSuccessStatusCode)
                {
                    string respData = response.Content.ReadAsStringAsync().Result;

                    var apiResponse = JsonConvert.DeserializeObject<Response>(respData);

                    resp = apiResponse;

                    if (resp.status == true)
                    {
                        if (resp.message != Constants.DATA_NOT_FOUND)
                        {
                            listData = JsonConvert.DeserializeObject<List<ViewGoodReceiveItemModel>>(apiResponse.output_data.ToString());

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

        public async Task<Response> GET_DETAIL(int id)
        {
            Response resp = new Response();

            ViewGoodReceiveItemModel listData = new ViewGoodReceiveItemModel();

            try
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(_domain + $"api/apiGoodReceiveItem/detail?id={id}");

                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);

                if (response.IsSuccessStatusCode)
                {
                    string respData = response.Content.ReadAsStringAsync().Result;

                    var apiResponse = JsonConvert.DeserializeObject<Response>(respData);

                    resp = apiResponse;

                    if (resp.status == true)
                    {
                        if (resp.message != Constants.DATA_NOT_FOUND)
                        {
                            listData = JsonConvert.DeserializeObject<ViewGoodReceiveItemModel>(apiResponse.output_data.ToString());

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

