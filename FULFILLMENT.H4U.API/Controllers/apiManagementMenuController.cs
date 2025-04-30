using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Repository;
using FULFILLMENT.H4U.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FULFILLMENT.H4U.API.Controllers
{
    [Route("api/[controller]")]
    public class apiManagementMenuController : ControllerBase
    {
        private readonly IManagementMenuRepository repoCollection;

        public apiManagementMenuController()
        {
            repoCollection = new ManagementMenuRepository();
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
        public async Task<IActionResult> INSERT([FromBody] SysMenuGroup param)
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
        public async Task<IActionResult> UPDATE([FromBody] SysMenuGroup param)
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



        #region List


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


        [HttpGet]
        [Route("LIST_GET_DETAIL")]
        public async Task<IActionResult> LIST_GET_DETAIL(int id)
        {
            try
            {
                var result = await repoCollection.LIST_GET_DETAIL(id);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("LIST_INSERT")]
        public async Task<IActionResult> LIST_INSERT([FromBody] SysMenuList param)
        {
            try
            {
                var result = await repoCollection.LIST_INSERT(param);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }


        [HttpPut]
        [Route("LIST_UPDATE")]
        public async Task<IActionResult> LIST_UPDATE([FromBody] SysMenuList param)
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

