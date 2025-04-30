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
    public class apiPicklistHeaderController : ControllerBase
    {

        private readonly IPicklistHeaderRepository repoCollection;

        public apiPicklistHeaderController()
        {
            repoCollection = new PicklistHeaderRepository();
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
        public async Task<IActionResult> INSERT(TransactionPicklist param)
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

        #endregion


    }
}
