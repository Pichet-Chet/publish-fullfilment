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
namespace FULFILLMENT_H4U.Controllersf
{
    public class MasterProductController : Controller
    {
        IMasterProductRepository _repoCollection;
        private IWebHostEnvironment _hostingEnvironment;

        public MasterProductController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _repoCollection = new MasterProductRepository(configuration);
            _hostingEnvironment = environment;

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
        public JsonResult INSERT([FromBody] MasterProduct model)
        {
            Response resp = new Response();

            SysUser userInfo = SessionHelper.GetObjectFromJson<SysUser>(this.HttpContext.Session, "UserInfo");

            try
            {
                if (model != null)
                {
                    model.CreateBy = Convert.ToString(userInfo.UserId);
                    model.UpdateBy = Convert.ToString(userInfo.UserId);
                    model.VendorId = userInfo.VenderId;

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

        public JsonResult UPDATE([FromBody] MasterProduct model)
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



        public async Task<JsonResult> GetFile(int productId)
        {
            Response resp = new Response();

            MasterProduct masterProduct = new MasterProduct();

            List<FileViewModel> fileViewModels = new List<FileViewModel>();

            try
            {
                resp = await Task.Run(() => _repoCollection.GET_DETAIL(productId));

                var jsonData = JsonConvert.SerializeObject(resp.output_data);

                masterProduct = JsonConvert.DeserializeObject<MasterProduct>(jsonData);

                if (!string.IsNullOrEmpty(masterProduct.FileLocation))
                {
                    DirectoryInfo fileDirectory = new DirectoryInfo(masterProduct.FileLocation);

                    foreach (FileInfo filex in fileDirectory.GetFiles())
                    {
                        byte[] imageArray = System.IO.File.ReadAllBytes(filex.FullName);
                        string base64ImageRepresentation = Convert.ToBase64String(imageArray);


                        FileViewModel fileViewModel = new FileViewModel();

                        var getFilename = Convert.ToString(filex.Name);

                        fileViewModel.Name = getFilename.Substring(0,20);
                        fileViewModel.FullName = filex.FullName;
                        fileViewModel.Location = filex.FullName;
                        fileViewModel.Base64 = base64ImageRepresentation;

                        fileViewModels.Add(fileViewModel);
                    }

                    resp.status = true;
                    resp.output_data = fileViewModels;
                }
                else
                {
                    resp.status = false;
                    resp.message = Constants.NOT_FOUND_FILE_UPLOAD;
                }

            }
            catch (Exception)
            {

                throw;
            }

            return new JsonResult(resp);
        }

        [HttpPost]
        public async Task<JsonResult> UploadFile(int productId)
        {
            Response resp = new Response();

            MasterProduct masterProduct = new MasterProduct();

            try
            {
                int runningFile = 0;

                resp = await Task.Run(() => _repoCollection.GET_DETAIL(productId));

                var jsonData = JsonConvert.SerializeObject(resp.output_data);

                masterProduct = JsonConvert.DeserializeObject<MasterProduct>(jsonData);

                var setLocationPath = "Uploads" + @"\" + "MasterProduct" + @"\" + masterProduct.ProductSku;

                if (Request.Form.Files.Count >= 1)
                {
                    foreach (var file in Request.Form.Files)
                    {

                        string uploads = Path.Combine(_hostingEnvironment.WebRootPath, setLocationPath);
                        string fileType = "";

                        if (!System.IO.Directory.Exists(uploads))
                        {
                            System.IO.Directory.CreateDirectory(uploads);
                        }

                        SysUser userInfo = SessionHelper.GetObjectFromJson<SysUser>(this.HttpContext.Session, "UserInfo");

                        masterProduct.FileLocation = uploads;
                        masterProduct.CreateBy = Convert.ToString(userInfo.UserId);
                        masterProduct.UpdateBy = Convert.ToString(userInfo.UserId);

                        resp = await Task.Run(() => _repoCollection.UPDATE(masterProduct));

                        if (file.ContentType == "image/png")
                        {
                            fileType = ".png";
                        }
                        else if (file.ContentType == "image/jpeg")
                        {
                            fileType = ".jpg";

                        }

                        string fileName = Guid.NewGuid().ToString().Substring(0,20) + fileType;

                        if (file.Length > 0)
                        {
                            using (FileStream fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                        }
                    }
                }
                else
                {
                    resp.status = false;
                    resp.message = Constants.NOT_FOUND_FILE_UPLOAD;

                    return new JsonResult(resp);
                }



            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
                resp.inner_exception = ex.InnerException == null ? "" : ex.InnerException.ToString();
            }

            return new JsonResult(resp);

        }

        [HttpPost]
        public async Task<JsonResult> DeleteFile(string filename , int productId)
        {
            Response resp = new Response();

            MasterProduct masterProduct = new MasterProduct();

            try
            {

                resp = await Task.Run(() => _repoCollection.GET_DETAIL(productId));

                var jsonData = JsonConvert.SerializeObject(resp.output_data);

                masterProduct = JsonConvert.DeserializeObject<MasterProduct>(jsonData);

                DirectoryInfo fileDirectory = new DirectoryInfo(masterProduct.FileLocation);

                foreach (FileInfo filex in fileDirectory.GetFiles())
                {
                    if (filex.Name.Contains(filename))
                    {
                        filex.Delete();
                    }

                    
                }

                resp.status = false;
                resp.message = Constants.NOT_FOUND_FILE_UPLOAD;

                return new JsonResult(resp);

            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
                resp.inner_exception = ex.InnerException == null ? "" : ex.InnerException.ToString();
            }

            return new JsonResult(resp);

        }
    }
}
