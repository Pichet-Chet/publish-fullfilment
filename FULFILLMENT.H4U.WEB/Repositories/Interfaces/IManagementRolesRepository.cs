using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface IManagementRolesRepository
    {
        #region Menu Group
        Task<Response> GET_ALL(FilterModel param);
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(SysRoleGroup param);
        Task<Response> UPDATE(SysRoleGroup param);

        #endregion


        #region MyRegion

        Task<Response> LIST_GET_ALL(FilterModel param);
        Task<Response> LIST_UPDATE(SysRoleList param);




        #endregion
    }
}
