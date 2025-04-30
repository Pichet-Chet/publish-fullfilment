using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<Response> SIGN_IN(SysUser param);
        Task<Response> SIGN_UP(SysUser param);
        Task<Response> RESET_PASSWORD(SysUser param);
    }
}
