using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface IMasterProductRepository
    {
        Task<Response> GET_ALL();
        Task<Response> GET_ACTIVE();
        Task<Response> GET_ALL_BY_VENDOR(int vendorId);
        Task<Response> GET_FILTER(FilterModel param);
        Task<Response> GET_DETAIL(int id);
        Task<Response> SEARCH(string textSearch);
        Task<Response> INSERT(MasterProduct param);
        Task<Response> UPDATE(MasterProduct param);
        Task<Response> DELETE(int id);
    }
}
