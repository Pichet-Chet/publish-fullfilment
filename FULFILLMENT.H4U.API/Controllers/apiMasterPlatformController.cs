using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Repository;
using FULFILLMENT.H4U.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FULFILLMENT.H4U.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class apiMasterPlatformController : ControllerBase
    {
        private readonly IMasterPlatformRepository repoCollection;

        public apiMasterPlatformController()
        {
            repoCollection = new MasterPlatformRepository();
        }

        [HttpGet]
        [Route("GET_ALL")]
        public async Task<IActionResult> GET_ALL([FromQuery] FilterModel param)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = repoCollection.GET_ALL(param).Result;

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

                var result = repoCollection.GET_DETAIL(id).Result;

                watch.Stop();

                result.response_time = watch.Elapsed.Seconds + " " + Constants.RESPONSE_UNIT;

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("INSERT")]
        public async Task<IActionResult> INSERT(MasterPlatform param)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = repoCollection.INSERT(param).Result;

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
        public async Task<IActionResult> UPDATE(MasterPlatform param)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = repoCollection.UPDATE(param).Result;

                watch.Stop();

                result.response_time = watch.Elapsed.Seconds + " " + Constants.RESPONSE_UNIT;

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DELETE")]
        public async Task<IActionResult> DELETE(int param)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = repoCollection.DELETE(param).Result;

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
