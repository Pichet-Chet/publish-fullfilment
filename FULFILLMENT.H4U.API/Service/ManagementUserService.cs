using System.Text;
using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;
using Org.BouncyCastle.Asn1.Cms;

namespace FULFILLMENT.H4U.API.Service
{
    public class ManagementUserService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();

        public async Task<Response> GET_ALL()
        {
            Response resp = new Response();

            List<ViewSysUserModel> viewData = new List<ViewSysUserModel>();

            List<SysUser> getData = new List<SysUser>();

            try
            {
                getData = await Task.Run(() => _dbContext.SysUsers.ToList());

                if (getData.Count > 0)
                {
                    var queryableMasterVendor = _dbContext.MasterVendors.AsQueryable();


                    foreach (var item in getData)
                    {
                        ViewSysUserModel viewModel = new ViewSysUserModel();

                        var DataVendor = queryableMasterVendor.Where(x => x.VendorId == item.VenderId).FirstOrDefault();

                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));

                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));


                        viewModel.UserId = item.UserId;
                        viewModel.UserName = item.UserName;
                        viewModel.UserPassword = Guid.NewGuid().ToString();
                        viewModel.FirstNameTh = item.FirstNameTh;
                        viewModel.LastNameTh = item.LastNameTh;
                        viewModel.FirstNameEn = item.FirstNameEn;
                        viewModel.LastNameEn = item.LastNameEn;
                        viewModel.Mobile = item.Mobile;
                        viewModel.Email = item.Email;
                        viewModel.CreateDate = item.CreateDate;
                        viewModel.UpdateDate = item.UpdateDate;
                        viewModel.IsActive = item.IsActive;
                        viewModel.VenderId = item.VenderId;
                        viewModel.SecretKey = item.SecretKey;
                        viewModel.Role = item.Role;
                        viewModel.ApplicationName = item.ApplicationName;
                        viewModel.CreateBy = getUserCrete;
                        viewModel.UpdateBy = getUserUpdate;

                        viewModel.VendorName = DataVendor.VendorName;
                        viewModel.ContactName = DataVendor.ContactName;
                        viewModel.ContactTel = DataVendor.ContactTel;
                        viewModel.ContactAddress = DataVendor.ContactAddress;
                        viewModel.LineId = DataVendor.LineId;
                        viewModel.Website = DataVendor.Website;
                        viewModel.vendorEmail = DataVendor.Email;

