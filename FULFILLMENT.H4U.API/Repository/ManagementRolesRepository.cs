using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Repository.Interface;
using FULFILLMENT.H4U.API.Service;

namespace FULFILLMENT.H4U.API.Repository
{
    public class ManagementRolesRepository : IManagementRolesRepository
    {
        ManagementRolesService service = new ManagementRolesService();



        #region Role Header

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

        public async Task<Response> GET_DETAIL(int id)
        {
            Response resp = new Response();

            try
            {
                var result = await service.GET_DETAIL(id);

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

        public async Task<Response> INSERT(SysRoleGroup param)
        {
            Response resp = new Response();

            try
            {
                var result = await service.INSERT(param);

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

        public async Task<Response> UPDATE(SysRoleGroup param)
        {
            Response resp = new Response();

            try
            {
                var result = await service.UPDATE(param);

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

        #region Role List

        public async Task<Response> LIST_GET_ALL(FilterModel param)
        {
            Response resp = new Response();

            try
            {
                var result = await service.LIST_GET_ALL(param);

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

        public async Task<Response> LIST_UPDATE(SysRoleList param)
        {
            Response resp = new Response();

            try
            {
                var result = await service.LIST_UPDATE(param);

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
