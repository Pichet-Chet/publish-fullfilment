using FULFILLMENT.H4U.API.Model;
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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IWebHostEnvironment _hostingEnvironment;

        IMasterBinRepository _repoCollection;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;

            _repoCollection = new MasterBinRepository(configuration);

            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            ViewSysUserModel userInfo = SessionHelper.GetObjectFromJson<ViewSysUserModel>(this.HttpContext.Session, "UserInfo");

            if (userInfo == null)
            {
                return Redirect(Url.Action("SignIn", "Authentication"));
            }

            return View(userInfo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccessDenide()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}