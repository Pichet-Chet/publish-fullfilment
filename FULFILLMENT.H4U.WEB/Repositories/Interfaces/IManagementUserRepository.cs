using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface IManagementUserRepository
    {
        Task<Response> GET_ALL();
        Task<Response> GET_COUNT();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(SysUser param);
        Task<Response> UPDATE(SysUser param);
        Task<Response> CHANGE_PASSWORD(SysUser param);
        Task<Response> EDIT_PROFILE(SysUser param);

    }
}
