using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface IPurchaseOrderHeaderRepository
    {
        Task<Response> GET_ALL(FilterModel param);
        Task<Response> GET_DETAIL(int id);



        #region Transaction

        Task<Response> INSERT(transactionPurchaseOrder param);
        Task<Response> UPDATE(transactionPurchaseOrder param);
        Task<Response> CANCEL(transactionPurchaseOrder param);

        #endregion

    }
}
