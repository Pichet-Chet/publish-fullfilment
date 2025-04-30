using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface ISysUserRepository
    {
        Task<Response> GET_ALL();
        Task<Response> GET_DETAIL(int id);
        Task<Response> SEARCH(string textSearch);
        Task<Response> INSERT(MasterBin param);
        Task<Response> UPDATE(MasterBin param);
        Task<Response> DELETE(int id);
    }
}