                        viewData.Add(viewModel);
                    }

                    resp.status = true;

                    resp.message = Constants.GET_DATA_SUCCESS;

                    resp.output_data = viewData;
                }
                else
                {
                    resp.status = true;
                    resp.message = Constants.DATA_NOT_FOUND;
                    resp.output_data = null;
                }
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
                resp.inner_exception = ex.InnerException == null ? "" : ex.InnerException.ToString();
            }

            return resp;
        }
        public async Task<Response> GET_COUNT()
        {
            Response resp = new Response();

            UserCount userCount = new UserCount();

            try
            {
                var getData = await Task.Run(() => _dbContext.SysUsers.ToList());

                if (getData.Count > 0)
                {
                    userCount.TotalUser = getData.Count();

                    userCount.TotalActive = getData.Where(x => x.IsActive == true).Count();

                    userCount.TotalInactive = getData.Where(x => x.IsActive == false).Count();

                    userCount.TotalPending = getData.Where(x => x.Role == Constants.SIGN_UP_INITIAL_ROLE).Count();

                    resp.status = true;

                    resp.message = Constants.GET_DATA_SUCCESS;

                    resp.output_data = userCount;
                }
                else
                {
                    resp.status = true;
                    resp.message = Constants.DATA_NOT_FOUND;
                    resp.output_data = null;
                }
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
                resp.inner_exception = ex.InnerException == null ? "" : ex.InnerException.ToString();
            }

            return resp;
        }
        public async Task<Response> GET_DETAIL(int id)
        {
            Response resp = new Response();

            try
            {
                var getData = await Task.Run(() => _dbContext.SysUsers.Where(x => x.UserId == id).FirstOrDefault());

                if (getData != null)
                {
                    var getUserCrete = helper.GetUserId(Convert.ToInt32(getData.CreateBy));

                    var getUserUpdate = helper.GetUserId(Convert.ToInt32(getData.UpdateBy));

                    var base64Bytes = System.Convert.FromBase64String(getData.UserPassword);

                    getData.UserPassword = System.Text.Encoding.UTF8.GetString(base64Bytes);

                    getData.CreateBy = getUserCrete;

                    getData.UpdateBy = getUserUpdate;

                    resp.status = true;

                    resp.message = Constants.GET_DATA_SUCCESS;

                    resp.output_data = getData;
                }
                else
                {
                    resp.status = true;
                    resp.message = Constants.DATA_NOT_FOUND;
                    resp.output_data = null;
                }
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
                resp.inner_exception = ex.InnerException == null ? "" : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> UPDATE(SysUser param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {
                    var checkDuplication = _dbContext.SysUsers.Where(x => x.UserName == param.UserName && x.Email == param.Email && x.UserId != param.UserId).FirstOrDefault();

                    var checkDataRole = _dbContext.SysRoleGroups.Where(x => param.Role.Contains(x.Value)).FirstOrDefault();

                    var checkDataVendor = _dbContext.MasterVendors.Where(x => x.VendorId == param.VenderId).FirstOrDefault();


                    if (checkDuplication != null)
                    {
                        validate = false;
                        resp.message = Constants.UPDATE_DUPLICATE;
                    }

                    if (checkDataRole == null)
                    {
                        validate = false;
                        resp.message = Constants.UPDATE_DATA_INVALID;
                    }

                    if (checkDataVendor == null)
                    {
                        validate = false;
                        resp.message = Constants.UPDATE_DATA_INVALID;
                    }


                    if (validate == true)
                    {
                        var update = await Task.Run(() => _dbContext.SysUsers.Where(x => x.UserId == param.UserId).FirstOrDefault());

                        if (update != null)
                        {
                            update.FirstNameEn = param.FirstNameEn;
                            update.LastNameEn = param.LastNameEn;
                            update.FirstNameTh = param.FirstNameTh;
                            update.LastNameTh = param.LastNameTh;
                            update.Mobile = param.Mobile;
                            update.UpdateBy = param.UpdateBy;
                            update.UpdateDate = helper.GetDateTimeNow();
                            update.IsActive = param.IsActive;
                            update.Role = param.Role;
                            update.Email = param.Email;
                            update.VenderId = param.VenderId;

                            _dbContext.SaveChanges();

                            resp.status = true;
                            resp.message = Constants.UPDATE_SUCCESS;
                        }
                        else
                        {
                            resp.status = false;
                            resp.message = Constants.UPDATE_ERROR;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
                resp.inner_exception = ex.InnerException == null ? "" : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> INSERT(SysUser param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {
                    var checkDuplication = _dbContext.SysUsers.Where(x => x.UserName == param.UserName && x.Email == param.Email && x.UserId != param.UserId).FirstOrDefault();

                    var checkDataRole = _dbContext.SysRoleGroups.Where(x => param.Role.Contains(x.Value)).FirstOrDefault();

                    var checkDataVendor = _dbContext.MasterVendors.Where(x => x.VendorId == param.VenderId).FirstOrDefault();


                    if (checkDuplication != null)
                    {
                        validate = false;
                        resp.message = Constants.UPDATE_DUPLICATE;
                    }

                    if (checkDataRole == null)
                    {
                        validate = false;
                        resp.message = Constants.UPDATE_DATA_INVALID;
                    }

                    if (checkDataVendor == null)
                    {
                        validate = false;
                        resp.message = Constants.UPDATE_DATA_INVALID;
                    }


                    if (validate == true)
                    {
                        param.CreateDate = helper.GetDateTimeNow();

                        param.UpdateDate = helper.GetDateTimeNow();

                        param.SecretKey = "N/A";

                        await Task.Run(() => _dbContext.SysUsers.Add(param));

                        _dbContext.SaveChanges();

                        resp.status = true;

                        resp.message = Constants.INSERT_SUCCESS;
                    }
                }
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
                resp.inner_exception = ex.InnerException == null ? "" : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> CHANGE_PASSWORD(SysUser param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {

                    if (validate == true)
                    {
                        var update = await Task.Run(() => _dbContext.SysUsers.Where(x => x.UserId == param.UserId).FirstOrDefault());

                        if (update != null)
                        {
                            update.UserPassword = param.UserPassword;
                            update.UpdateBy = param.UpdateBy;
                            update.UpdateDate = helper.GetDateTimeNow();

                            _dbContext.SaveChanges();

                            resp.status = true;
                            resp.message = Constants.UPDATE_SUCCESS;
                        }
                        else
                        {
                            resp.status = false;
                            resp.message = Constants.UPDATE_ERROR;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
                resp.inner_exception = ex.InnerException == null ? "" : ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> EDIT_PROFILE(SysUser param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {

                    if (validate == true)
                    {
                        var update = await Task.Run(() => _dbContext.SysUsers.Where(x => x.UserId == param.UserId).FirstOrDefault());

                        if (update != null)
                        {
                            update.UserPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(param.UserPassword));

                            update.FirstNameEn = param.FirstNameEn;
                            update.LastNameEn = param.LastNameEn;
                            update.FirstNameTh = param.FirstNameTh;
                            update.LastNameTh = param.LastNameTh;
                            update.Mobile = param.Mobile;

                            update.UpdateBy = param.UpdateBy;
                            update.UpdateDate = helper.GetDateTimeNow();

                            _dbContext.SaveChanges();

                            resp.status = true;
                            resp.message = Constants.UPDATE_SUCCESS;
                        }
                        else
                        {
                            resp.status = false;
                            resp.message = Constants.UPDATE_ERROR;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
                resp.inner_exception = ex.InnerException == null ? "" : ex.InnerException.ToString();
            }

            return resp;
        }


    }
}
