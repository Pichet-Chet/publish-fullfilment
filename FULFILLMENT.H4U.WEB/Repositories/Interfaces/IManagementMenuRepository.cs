using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface IManagementMenuRepositorys
    {

        #region Menu Group
        Task<Response> GET_ALL(FilterModel param);
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(SysMenuGroup param);
        Task<Response> UPDATE(SysMenuGroup param);

        #endregion



        #region Menu List

        Task<Response> LIST_GET_ALL(FilterModel param);
        Task<Response> LIST_GET_DETAIL(int id);
        Task<Response> LIST_INSERT(SysMenuList param);
        Task<Response> LIST_UPDATE(SysMenuList param);

        #endregion
    }
}
