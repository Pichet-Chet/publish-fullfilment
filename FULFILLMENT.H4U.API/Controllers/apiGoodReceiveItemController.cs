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
    public class apiGoodReceiveItemController : ControllerBase
    {
        private readonly IGoodReceiveItemRepository repoCollection;

        public apiGoodReceiveItemController()
        {
            repoCollection = new GoodReceiveItemRepository();
        }


        [HttpGet]
        [Route("GET_ALL_BY_HEADER")]
        public async Task<IActionResult> GET_ALL_BY_HEADER(int headerId)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = await repoCollection.GET_ALL_BY_HEADER(headerId);

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

        [HttpPut]
        [Route("UPDATE")]
        public async Task<IActionResult> UPDATE(transactionGoodReceive param)
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

    }
}
