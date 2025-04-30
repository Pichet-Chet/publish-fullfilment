using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;
using FULFILLMENT.H4U.API.Repository.Interface;
using FULFILLMENT.H4U.API.Service;
using FULFILLMENT.H4U.API.Service.transaction;

namespace FULFILLMENT.H4U.API.Repository
{
    public class PicklistHeaderRepository : IPicklistHeaderRepository
    {
        PicklistHeaderService service = new PicklistHeaderService();

        transactionPicklistService transaction = new transactionPicklistService();

        public async Task<Response> GET_ALL(FilterModel param)
        {
            Response resp = new Response();

            try
            {
                var result = await service.GET_ALL(param);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
                resp.inner_exception = ex.InnerException == null ? "" : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> GET_DETAIL(int id)
        {
            Response resp = new Response();

            try
            {
                var result = await service.GET_DETAIL(id);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
                resp.inner_exception = ex.InnerException == null ? "" : ex.InnerException.ToString();
            }

            return resp;
        }


        #region Transaction

        public async Task<Response> INSERT(TransactionPicklist param)
        {
            Response resp = new Response();

            try
            {
                var result = await transaction.INSERT(param);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
                resp.inner_exception = ex.InnerException == null ? "" : ex.InnerException.ToString();
            }

            return resp;
        }

        #endregion

    }
}
