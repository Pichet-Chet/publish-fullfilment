using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Repository.Interface;
using FULFILLMENT.H4U.API.Service;

namespace FULFILLMENT.H4U.API.Repository
{
    public class InboundItemRepository : IInboundItemRepository
    {
        InboundItemService service = new InboundItemService();

        public async Task<Response> GET_ALL_BY_HEADER(int headerId)
        {
            Response resp = new Response();

            try
            {
                var result = service.GET_ALL_BY_HEADER(headerId).Result;

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

        public async Task<Response> GET_DETAIL(int id)
        {
            Response resp = new Response();

            try
            {
                var result = service.GET_DETAIL(id).Result;

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



        #region Transaction
        public async Task<Response> UPDATE(InboundItem param)
        {
            Response resp = new Response();

            try
            {
                var result = service.UPDATE(param).Result;

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

        #endregion

    }
}
