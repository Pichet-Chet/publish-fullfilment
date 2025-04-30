using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Repository.Interface;
using FULFILLMENT.H4U.API.Service;

namespace FULFILLMENT.H4U.API.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        AuthenticationService service = new AuthenticationService();

        public async Task<Response> SIGN_IN(SysUser param)
        {
            Response resp = new Response();

            try
            {
                var result = await Task.Run(() => service.SIGN_IN(param));

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

        public async Task<Response> SIGN_UP(SysUser param)
        {
            Response resp = new Response();

            try
            {
                var result = await Task.Run(() => service.SIGN_UP(param));

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

        public async Task<Response> RESET_PASSWORD(SysUser param)
        {
            Response resp = new Response();

            try
            {
                var result = await Task.Run(() => service.RESET_PASSWORD(param));

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
