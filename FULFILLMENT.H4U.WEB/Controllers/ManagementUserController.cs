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
    public class ManagementUserController : Controller
    {
        IManagementUserRepository _repoCollection;

        private readonly ILogger<HomeController> _logger;

        private IWebHostEnvironment _hostingEnvironment;
        public ManagementUserController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;

            _hostingEnvironment = hostingEnvironment;

            _repoCollection = new ManagementUserRepository(configuration);

        }
        public async Task<IActionResult> Index()
        {
            Response resp = new Response();

            UserCount userCount = new UserCount();

            ViewSysUserModel userInfo = SessionHelper.GetObjectFromJson<ViewSysUserModel>(this.HttpContext.Session, "UserInfo");

            resp  = await USER_COUNT();

            if (resp.status == true)
            {
                var jsonOutput = JsonConvert.SerializeObject(resp.output_data);

                userCount = JsonConvert.DeserializeObject<UserCount>(jsonOutput);
            }

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

            return View(userCount);
        }

        public async Task<JsonResult> GET_ALL()
        {
            Response resp = new Response();

            try
            {
                resp = _repoCollection.GET_ALL().Result;
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp);
        }

        public async Task<Response> USER_COUNT()
        {
            Response resp = new Response();

            try
            {
                resp = _repoCollection.GET_COUNT().Result;
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return resp;
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

        public JsonResult UPDATE([FromBody] SysUser model)
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

        public JsonResult INSERT([FromBody] SysUser model)
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

        public JsonResult CHANGE_PASSWORD([FromBody] SysUser model)
        {
            Response resp = new Response();

            SysUser userInfo = SessionHelper.GetObjectFromJson<SysUser>(this.HttpContext.Session, "UserInfo");

            try
            {
                if (model != null)
                {
                    model.CreateBy = Convert.ToString(userInfo.UserId);
                    model.UpdateBy = Convert.ToString(userInfo.UserId);

                    resp = _repoCollection.CHANGE_PASSWORD(model).Result;
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

        public JsonResult EDIT_PROFILE([FromBody] SysUser model)
        {
            Response resp = new Response();

            SysUser userInfo = SessionHelper.GetObjectFromJson<SysUser>(this.HttpContext.Session, "UserInfo");

            try
            {
                if (model != null)
                {
                    model.CreateBy = Convert.ToString(userInfo.UserId);
                    model.UpdateBy = Convert.ToString(userInfo.UserId);

                    resp = _repoCollection.EDIT_PROFILE(model).Result;
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
