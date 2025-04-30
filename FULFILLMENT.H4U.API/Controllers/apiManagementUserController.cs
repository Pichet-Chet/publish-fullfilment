using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Repository;
using FULFILLMENT.H4U.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FULFILLMENT.H4U.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class apiManagementUserController : ControllerBase
    {
        private readonly IManagementUserRepository repoCollection;

        public apiManagementUserController()
        {
            repoCollection = new ManagementUserRepository();
        }


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
        [Route("GET_COUNT")]
        public async Task<IActionResult> GET_COUNT()
        {
            try
            {
                var result = await repoCollection.GET_COUNT();

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


        [HttpPost]
        [Route("INSERT")]
        public async Task<IActionResult> INSERT(SysUser param)
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
        public async Task<IActionResult> UPDATE(SysUser param)
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

        [HttpPut]
        [Route("CHANGE_PASSWORD")]
        public async Task<IActionResult> CHANGE_PASSWORD(SysUser param)
        {
            try
            {
                var result = await repoCollection.CHANGE_PASSWORD(param);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("EDIT_PROFILE")]
        public async Task<IActionResult> EDIT_PROFILE(SysUser param)
        {
            try
            {
                var result = await repoCollection.EDIT_PROFILE(param);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



    }
}
