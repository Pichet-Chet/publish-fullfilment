using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
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
    public class WarehousePackingController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IWebHostEnvironment _hostingEnvironment;

        IPackingRepository _repoCollection;


        public WarehousePackingController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;

            _hostingEnvironment = hostingEnvironment;

            _repoCollection = new PackingRepository(configuration);

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

            try
            {
                ViewSysUserModel userInfo = SessionHelper.GetObjectFromJson<ViewSysUserModel>(this.HttpContext.Session, "UserInfo");

                param.vendorId = userInfo.VenderId;

                resp = await _repoCollection.GET_ALL(param);
                resp.output_data = resp.output_data == null ? new List<string>() : resp.output_data;

            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp);
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


        public async Task<JsonResult> INSERT([FromBody] Packing model)
        {
            Response resp = new Response();

            ViewSysUserModel userInfo = SessionHelper.GetObjectFromJson<ViewSysUserModel>(this.HttpContext.Session, "UserInfo");

            try
            {
                if (model != null)
                {
                    model.CreateBy = Convert.ToString(userInfo.UserId);
                    model.UpdateBy = Convert.ToString(userInfo.UserId);
                    //model.VendorId = userInfo.VenderId;

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


        public async Task<JsonResult> UPDATE([FromBody] Packing model)
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

    }
}
