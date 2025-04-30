using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface IMasterProductTypeRepository
    {
        Task<Response> GET_ALL(FilterModel param);
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(MasterProductsType param);
        Task<Response> UPDATE(MasterProductsType param);
        Task<Response> DELETE(int id);
    }
}
