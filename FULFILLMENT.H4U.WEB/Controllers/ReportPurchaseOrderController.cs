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
    public class ReportPurchaseOrderController : Controller
    {
        IPurchaseOrderHeaderRepository _repoHeader;

        IPurchaseOrderItemRepository _repoItem;

        IReportStockRepository _repoStock;
        public ReportPurchaseOrderController(IConfiguration configuration)
        {
            _repoHeader = new PurchaseOrderHeaderRepository(configuration);

            _repoItem = new PurchaseOrderItemRepository(configuration);

            _repoStock = new ReportStockRepository(configuration);
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

        #region Purchase Order Header

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

        public async Task<JsonResult> DETAIL(int id)
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

            return new JsonResult(resp.output_data);

        }


        #endregion




        #region Purchase Order Item

        public async Task<JsonResult> ITEM_GET_ALL_BY_HEADER(int headerId)
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

            return new JsonResult(resp.output_data);

        }

        public async Task<JsonResult> ITEM_DETAIL(int id)
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

            return new JsonResult(resp.output_data);

        }

        public async Task<JsonResult> GET_STOCK_BALANCE_ITEM(int productId)
        {
            Response resp = new Response();

            try
            {
                resp = await _repoStock.GET_STOCK_BALANCE_ITEM(productId);
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
