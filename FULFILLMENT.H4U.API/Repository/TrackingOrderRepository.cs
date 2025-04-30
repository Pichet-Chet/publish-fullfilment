using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Repository.Interface;
using FULFILLMENT.H4U.API.Service;


namespace FULFILLMENT.H4U.API.Repository
{
    public class TrackingOrderRepository : ITrackingOrderRepository
    {
        TrackingOrderService service = new TrackingOrderService();


        public async Task<Response> GET_ALL(FilterModel param)
        {
            Response resp = new Response();

            try
            {
                var result = await service.GET_ALL(param);

                resp = result;
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
