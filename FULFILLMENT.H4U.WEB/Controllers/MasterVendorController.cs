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

namespace FULFILLMENT_H4U.Controllers
{
    public class MasterVendorController : Controller
    {
        IMasterVendorRepository _repoCollection;

        public MasterVendorController(IConfiguration configuration)
        {
            _repoCollection = new MasterVendorRepository(configuration);
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

        public async Task<JsonResult> GET_ACTIVE()
        {
            Response resp = new Response();

            try
            {
                resp = _repoCollection.GET_ACTIVE().Result;
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp);
        }

        public JsonResult DETAIL(int id)
        {
            Response resp = new Response();

            try
            {
                resp = _repoCollection.GET_DETAIL(id).Result;

            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp.output_data);

        }

        [HttpPost]
        public JsonResult INSERT([FromBody] MasterVendor model)
        {
            Response resp = new Response();

            SysUser userInfo = SessionHelper.GetObjectFromJson<SysUser>(this.HttpContext.Session, "UserInfo");

            try
            {
                if (model != null)
                {
                    model.CreateBy = Convert.ToString(userInfo.UserId);
                    model.UpdateBy = Convert.ToString(userInfo.UserId);

                    resp = _repoCollection.INSERT(model).Result;
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

        public JsonResult UPDATE([FromBody] MasterVendor model)
        {
            Response resp = new Response();

            SysUser userInfo = SessionHelper.GetObjectFromJson<SysUser>(this.HttpContext.Session, "UserInfo");

            try
            {
                if (model != null)
                {
                    model.CreateBy = Convert.ToString(userInfo.UserId);
                    model.UpdateBy = Convert.ToString(userInfo.UserId);

                    resp = _repoCollection.UPDATE(model).Result;
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

        public JsonResult DELETE(int id)
        {
            Response resp = new Response();

            try
            {
                resp = _repoCollection.DELETE(id).Result;
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
