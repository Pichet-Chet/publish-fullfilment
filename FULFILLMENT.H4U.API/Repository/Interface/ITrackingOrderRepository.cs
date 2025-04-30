using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface ITrackingOrderRepository
    {
        Task<Response> GET_ALL(FilterModel param);

    }
}
