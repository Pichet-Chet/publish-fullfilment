using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;
using FULFILLMENT_H4U.Helper;
using FULFILLMENT_H4U.Repositories.Interfaces;
using Newtonsoft.Json;

namespace FULFILLMENT_H4U.Repositories
{
    public class InboundHeaderRepository : IInboundHeaderRepository
    {
        IConfiguration _configuration;

        private string _domain = string.Empty;

        HttpClientHandler clientHandler = new HttpClientHandler();

        FilterHelper _filter = new FilterHelper();


        public InboundHeaderRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            _domain = _configuration["API"];
        }

        public async Task<Response> GET_ALL(FilterModel param)
        {
            Response resp = new Response();

            List<ViewInboundHeaderModel> listData = new List<ViewInboundHeaderModel>();

            try
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                string filterString = _filter.filter(param);

                client.BaseAddress = new Uri(_domain + $"api/apiInboundHeader/get_filter?{filterString}");

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
                            listData = JsonConvert.DeserializeObject<List<ViewInboundHeaderModel>>(apiResponse.output_data.ToString());

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

        public async Task<Response> GET_ALL_BY_VENDOR(int vendorId)
        {
            Response resp = new Response();

            List<ViewInboundHeaderModel> listData = new List<ViewInboundHeaderModel>();

            try
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(_domain + $"api/apiInboundHeader/GET_BY_VENDOR?vendorId={vendorId}");

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
                            listData = JsonConvert.DeserializeObject<List<ViewInboundHeaderModel>>(apiResponse.output_data.ToString());

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

            ViewInboundHeaderModel listData = new ViewInboundHeaderModel();

            try
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(_domain + $"api/apiInboundHeader/detail?id={id}");

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
                            listData = JsonConvert.DeserializeObject<ViewInboundHeaderModel>(apiResponse.output_data.ToString());

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

        public async Task<Response> GET_FILTER(FilterModel param)
        {
            Response resp = new Response();

            ViewInboundHeaderModel listData = new ViewInboundHeaderModel();

            try
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                string filterString = _filter.filter(param);


                client.BaseAddress = new Uri(_domain + $"api/apiInboundHeader/GET_FILTER?{filterString}");

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
                            listData = JsonConvert.DeserializeObject<ViewInboundHeaderModel>(apiResponse.output_data.ToString());

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

        public async Task<Response> INSERT(transactionInbound param)
        {
            Response resp = new Response();

            try
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(_domain + $"api/apiInboundHeader/INSERT");

                HttpResponseMessage response = client.PostAsJsonAsync(client.BaseAddress, param).Result;

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

        public async Task<Response> UPDATE(InboundHeader param)
        {
            Response resp = new Response();

            try
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(_domain + $"api/apiInboundHeader/update");

                HttpResponseMessage response = await client.PutAsJsonAsync(client.BaseAddress, param);

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

        public async Task<Response> ARRIVED_TIME(InboundHeader param)
        {
            Response resp = new Response();

            try
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(_domain + $"api/apiInboundHeader/arrived_time");

                HttpResponseMessage response = await client.PutAsJsonAsync(client.BaseAddress, param);

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

        public async Task<Response> SOFT_DELETE(InboundHeader param)
        {
            Response resp = new Response();

            try
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(_domain + $"api/apiInboundHeader/soft_delete");

                HttpResponseMessage response = await client.PutAsJsonAsync(client.BaseAddress, param);

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

        public async Task<Response> GET_ITEM(int headerId)
        {
            Response resp = new Response();

            List<ViewInboundItemModel> listData = new List<ViewInboundItemModel>();

            try
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(_domain + $"api/apiInboundItem/GET_BY_HEADER?headerId={headerId}");

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
                            listData = JsonConvert.DeserializeObject<List<ViewInboundItemModel>>(apiResponse.output_data.ToString());

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

    }
}
