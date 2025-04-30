using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Transaction;
using FULFILLMENT.H4U.API.Repository;
using FULFILLMENT.H4U.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FULFILLMENT.H4U.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class apiPurchaseOrderHeaderController : ControllerBase
    {
        private readonly IPurchaseOrderHeaderRepository repoCollection;

        public apiPurchaseOrderHeaderController()
        {
            repoCollection = new PurchaseOrderHeaderRepository();
        }

        [HttpGet]
        [Route("GET_ALL")]
        public async Task<IActionResult> GET_ALL([FromQuery] FilterModel param)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = await repoCollection.GET_ALL(param);

                watch.Stop();

                result.response_time = watch.Elapsed.Seconds + " " + Constants.RESPONSE_UNIT;

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("DETAIL")]
        public async Task<IActionResult> DETAIL(int id)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = await repoCollection.GET_DETAIL(id);

                watch.Stop();

                result.response_time = watch.Elapsed.Seconds + " " + Constants.RESPONSE_UNIT;

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        #region Transaction


        [HttpPost]
        [Route("INSERT")]
        public async Task<IActionResult> INSERT(transactionPurchaseOrder param)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = await repoCollection.INSERT(param);

                watch.Stop();

                result.response_time = watch.Elapsed.Seconds + " " + Constants.RESPONSE_UNIT;

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("UPDATE")]
        public async Task<IActionResult> UPDATE(transactionPurchaseOrder param)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = await repoCollection.UPDATE(param);

                watch.Stop();

                result.response_time = watch.Elapsed.Seconds + " " + Constants.RESPONSE_UNIT;

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("CANCEL")]
        public async Task<IActionResult> CANCEL(transactionPurchaseOrder param)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = await repoCollection.CANCEL(param);

                watch.Stop();

                result.response_time = watch.Elapsed.Seconds + " " + Constants.RESPONSE_UNIT;

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPut]
        [Route("UPDATE_PICKLIST")]
        public async Task<IActionResult> UPDATE_PICKLIST(transactionPurchaseOrder param)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = await repoCollection.UPDATE_PICKLIST(param);

                watch.Stop();

                result.response_time = watch.Elapsed.Seconds + " " + Constants.RESPONSE_UNIT;

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UPDATE_PACKING")]
        public async Task<IActionResult> UPDATE_PACKING(transactionPurchaseOrder param)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = await repoCollection.UPDATE_PACKING(param);

                watch.Stop();

                result.response_time = watch.Elapsed.Seconds + " " + Constants.RESPONSE_UNIT;

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UPDATE_SHIPPED")]
        public async Task<IActionResult> UPDATE_SHIPPED(transactionPurchaseOrder param)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = await repoCollection.UPDATE_SHIPPED(param);

                watch.Stop();

                result.response_time = watch.Elapsed.Seconds + " " + Constants.RESPONSE_UNIT;

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        #endregion
    }
}
