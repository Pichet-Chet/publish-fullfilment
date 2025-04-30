using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface IPurchaseOrderItemRepository
    {
        Task<Response> GET_ALL_BY_HEADER(int headerId);

        Task<Response> GET_DETAIL(int id);
    }
}
