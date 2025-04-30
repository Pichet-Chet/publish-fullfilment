using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;
using FULFILLMENT.H4U.API.Repository.Interface;
using FULFILLMENT.H4U.API.Service;
using FULFILLMENT.H4U.API.Service.transaction;
using System;

namespace FULFILLMENT.H4U.API.Repository
{
    public class GoodReceiveItemRepository : IGoodReceiveItemRepository
    {
        GoodReceiveItemService service = new GoodReceiveItemService();

        transactionGoodReceiveService transaction = new transactionGoodReceiveService();

        public async Task<Response> GET_ALL_BY_HEADER(int headerId)
        {
            Response resp = new Response();

            try
            {
                var result = await service.GET_ALL_BY_HEADER(headerId);

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

        public async Task<Response> UPDATE(transactionGoodReceive param)
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

        #endregion

    }
}

