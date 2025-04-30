using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Repository.Interface;
using FULFILLMENT.H4U.API.Service;

namespace FULFILLMENT.H4U.API.Repository
{
    public class ManagementUserRepository : IManagementUserRepository
    {
        ManagementUserService service = new ManagementUserService();

        public async Task<Response> CHANGE_PASSWORD(SysUser param)
        {
            Response resp = new Response();

            try
            {
                var result = service.CHANGE_PASSWORD(param).Result;

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

        public async Task<Response> EDIT_PROFILE(SysUser param)
        {
            Response resp = new Response();

            try
            {
                var result = service.EDIT_PROFILE(param).Result;

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

        public async Task<Response> GET_ALL()
        {
            Response resp = new Response();

            try
            {
                var result = service.GET_ALL().Result;

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

        public async Task<Response> GET_COUNT()
        {
            Response resp = new Response();

            try
            {
                var result = service.GET_COUNT().Result;

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

        public async Task<Response> UPDATE(SysUser param)
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

        public async Task<Response> INSERT(SysUser param)
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
