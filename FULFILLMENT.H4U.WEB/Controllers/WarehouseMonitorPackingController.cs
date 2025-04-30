using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT_H4U.Helper;
using FULFILLMENT_H4U.Repositories;
using FULFILLMENT_H4U.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FULFILLMENT_H4U.Controllers
{
    public class WarehouseMonitorPackingController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IWebHostEnvironment _hostingEnvironment;

        IPackingRepository _repoCollection;


        public WarehouseMonitorPackingController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
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
                resp = await _repoCollection.GET_ALL(param);
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
