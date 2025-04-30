using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Repository;
using FULFILLMENT.H4U.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FULFILLMENT.H4U.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class apiSysUserController : ControllerBase
    {
        private readonly ISysUserRepository repoCollection;

        public apiSysUserController()
        {
            repoCollection = new SysUserRepository();
        }

        [HttpGet]
        [Route("GET")]
        public async Task<IActionResult> GET()
        {
            var result = repoCollection.GET_ALL().Result;

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
        public async Task<IActionResult> INSERT(MasterBin param)
        {
            var result = repoCollection.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPut]
        [Route("UPDATE")]
        public async Task<IActionResult> UPDATE(MasterBin param)
        {
            var result = repoCollection.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpDelete]
        [Route("DELETE")]
        public async Task<IActionResult> DELETE(int id)
        {
            var result = repoCollection.DELETE(id).Result;

            return Ok(result);
        }
    }
}
