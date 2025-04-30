using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Repository;
using FULFILLMENT.H4U.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FULFILLMENT.H4U.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class apiSysAccessLogController : ControllerBase
    {
        private readonly ISysAccessLogRepository repoCollection;

        public apiSysAccessLogController()
        {
            repoCollection = new SysAccessLogRepository();
        }

        [HttpPut]
        [Route("INSERT")]
        public IActionResult INSERT(SysAccess param)
        {
            var result = repoCollection.INSERT(param).Result;

            return Ok(result);
        }

    }
}
