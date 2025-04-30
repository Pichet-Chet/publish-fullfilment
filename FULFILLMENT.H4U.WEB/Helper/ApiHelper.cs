using FULFILLMENT.H4U.API.Model.Reponse;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace FULFILLMENT_H4U.Helper
{
    public class ApiHelper
    {
        public static async Task<Response> PostURI(string url, HttpContent c)
        {
            Response resp = new Response();
            try
            {
                using (var client = new HttpClient())
                {
                    var baseAdress = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build().GetSection("UrlConnect")["ApiBaseAddress"];


                    string apiUrl = baseAdress + url;

                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage result = await Task.Run(() => client.PostAsync(new Uri(apiUrl), c));

                    if (result.IsSuccessStatusCode)
                    {
                        var x = result.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<Response>(x.ToString());
                    }
                    else
                    {
                        var getException = result.Content.ReadAsStringAsync();
                        resp.message = getException.ToString();
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
