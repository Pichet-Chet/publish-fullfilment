using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Repository;
using FULFILLMENT.H4U.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FULFILLMENT.H4U.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class apiMasterLocationController : ControllerBase
    {
        private readonly IMasterLocationRepository repoCollection;

        public apiMasterLocationController()
        {
            repoCollection = new MasterLocationRepository();
        }


        #region master_bin

        [HttpGet]
        [Route("GET")]
        public async Task<IActionResult> GET()
        {
            try
            {
                var result = await repoCollection.GET_ALL();

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GET_FILTER")]
        public async Task<IActionResult> GET_FILTER([FromQuery] FilterModel param)
        {
            try
            {
                var result = await repoCollection.GET_FILTER(param);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GET_ACTIVE")]
        public async Task<IActionResult> GET_ACTIVE()
        {
            try
            {
                var result = await repoCollection.GET_ACTIVE();

                return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("DETAIL")]
        public async Task<IActionResult> DETAIL(int id)
        {
            try
            {
                var result = await repoCollection.GET_DETAIL(id);

                return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }


        }

        [HttpGet]
        [Route("SEARCH")]
        public async Task<IActionResult> SEARCH(string textSearch)
        {
            try
            {
                var result = await repoCollection.SEARCH(textSearch);

                return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("INSERT")]
        public async Task<IActionResult> INSERT(MasterLocation param)
        {
            try
            {
                var result = await repoCollection.INSERT(param);

                return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("UPDATE")]
        public async Task<IActionResult> UPDATE(MasterLocation param)
        {
            try
            {
                var result = await repoCollection.UPDATE(param);

                return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("DELETE")]
        public async Task<IActionResult> DELETE(int id)
        {
            try
            {
                var result = await repoCollection.DELETE(id);

                return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GET_TYPE")]
        public async Task<IActionResult> GET_TYPE()
        {
            try
            {
                var result = repoCollection.GET_TYPE().Result;

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        #endregion
    }
}
