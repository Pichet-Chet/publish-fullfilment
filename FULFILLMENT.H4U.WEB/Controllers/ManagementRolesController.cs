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
    public class ManagementRolesController : Controller
    {
        IManagementRolesRepository _repoCollection;

        IManagementUserRepository _repoUser;


        private readonly ILogger<HomeController> _logger;

        private IWebHostEnvironment _hostingEnvironment;

        public ManagementRolesController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;

            _hostingEnvironment = hostingEnvironment;

            _repoCollection = new ManagementRolesRepository(configuration);
            _repoUser = new ManagementUserRepository(configuration);

        }

        public async Task<IActionResult> Index()
        {
            FilterModel filter = new FilterModel();

            ViewSysUserModel userInfo = SessionHelper.GetObjectFromJson<ViewSysUserModel>(this.HttpContext.Session, "UserInfo");

            Response resp = new Response();

            ViewSysRoleModel viewModel = new ViewSysRoleModel();

            List<ViewSysRoleGroupModel> viewRoleGroup = new List<ViewSysRoleGroupModel>();

            List<SysRoleGroup> header = new List<SysRoleGroup>();

            List<SysUser> user = new List<SysUser>();


            resp = await GET_HEADER(filter);

            if (resp.status == true)
            {
                var jsonOutput = JsonConvert.SerializeObject(resp.output_data);

                header = JsonConvert.DeserializeObject<List<SysRoleGroup>>(jsonOutput);
            }


            resp = await _repoUser.GET_ALL();

            if (resp.status == true)
            {
                var jsonOutput = JsonConvert.SerializeObject(resp.output_data);

                user = JsonConvert.DeserializeObject<List<SysUser>>(jsonOutput);
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

            foreach (var item in header)
            {
                int CountUser = 0;

                foreach (var listUser in user)
                {
                    if (listUser.Role == item.Value)
                    {
                        CountUser++;
                    }
                }

                ViewSysRoleGroupModel model = new ViewSysRoleGroupModel();

                List<SysRoleList> roleList = new List<SysRoleList>();

                model.Id = item.Id;
                model.Value = item.Value;
                model.Name = item.Name;
                model.Description = item.Description;
                model.UserCount = CountUser;

                viewRoleGroup.Add(model);

            }

            viewModel.viewSysRoleGroupModel.AddRange(viewRoleGroup);



            return View(viewModel);
        }


        #region MyRegion


        public async Task<JsonResult> GET_ALL([FromQuery] FilterModel param)
        {
            Response resp = new Response();

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

        public async Task<Response> GET_HEADER([FromQuery] FilterModel param)
        {
            Response resp = new Response();

            try
            {
                resp = _repoCollection.GET_ALL(param).Result;
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return resp;
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

        public async Task<JsonResult> INSERT([FromBody] SysRoleGroup model)
        {
            Response resp = new Response();

            SysUser userInfo = SessionHelper.GetObjectFromJson<SysUser>(this.HttpContext.Session, "UserInfo");

            try
            {
                if (model != null)
                {
                    model.CreateBy = Convert.ToString(userInfo.UserId);
                    model.UpdateBy = Convert.ToString(userInfo.UserId);

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

        public async Task<JsonResult> UPDATE([FromBody] SysRoleGroup model)
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



        #endregion



        #region LIST GET

        public async Task<JsonResult> LIST_GET_ALL([FromQuery] FilterModel param)
        {
            Response resp = new Response();

            ViewSysRoleModel viewModel = new ViewSysRoleModel();

            List<ViewSysRoleListModel> result = new List<ViewSysRoleListModel>();

            try
            {
                resp = _repoCollection.LIST_GET_ALL(param).Result;

                if (resp.status == true)
                {
                    var jsonOutput = JsonConvert.SerializeObject(resp.output_data);

                    viewModel = JsonConvert.DeserializeObject<ViewSysRoleModel>(jsonOutput);

                    result = viewModel.viewSysRoleListModel.OrderBy(x=>x.menuGroupSeq).ToList();
                }
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(result);
        }

        public async Task<JsonResult> UPDATE_ITEM([FromBody] SysRoleList model)
        {
            Response resp = new Response();

            SysUser userInfo = SessionHelper.GetObjectFromJson<SysUser>(this.HttpContext.Session, "UserInfo");

            try
            {
                if (model != null)
                {
                    model.CreateBy = Convert.ToString(userInfo.UserId);
                    model.UpdateBy = Convert.ToString(userInfo.UserId);

                    resp = await _repoCollection.LIST_UPDATE(model);
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



    }
}
