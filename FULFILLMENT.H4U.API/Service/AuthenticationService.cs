using System.Text;
using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;

namespace FULFILLMENT.H4U.API.Service
{
    public class AuthenticationService
    {
        HelperService helper = new HelperService();

        readonly DataContext _dbContext = new();

        public async Task<Response> SIGN_IN(SysUser param)
        {
            Response resp = new Response();

            ViewSysUserModel viewModel = new ViewSysUserModel();

            MasterVendor vendorMaster = new MasterVendor();

            List<SysRoleList> listRole = new List<SysRoleList>();

            List<SysMenuGroup> menuGroupList = new List<SysMenuGroup>();
            List<SysMenuList> menuList = new List<SysMenuList>();

            try
            {


                var getData = await Task.Run(() => _dbContext.SysUsers.Where(x => x.Email.ToUpper() == param.Email.ToUpper() && x.UserPassword == Convert.ToBase64String(Encoding.UTF8.GetBytes(param.UserPassword))).FirstOrDefault());

                if (getData != null)
                {
                    vendorMaster = _dbContext.MasterVendors.Where(x => x.VendorId == getData.VenderId).FirstOrDefault();

                    if (getData.IsActive == true)
                    {
                        var getMenuHeaderId = _dbContext.SysRoleGroups.Where(x => x.Value == getData.Role).FirstOrDefault();

                        listRole = await Task.Run(() => _dbContext.SysRoleLists.Where(x => x.RoleGroupId == getMenuHeaderId.Id).OrderBy(x => x.RoleGroupId).ToList());

                        if (listRole.Count > 0)
                        {
                            var queryableRoleGroupId = _dbContext.SysRoleGroups.AsQueryable();
                            var queryableMenuGroupId = _dbContext.SysMenuGroups.AsQueryable();
                            var queryableMenuId = _dbContext.SysMenuLists.AsQueryable();

                            foreach (var item in listRole.Where(x => x.IsActive == true))
                            {
                                SysMenuGroup menuGroupData = new SysMenuGroup();
                                SysMenuList menuListData = new SysMenuList();

                                menuGroupData = _dbContext.SysMenuGroups.Where(x => x.Id == item.MenuGroupId).FirstOrDefault();
                                menuListData = _dbContext.SysMenuLists.Where(x => x.Id == item.MenuId && x.IsActive == true).FirstOrDefault();

                                menuGroupList.Add(menuGroupData);

                                if (menuListData != null)
                                {
                                    menuList.Add(menuListData);
                                }

                                ViewSysRoleListModel obj = new ViewSysRoleListModel();

                                var getRoleGroup = queryableRoleGroupId.Where(x => x.Id == item.RoleGroupId).FirstOrDefault();
                                var getMenuGroup = queryableMenuGroupId.Where(x => x.Id == item.MenuGroupId).FirstOrDefault();
                                var getMenu = queryableMenuId.Where(x => x.Id == item.MenuId).FirstOrDefault();
                                var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                                var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                                obj.Id = item.Id;
                                obj.RoleGroupId = item.RoleGroupId;
                                obj.MenuGroupId = item.MenuGroupId;
                                obj.MenuId = item.MenuId;
                                obj.CreateBy = getUserCrete;
                                obj.UpdateBy = getUserUpdate;
                                obj.IsActive = item.IsActive;
                                obj.CreateDate = item.CreateDate;
                                obj.UpdateDate = item.UpdateDate;

                                obj.roleGroupName = getRoleGroup.Name;
                                obj.menuGroupName = getMenuGroup.MenuName;
                                obj.menuGroupSeq = getMenuGroup.MenuSequence;
                                obj.menuName = getMenu.MenuName;
                                obj.menuSeq = getMenu.MenuSequence;

                                viewModel.roleAccess.Add(obj);
                            }
                        }

                        viewModel.UserId = getData.UserId;
                        viewModel.UserName = getData.UserName;
                        viewModel.UserPassword = getData.UserPassword;
                        viewModel.FirstNameTh = getData.FirstNameTh;
                        viewModel.LastNameTh = getData.LastNameTh;
                        viewModel.FirstNameEn = getData.FirstNameEn;
                        viewModel.LastNameEn = getData.LastNameEn;
                        viewModel.Mobile = getData.Mobile;
                        viewModel.Email = getData.Email;
                        viewModel.CreateBy = getData.CreateBy;
                        viewModel.CreateDate = getData.CreateDate;
                        viewModel.UpdateBy = getData.UpdateBy;
                        viewModel.UpdateDate = getData.UpdateDate;
                        viewModel.IsActive = getData.IsActive;
                        viewModel.VenderId = getData.VenderId;
                        viewModel.vendorEmail = vendorMaster.Email;
                        viewModel.SecretKey = getData.SecretKey;
                        viewModel.ApplicationName = getData.ApplicationName;
                        viewModel.Role = getData.Role;


                        viewModel.VendorName = vendorMaster.VendorName;
                        viewModel.ContactName = vendorMaster.ContactName;
                        viewModel.ContactTel = vendorMaster.ContactTel;
                        viewModel.ContactAddress = vendorMaster.ContactAddress;
                        viewModel.vendorEmail = vendorMaster.Email;
                        viewModel.vendorBalance = vendorMaster.Balance;

                        menuGroupList = menuGroupList.GroupBy(x => x.Id).Select(d => d.First()).ToList();


                        viewModel.menuAccess = menuList;
                        viewModel.groupAccess = menuGroupList;




                        resp.status = true;

                        resp.message = Constants.SIGN_IN_SUCCESS;

                        resp.output_data = viewModel;
                    }
                    else
                    {
                        resp.status = false;

                        resp.message = Constants.ACCOUNT_BANNED;
                    }
                }
                else
                {
                    resp.status = false;
                    resp.message = Constants.SIGN_IN_ERROR;
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
        public async Task<Response> SIGN_UP(SysUser param)
        {
            Response resp = new Response();

            try
            {
                var checkData = await Task.Run(() => _dbContext.SysUsers.Where(x => x.UserName.ToUpper() == param.UserName.ToUpper()).FirstOrDefault());


                if (checkData != null)
                {
                    resp.status = false;
                    resp.message = Constants.SIGN_UP_USERNAME_ALREADY;
                }
                else if (checkData == null)
                {
                    checkData = await Task.Run(() => _dbContext.SysUsers.Where(x => x.Email.ToUpper() == param.Email.ToUpper()).FirstOrDefault());

                    if (checkData != null)
                    {
                        resp.status = false;
                        resp.message = Constants.SIGN_UP_EMAIL_ALREADY;
                    }
                }

                if (checkData == null)
                {
                    var GetDataVendorCode = await Task.Run(() => _dbContext.MasterVendors.Where(x => x.SerectKey == param.SecretKey).FirstOrDefault());

                    if (GetDataVendorCode == null)
                    {
                        resp.status = false;
                        resp.message = Constants.SIGN_UP_USER_VENDOR_FAILD;
                    }
                    else
                    {
                        if (param.SecretKey != GetDataVendorCode.SerectKey)
                        {
                            resp.status = false;
                            resp.message = Constants.SIGN_UP_USER_SECRET_FAILD;
                        }
                        else
                        {
                            param.UserPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(param.UserPassword));
                            param.IsActive = true;
                            param.CreateBy = param.CreateBy;
                            param.CreateDate = helper.GetDateTimeNow();
                            param.UpdateBy = param.UpdateBy;
                            param.UpdateDate = helper.GetDateTimeNow();
                            param.SecretKey = param.SecretKey;
                            param.VenderId = GetDataVendorCode.VendorId;
                            param.Role = Constants.SIGN_UP_INITIAL_ROLE;

                            await Task.Run(() => _dbContext.SysUsers.Add(param));

                            _dbContext.SaveChanges();

                            resp.status = true;

                            resp.message = Constants.SIGN_UP_SUCCESS;
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
        public async Task<Response> RESET_PASSWORD(SysUser param)
        {
            Response resp = new Response();

            try
            {
                var getData = await Task.Run(() => _dbContext.SysUsers.Where(x => x.Email == param.UserName && x.SecretKey == param.SecretKey).FirstOrDefault());

                if (getData != null)
                {
                    getData.UserPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(param.UserPassword));

                    _dbContext.SaveChanges();

                    resp.status = true;

                    resp.message = Constants.SIGN_IN_SUCCESS;

                    resp.output_data = getData.UserPassword;
                }
                else
                {
                    resp.status = false;

                    resp.message = Constants.SIGN_IN_SUCCESS;

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
    }
}
