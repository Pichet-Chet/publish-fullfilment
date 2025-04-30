using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface IMasterPlatformRepository
    {
        Task<Response> GET_ALL(FilterModel param);
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(MasterPlatform param);
        Task<Response> UPDATE(MasterPlatform param);
        Task<Response> DELETE(int id);
    }
}
