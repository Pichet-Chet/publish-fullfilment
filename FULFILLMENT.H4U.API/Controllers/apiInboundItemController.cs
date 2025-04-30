using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Repository;
using FULFILLMENT.H4U.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FULFILLMENT.H4U.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class apiInboundItemController : ControllerBase
    {
        private readonly IInboundItemRepository repoCollection;

        public apiInboundItemController()
        {
            repoCollection = new InboundItemRepository();
        }


        [HttpGet]
        [Route("GET_BY_HEADER")]
        public async Task<IActionResult> GET_BY_HEADER(int headerId)
        {
            try
            {
                var result = await repoCollection.GET_ALL_BY_HEADER(headerId);

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


        [HttpPut]
        [Route("UPDATE")]
        public async Task<IActionResult> UPDATE(InboundItem param)
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

    }
}
