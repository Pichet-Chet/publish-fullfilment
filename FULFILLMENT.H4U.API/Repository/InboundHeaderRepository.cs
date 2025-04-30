using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;
using FULFILLMENT.H4U.API.Repository.Interface;
using FULFILLMENT.H4U.API.Service;
using FULFILLMENT.H4U.API.Service.transaction;

namespace FULFILLMENT.H4U.API.Repository
{
    public class InboundHeaderRepository : IInboundHeaderRepository
    {
        InboundHeaderService service = new InboundHeaderService();
        transactionInboundService transaction = new transactionInboundService();

        public async Task<Response> GET_ALL()
        {
            Response resp = new Response();

            try
            {
                var result = await service.GET_ALL();

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

        public async Task<Response> GET_ALL_BY_VENDOR(int vendorId)
        {
            Response resp = new Response();

            try
            {
                var result = await service.GET_ALL_BY_VENDOR(vendorId);

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

        public async Task<Response> GET_FILTER(FilterModel param)
        {
            Response resp = new Response();

            try
            {
                var result = await service.GET_FILTER(param);

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

        public async Task<Response> SOFT_DELETE(InboundHeader param)
        {
            Response resp = new Response();

            try
            {
                var result = await service.SOFT_DELETE(param);

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

        public async Task<Response> INSERT(transactionInbound param)
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

        public async Task<Response> UPDATE(InboundHeader param)
        {
            Response resp = new Response();

            try
            {
                var result = await service.UPDATE(param);

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

        public async Task<Response> ARRIVED_TIME(InboundHeader param)
        {
            Response resp = new Response();

            try
            {
                var result = await service.ARRIVED_TIME(param);

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
