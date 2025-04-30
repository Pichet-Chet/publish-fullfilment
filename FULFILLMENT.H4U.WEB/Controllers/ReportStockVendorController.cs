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
    public class ReportStockVendorController : Controller
    {
        IReportStockRepository _repoCollection;

        public ReportStockVendorController(IConfiguration configuration)
        {
            _repoCollection = new ReportStockRepository(configuration);
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
