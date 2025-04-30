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
    public class apiMasterBoxController : ControllerBase
    {
        private readonly IMasterBoxRepository repoCollection;

        public apiMasterBoxController()
        {
            repoCollection = new MasterBoxRepository();
        }

        [HttpGet]
        [Route("GET_ALL")]
        public async Task<IActionResult> GET_ALL([FromQuery] FilterModel param)
        {
            var result = repoCollection.GET_ALL(param).Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("DETAIL")]
        public async Task<IActionResult> DETAIL(int id)
        {
            var result = repoCollection.GET_DETAIL(id).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("INSERT")]
        public async Task<IActionResult> INSERT(MasterBox param)
        {
            var result = repoCollection.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPut]
        [Route("UPDATE")]
        public async Task<IActionResult> UPDATE(MasterBox param)
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
