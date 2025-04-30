using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;

namespace FULFILLMENT.H4U.API.Service
{
    public class ManagementRolesService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();

        #region Role Header


        #region Menu Header
        public async Task<Response> GET_ALL(FilterModel param)
        {
            Response resp = new Response();

            List<SysRoleGroup> viewData = new List<SysRoleGroup>();

            try
            {
                viewData = await Task.Run(() => _dbContext.SysRoleGroups.OrderBy(x => x.Value).ThenBy(x => x.Name).ToList());

                if (viewData.Count > 0)
                {
                    foreach (var item in viewData)
                    {
                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                        item.CreateBy = getUserCrete;
                        item.UpdateBy = getUserUpdate;

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

        public async Task<Response> GET_DETAIL(int id)
        {
            Response resp = new Response();

            SysRoleGroup viewData = new SysRoleGroup();

            try
            {
                viewData = await Task.Run(() => _dbContext.SysRoleGroups.Where(x => x.Id == id).FirstOrDefault());

                if (viewData != null)
                {
                    var getUserCrete = helper.GetUserId(Convert.ToInt32(viewData.CreateBy));
                    var getUserUpdate = helper.GetUserId(Convert.ToInt32(viewData.UpdateBy));

                    viewData.CreateBy = getUserCrete;
                    viewData.UpdateBy = getUserUpdate;


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

        public async Task<Response> INSERT(SysRoleGroup param)
        {
            Response resp = new Response();

            List<SysRoleList> roleList = new List<SysRoleList>();

            bool validate = true;

            try
            {
                using (_dbContext)
                {
                    var checkDuplication = _dbContext.SysRoleGroups.Where(x => x.Value == param.Value || x.Name == param.Name).FirstOrDefault();

                    if (checkDuplication != null)
                    {
                        validate = false;
                        resp.message = Constants.INSERT_DUPLICATE;
                    }

                    if (validate == true)
                    {
                        param.CreateDate = helper.GetDateTimeNow();

                        await Task.Run(() => _dbContext.SysRoleGroups.Add(param));

                        _dbContext.SaveChanges();

                        // Insert Menu to role group

                        var getDataRoleGroupFirst = await Task.Run(() => _dbContext.SysRoleGroups.FirstOrDefault());

                        var getDataRoleList = await Task.Run(() => _dbContext.SysRoleLists.Where(x => x.RoleGroupId == getDataRoleGroupFirst.Id).ToList());

                        foreach (var item in getDataRoleList)
                        {
                            SysRoleList menu = new SysRoleList();

                            menu.RoleGroupId = param.Id;
                            menu.MenuGroupId = item.MenuGroupId;
                            menu.MenuId = item.MenuId;
                            menu.CreateDate = helper.GetDateTimeNow();
                            menu.UpdateDate = helper.GetDateTimeNow();
                            menu.CreateBy = param.CreateBy;
                            menu.UpdateBy = param.UpdateBy;
                            menu.IsActive = false;

                            roleList.Add(menu);
                        }

                        if (roleList.Count > 0)
                        {
                            _dbContext.AddRangeAsync(roleList);

                            _dbContext.SaveChangesAsync();
                        }



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

        public async Task<Response> UPDATE(SysRoleGroup param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {
                    var checkDuplication = _dbContext.SysRoleGroups.Where(x => x.Value == param.Value && x.Name == param.Name && x.Id != param.Id).FirstOrDefault();

                    if (checkDuplication != null)
                    {
                        validate = false;
                        resp.message = Constants.UPDATE_DUPLICATE;
                    }

                    if (validate == true)
                    {
                        var update = await Task.Run(() => _dbContext.SysRoleGroups.Where(x => x.Id == param.Id).FirstOrDefault());

                        if (update != null)
                        {
                            update.Name = param.Name;
                            update.Description = param.Description;
                            update.UpdateDate = helper.GetDateTimeNow();
                            update.IsActive = param.IsActive;

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
        #endregion



        #endregion

        #region Role List

        public async Task<Response> LIST_GET_ALL(FilterModel param)
        {
            Response resp = new Response();

            ViewSysRoleModel viewModel = new ViewSysRoleModel();

            List<SysRoleList> viewData = new List<SysRoleList>();

            try
            {
                viewData = await Task.Run(() => _dbContext.SysRoleLists.Where(x => x.RoleGroupId == param.roleGroupId).OrderBy(x => x.RoleGroupId).ToList());

                if (viewData.Count > 0)
                {
                    var queryableRoleGroupId = _dbContext.SysRoleGroups.AsQueryable();
                    var queryableMenuGroupId = _dbContext.SysMenuGroups.AsQueryable();
                    var queryableMenuId = _dbContext.SysMenuLists.AsQueryable();

                    foreach (var item in viewData)
                    {
                        ViewSysRoleModel model = new ViewSysRoleModel();

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

                        viewModel.viewSysRoleListModel.Add(obj);
                    }

                    viewModel.viewSysRoleListModel.OrderBy(x => x.menuGroupSeq).ThenBy(x => x.menuSeq).ToList();

                    resp.status = true;

                    resp.message = Constants.GET_DATA_SUCCESS;

                    resp.output_data = viewModel;
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


        public async Task<Response> LIST_UPDATE(SysRoleList param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {
                    var update = await Task.Run(() => _dbContext.SysRoleLists.Where(x => x.Id == param.Id).FirstOrDefault());

                    if (update != null)
                    {
                        update.UpdateBy = param.UpdateBy;
                        update.UpdateDate = helper.GetDateTimeNow();
                        update.IsActive = update.IsActive == true ? false : true;

                        _dbContext.SaveChanges();

                        resp.status = true;
                        resp.message = Constants.UPDATE_SUCCESS;
                        resp.output_data = update.RoleGroupId;
                    }
                    else
                    {
                        resp.status = false;
                        resp.message = Constants.UPDATE_ERROR;
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



        #endregion
    }
}
