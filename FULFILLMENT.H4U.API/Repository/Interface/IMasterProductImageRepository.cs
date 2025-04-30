using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Reponse;
namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface IMasterProductImageRepository
    {
        Task<Response> GET_ALL();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProductImage param);
        Task<Response> UPDATE(ProductImage param);
        Task<Response> DELETE(int id);
    }
}
