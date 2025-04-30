using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface IPicklistItemRepository
    {
        Task<Response> GET_ALL_BY_HEADER(int headerId);
        Task<Response> GET_DETAIL(int id);
    }
}
