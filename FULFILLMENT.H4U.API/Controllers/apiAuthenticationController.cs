using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Repository;
using FULFILLMENT.H4U.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FULFILLMENT.H4U.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class apiAuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository repoCollection;

        public apiAuthenticationController()
        {
            repoCollection = new AuthenticationRepository();
        }

        [HttpPost]
        [Route("SIGNIN")]
        public async Task<IActionResult> SIGNIN(SysUser param)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = await repoCollection.SIGN_IN(param);

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
        [Route("SIGNUP")]
        public async Task<IActionResult> SIGNUP(SysUser param)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = await repoCollection.SIGN_UP(param);

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
        [Route("RESET_PASSWORD")]
        public async Task<IActionResult> RESET_PASSWORD(SysUser param)
        {
            try
            {
                var watch = new Stopwatch();

                watch.Start();

                var result = await repoCollection.RESET_PASSWORD(param);

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
