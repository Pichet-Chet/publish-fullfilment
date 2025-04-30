using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;

namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface IPackingRepository
    {
        Task<Response> GET_ALL(FilterModel param);
        Task<Response> GET_DETAIL(int id);

        Task<Response> INSERT(Packing param);
        Task<Response> UPDATE(Packing param);

    }
}
