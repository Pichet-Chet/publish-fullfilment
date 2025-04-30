using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface ITrackingOrderRepository
    {
        Task<Response> GET_ALL(FilterModel param);

    }
}
