using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;
using FULFILLMENT_H4U.Helper;
using FULFILLMENT_H4U.Models;
using FULFILLMENT_H4U.Repositories;
using FULFILLMENT_H4U.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;


namespace FULFILLMENT_H4U.Controllers
{

    public class AuthenticationController : Controller
    {
        HelperService helper = new HelperService();

        private readonly IConfiguration _configuration;
        private IAuthenticationRepository repo_authen;
        private ISysAccessLogRepository repo_access_log;


        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
            repo_authen = new AuthenticationRepository(configuration);
            repo_access_log = new SysAccessLogRepository(configuration);

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            ViewSysUserModel userInfo = SessionHelper.GetObjectFromJson<ViewSysUserModel>(this.HttpContext.Session, "UserInfo");

            if (userInfo != null)
            {
                return Redirect(Url.Action("Index", "Home"));
            }

            return View();
        }

        public IActionResult SignUp()
        {
            ViewSysUserModel userInfo = SessionHelper.GetObjectFromJson<ViewSysUserModel>(this.HttpContext.Session, "UserInfo");

            if (userInfo != null)
            {
                return Redirect(Url.Action("Index", "Home"));
            }

            return View();
        }

        public IActionResult ResetPassword()
        {
            SessionHelper.SetObjectAsJson(this.HttpContext.Session, "UserInfo", null);

            return View();
        }

        public IActionResult SignOut()
        {
            SessionHelper.SetObjectAsJson(this.HttpContext.Session, "UserInfo", null);

            return Redirect(Url.Action("SignIn", "Authentication"));
        }

        public IActionResult GetUserInfo()
        {
            ViewSysUserModel userInfo = SessionHelper.GetObjectFromJson<ViewSysUserModel>(this.HttpContext.Session, "UserInfo");

            return View(userInfo);
        }

        [HttpPost]
        public async Task<JsonResult> AuthorizeUser([FromBody] SysUser param)
        {
            Response resp = new Response();

            param.ApplicationName = _configuration["ApplicationName"];

            try
            {
                param.SecretKey = "";
                param.IsActive = true;

                var apiSignin = await Task.Run(() => repo_authen.SIGN_IN(param));

                if (apiSignin.status == true)
                {
                    var remoteIpAddress = this.HttpContext.Connection.RemoteIpAddress?.ToString();

                    var remoteIpAddressMac = Request.HttpContext.Connection.LocalIpAddress;

                    string MachineName1 = Environment.MachineName;

                    var userSignin = JsonConvert.DeserializeObject<ViewSysUserModel>(apiSignin.output_data.ToString());

                    SysAccess sysAccess = new SysAccess();

                    sysAccess.UserName = userSignin.UserName;
                    sysAccess.AccessFunction = Constants.FUNCTION_SIGN_IN;
                    sysAccess.TransactionDate = helper.GetDateTimeNow();
                    sysAccess.AccessDetail = $"Login>>{apiSignin.status}";
                    sysAccess.IpAddress = remoteIpAddress;
                    sysAccess.MacAddress = MachineName1;
                    sysAccess.VendorCode = userSignin.VenderId;
                    sysAccess.MenuCode = 0;

                    var respLog = await repo_access_log.INSERT(sysAccess);


                    if (apiSignin.status && userSignin != null)
                    {

                        //ViewSysUserModel UserInfo = new ViewSysUserModel();

                        //var apiMenuList = await Task.Run(() => repo_menu_list.GET_DETAIL(UserInfo.UserId));

                        //if (apiMenuList.status == true)
                        //{
                        //    List<SysMenu> listMenu = new List<SysMenu>();

                        //    listMenu = JsonConvert.DeserializeObject<List<SysMenu>>(apiMenuList.output_data.ToString());

                        //    if (listMenu != null && listMenu.Count > 0)
                        //    {
                        //        UserInfo.MenuList = new List<SysMenu>();

                        //        UserInfo.MenuList = listMenu;
                        //    }
                        //}


                        SessionHelper.SetObjectAsJson(this.HttpContext.Session, "UserInfo", userSignin);

                        resp.status = apiSignin.status;
                        resp.message = apiSignin.message;
                        //resp.output_data = UserInfo;
                    }
                }
                else
                {
                    resp.status = false;
                }
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp);
        }


        [HttpPost]
        public JsonResult Register([FromBody] SysUser model)
        {
            Response resp = new Response();

            try
            {
                if (model != null)
                {
                    model.UserId = 0;

                    resp = repo_authen.SIGN_UP(model).Result;
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

        [HttpPost]
        public JsonResult ResetPassword([FromBody] SysUser model)
        {
            Response resp = new Response();

            try
            {
                if (model != null)
                {
                    model.UserId = 0;

                    resp = repo_authen.RESET_PASSWORD(model).Result;
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
