using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;
using FULFILLMENT.H4U.API.Repository.Interface;
using FULFILLMENT.H4U.API.Service;
using FULFILLMENT.H4U.API.Service.transaction;

namespace FULFILLMENT.H4U.API.Repository
{
    public class PurchaseOrderHeaderRepository : IPurchaseOrderHeaderRepository
    {
        PurchaseOrderHeaderService service = new PurchaseOrderHeaderService();

        transactionPurchaseOrderService transaction = new transactionPurchaseOrderService();



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

        public async Task<Response> INSERT(transactionPurchaseOrder param)
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

        public async Task<Response> UPDATE(transactionPurchaseOrder param)
        {
            Response resp = new Response();

            try
            {
                var result = await transaction.UPDATE(param);

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

        public async Task<Response> CANCEL(transactionPurchaseOrder param)
        {
            Response resp = new Response();

            try
            {
                var result = await transaction.CANCEL(param);

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

        public async Task<Response> UPDATE_PICKLIST(transactionPurchaseOrder param)
        {
            Response resp = new Response();

            try
            {
                var result = await transaction.UPDATE_PICKLIST(param);

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

        public async Task<Response> UPDATE_PACKING(transactionPurchaseOrder param)
        {
            Response resp = new Response();

            try
            {
                var result = await transaction.UPDATE_PACKING(param);

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

        public async Task<Response> UPDATE_SHIPPED(transactionPurchaseOrder param)
        {
            Response resp = new Response();

            try
            {
                var result = await transaction.UPDATE_SHIPPED(param);

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
