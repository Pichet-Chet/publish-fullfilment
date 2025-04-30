using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;

namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface IInboundHeaderRepository
    {
        Task<Response> GET_ALL();

        Task<Response> GET_ALL_BY_VENDOR(int vendorId);

        Task<Response> GET_DETAIL(int id);

        Task<Response> SOFT_DELETE(InboundHeader id);

        Task<Response> GET_FILTER(FilterModel param);



        #region Transaction

        Task<Response> INSERT(transactionInbound param);

        Task<Response> UPDATE(InboundHeader param);

        Task<Response> ARRIVED_TIME(InboundHeader param);

        #endregion

    }
}
