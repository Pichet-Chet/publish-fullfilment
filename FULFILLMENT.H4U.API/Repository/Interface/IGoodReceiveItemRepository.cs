using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;


namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface IGoodReceiveItemRepository
    {
        Task<Response> GET_ALL_BY_HEADER(int headerId);

        Task<Response> GET_DETAIL(int id);



        #region transaction
        Task<Response> UPDATE(transactionGoodReceive param);

        #endregion

    }
}

