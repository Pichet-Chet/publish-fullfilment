using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Constants;
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


namespace FULFILLMENT_H4U.Controllers
{
    public class RequestInboundController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IWebHostEnvironment _hostingEnvironment;

        IInboundHeaderRepository _repoCollection;

        public RequestInboundController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;

            _hostingEnvironment = hostingEnvironment;

            _repoCollection = new InboundHeaderRepository(configuration);
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


        public async Task<JsonResult> GET_ALL([FromQuery] FilterModel param)
        {
            Response resp = new Response();

            ViewSysUserModel userInfo = SessionHelper.GetObjectFromJson<ViewSysUserModel>(this.HttpContext.Session, "UserInfo");


            if (Constants.ROLE_FOR_COMPANY_ONLY.Contains(userInfo.Role))
            {
                param.vendorId = userInfo.VenderId;
            }

            try
            {
                resp = _repoCollection.GET_ALL(param).Result;

                resp.output_data = resp.output_data == null ? new List<string>() : resp.output_data;
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp);
        }

        public async Task<JsonResult> GET_ALL_BY_VENDOR(int vendorId)
        {
            Response resp = new Response();

            try
            {
                resp = await _repoCollection.GET_ALL_BY_VENDOR(vendorId);

            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp.output_data);

        }

        public async Task<JsonResult> DETAIL(int id)
        {
            Response resp = new Response();

            try
            {
                resp = await _repoCollection.GET_DETAIL(id);

            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp.output_data);

        }

        public async Task<JsonResult> INSERT([FromBody] transactionInbound model)
        {
            Response resp = new Response();

            ViewSysUserModel userInfo = SessionHelper.GetObjectFromJson<ViewSysUserModel>(this.HttpContext.Session, "UserInfo");

            try
            {
                if (model != null)
                {
                    model.header.VendorId = userInfo.VenderId;
                    model.header.CreateBy = Convert.ToString(userInfo.UserId);
                    model.header.UpdateBy = Convert.ToString(userInfo.UserId);


                    foreach (var item in model.item)
                    {
                        item.CreateBy = Convert.ToString(userInfo.UserId);
                    }

                    resp = await _repoCollection.INSERT(model);
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

        public async Task<JsonResult> UPDATE([FromBody] InboundHeader model)
        {
            Response resp = new Response();

            ViewSysUserModel userInfo = SessionHelper.GetObjectFromJson<ViewSysUserModel>(this.HttpContext.Session, "UserInfo");

            try
            {
                if (model != null)
                {
                    model.CreateBy = Convert.ToString(userInfo.UserId);

                    model.UpdateBy = Convert.ToString(userInfo.UserId);

                    resp = await _repoCollection.UPDATE(model);
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

        public async Task<JsonResult> SOFT_DELETE([FromBody] InboundHeader model)
        {
            Response resp = new Response();

            SysUser userInfo = SessionHelper.GetObjectFromJson<SysUser>(this.HttpContext.Session, "UserInfo");

            try
            {
                if (model != null)
                {
                    model.CreateBy = Convert.ToString(userInfo.UserId);

                    model.UpdateBy = Convert.ToString(userInfo.UserId);

                    resp = await _repoCollection.SOFT_DELETE(model);
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

        public async Task<JsonResult> ARRIVED_TIME([FromBody] InboundHeader model)
        {
            Response resp = new Response();

            SysUser userInfo = SessionHelper.GetObjectFromJson<SysUser>(this.HttpContext.Session, "UserInfo");

            try
            {
                if (model != null)
                {
                    model.CreateBy = Convert.ToString(userInfo.UserId);

                    model.UpdateBy = Convert.ToString(userInfo.UserId);

                    resp = await _repoCollection.UPDATE(model);
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

        public async Task<JsonResult> GET_ITEM(int headerId)
        {
            Response resp = new Response();

            try
            {
                resp = await _repoCollection.GET_ITEM(headerId);

            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp.output_data);

        }


    }
}
