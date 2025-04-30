using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Repository;
using FULFILLMENT.H4U.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FULFILLMENT.H4U.API.Controllers
{
    [Route("api/[controller]")]
    public class apiManagementRoleController : ControllerBase
    {
        private readonly IManagementRolesRepository repoCollection;

        public apiManagementRoleController()
        {
            repoCollection = new ManagementRolesRepository();
        }


        #region Group


        [HttpGet]
        [Route("GET_ALL")]
        public async Task<IActionResult> GET_ALL([FromQuery] FilterModel param)
        {
            try
            {
                var result = await repoCollection.GET_ALL(param);

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
        public async Task<IActionResult> INSERT([FromBody] SysRoleGroup param)
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
        public async Task<IActionResult> UPDATE([FromBody] SysRoleGroup param)
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

        #endregion


        #region Item

        [HttpGet]
        [Route("LIST_GET_ALL")]
        public async Task<IActionResult> LIST_GET_ALL([FromQuery] FilterModel param)
        {
            try
            {
                var result = await repoCollection.LIST_GET_ALL(param);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("LIST_UPDATE")]
        public async Task<IActionResult> LIST_UPDATE([FromBody] SysRoleList param)
        {
            try
            {
                var result = await repoCollection.LIST_UPDATE(param);

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

