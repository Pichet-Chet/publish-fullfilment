using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface IManagementRolesRepository
    {
        #region Role Header

        Task<Response> GET_ALL(FilterModel param);
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(SysRoleGroup param);
        Task<Response> UPDATE(SysRoleGroup param);

        #endregion

        #region Role List

        Task<Response> LIST_GET_ALL(FilterModel param);

        Task<Response> LIST_UPDATE(SysRoleList param);



        #endregion
    }
}
