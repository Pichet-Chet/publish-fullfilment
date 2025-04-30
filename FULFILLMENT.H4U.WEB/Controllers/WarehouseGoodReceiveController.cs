using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;
using FULFILLMENT_H4U.Helper;
using FULFILLMENT_H4U.Models;
using FULFILLMENT_H4U.Repositories;
using FULFILLMENT_H4U.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FULFILLMENT_H4U.Controllers
{
    public class WarehouseGoodReceiveController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IWebHostEnvironment _hostingEnvironment;

        IGoodReceiveHeaderRepository _repoHeader;

        IGoodReceiveItemRepository _repoItem;


        public WarehouseGoodReceiveController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;

            _hostingEnvironment = hostingEnvironment;

            _repoHeader = new GoodReceiveHeaderRepository(configuration);

            _repoItem = new GoodReceiveItemRepository(configuration);

        }

        public IActionResult Index()
        {

            ViewSysUserModel userInfo = SessionHelper.GetObjectFromJson<ViewSysUserModel>(this.HttpContext.Session, "UserInfo");

            if (userInfo == null)
            {
                return Redirect(Url.Action("SignIn", "Authentication"));
            }
            else
            {
                bool Access = false;

                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

                foreach (var item in userInfo.menuAccess)
                {
                    if (item.Controller.Contains(controllerName))
                    {
                        Access = true;
                        break;
                    }
                }

                if (Access == false)
                {
                    return RedirectToAction("AccessDenide", "Home");
                }
            }

            return View();
        }



        #region Goods Receive Header

        public async Task<JsonResult> GET_ALL([FromQuery] FilterModel param)
        {
            Response resp = new Response();

            try
            {
                resp = await _repoHeader.GET_ALL(param);
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp);
        }

        public async Task<JsonResult> GET_DETAIL(int id)
        {
            Response resp = new Response();

            try
            {
                resp = await _repoHeader.GET_DETAIL(id);
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp);
        }

        public async Task<JsonResult> INSERT([FromBody] transactionGoodReceive param)
        {
            Response resp = new Response();

            ViewSysUserModel userInfo = SessionHelper.GetObjectFromJson<ViewSysUserModel>(this.HttpContext.Session, "UserInfo");

            try
            {
                if (param != null)
                {
                    //param.header.VendorId = userInfo.VenderId;
                    param.header.CreateBy = Convert.ToString(userInfo.UserId);
                    param.header.UpdateBy = Convert.ToString(userInfo.UserId);


                    foreach (var item in param.item)
                    {
                        item.CreateBy = Convert.ToString(userInfo.UserId);
                    }

                    resp = await _repoHeader.INSERT(param);
                }
                else
                {
                    resp.status = false;
                    resp.message = Constants.DATA_TYPE_ERROR;
                }

            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp);
        }

        #endregion




        #region Goods Receive Item

        public async Task<JsonResult> GET_ALL_BY_HEADER(int headerId)
        {
            Response resp = new Response();

            try
            {
                resp = await _repoItem.GET_ALL_BY_HEADER(headerId);
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp);
        }

        public async Task<JsonResult> GET_DETAIL_BY_ITEM(int id)
        {
            Response resp = new Response();

            try
            {
                resp = await _repoItem.GET_DETAIL(id);
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp);
        }

        public async Task<JsonResult> UPDATE([FromBody] transactionGoodReceive param)
        {
            Response resp = new Response();

            ViewSysUserModel userInfo = SessionHelper.GetObjectFromJson<ViewSysUserModel>(this.HttpContext.Session, "UserInfo");

            try
            {
                if (param != null)
                {
                    //param.header.VendorId = userInfo.VenderId;
                    param.header.CreateBy = Convert.ToString(userInfo.UserId);
                    param.header.UpdateBy = Convert.ToString(userInfo.UserId);


                    foreach (var item in param.item)
                    {
                        item.CreateBy = Convert.ToString(userInfo.UserId);
                        item.UpdateBy = Convert.ToString(userInfo.UserId);
                    }

                    resp = await _repoHeader.UPDATE(param);
                }
                else
                {
                    resp.status = false;
                    resp.message = Constants.DATA_TYPE_ERROR;
                }

            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp);
        }


        #endregion




        #region Good Receive Status

        public async Task<JsonResult> GET_STATUS([FromQuery] FilterModel param)
        {
            Response resp = new Response();

            try
            {
                resp = await _repoHeader.GET_STATUS(param);
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp);
        }


        #endregion



    }
}

