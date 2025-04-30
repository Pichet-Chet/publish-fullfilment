using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface IInboundHeaderRepository
    {
        Task<Response> GET_ALL(FilterModel param);

        Task<Response> GET_ALL_BY_VENDOR(int vendorId);

        Task<Response> GET_DETAIL(int id);

        Task<Response> SOFT_DELETE(InboundHeader id);

        Task<Response> UPDATE(InboundHeader param);

        Task<Response> ARRIVED_TIME(InboundHeader param);

        Task<Response> INSERT(transactionInbound param);

        Task<Response> GET_ITEM(int headerId);

    }
}
