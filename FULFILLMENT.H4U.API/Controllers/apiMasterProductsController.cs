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
    public class apiMasterProductsController : ControllerBase
    {
        private readonly IMasterProductRepository repoCollection;

        public apiMasterProductsController()
        {
            repoCollection = new MasterProductRepository();
        }

        [HttpGet]
        [Route("GET")]
        public async Task<IActionResult> GET()
        {
            var result = repoCollection.GET_ALL().Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("GET_ACTIVE")]
        public async Task<IActionResult> GET_ACTIVE()
        {
            try
            {
                var result = repoCollection.GET_ACTIVE().Result;

                return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GET_BY_VENDOR")]
        public async Task<IActionResult> GET_BY_VENDOR(int vendorId)
        {
            var result = repoCollection.GET_ALL_BY_VENDOR(vendorId).Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("GET_FILTER")]
        public async Task<IActionResult> GET_FILTER([FromQuery]FilterModel param)
        {
            var result = repoCollection.GET_FILTER(param).Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("DETAIL")]
        public async Task<IActionResult> DETAIL(int id)
        {
            var result = repoCollection.GET_DETAIL(id).Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("SEARCH")]
        public async Task<IActionResult> SEARCH(string textSearch)
        {
            var result = repoCollection.SEARCH(textSearch).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("INSERT")]
        public async Task<IActionResult> INSERT(MasterProduct param)
        {
            var result = repoCollection.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPut]
        [Route("UPDATE")]
        public async Task<IActionResult> UPDATE(MasterProduct param)
        {
            var result = repoCollection.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpDelete]
        [Route("DELETE")]
        public async Task<IActionResult> DELETE(int param)
        {
            var result = repoCollection.DELETE(param).Result;

            return Ok(result);
        }
    }
}
