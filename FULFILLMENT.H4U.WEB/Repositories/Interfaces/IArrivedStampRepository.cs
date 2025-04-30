using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface IArrivedStampRepository
    {
        Task<Response> GET_ALL(FilterModel param);
        Task<Response> GET_ALL_BY_VENDOR(int vendorId);
        Task<Response> UPDATE(InboundHeader param);

    }
}
