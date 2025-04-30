using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Repository.Interface;
using FULFILLMENT.H4U.API.Service;

namespace FULFILLMENT.H4U.API.Repository
{
    public class SysAccessLogRepository : ISysAccessLogRepository
    {        
        SysAccessLogService service = new SysAccessLogService();

        public async Task<Response> INSERT(SysAccess param)
        {
            Response resp = new Response();

            try
            {
                var result = service.INSERT(param).Result;

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
