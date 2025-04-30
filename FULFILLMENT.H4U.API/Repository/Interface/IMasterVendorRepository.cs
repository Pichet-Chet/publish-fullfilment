using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface IMasterVendorRepository
    {
        Task<Response> GET_ALL();
        Task<Response> GET_ACTIVE();
        Task<Response> GET_DETAIL(int id);
        Task<Response> GET_FILTER(FilterModel param);
        Task<Response> GET_BALANCE_MOVEMENT(FilterModel param);
        Task<Response> SEARCH(string textSearch);
        Task<Response> INSERT(MasterVendor param);
        Task<Response> UPDATE(MasterVendor param);
        Task<Response> DELETE(int id);
    }
}
