using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT.H4U.API.Service
{
    public class SysAccessLogService
    {
        readonly DataContext _dbContext = new();


        public async Task<Response> INSERT(SysAccess param)
        {
            Response resp = new Response();

            try
            {
                await Task.Run(() => _dbContext.SysAccesses.Add(param));

                _dbContext.SaveChanges();

                resp.status = true;

                resp.message = Constants.SIGN_UP_SUCCESS;
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
