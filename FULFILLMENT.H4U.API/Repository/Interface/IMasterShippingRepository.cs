using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface IMasterShippingRepository
    {
        Task<Response> GET_ALL(FilterModel param);
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(MasterShipping param);
        Task<Response> UPDATE(MasterShipping param);
        Task<Response> DELETE(int id);
    }
}
