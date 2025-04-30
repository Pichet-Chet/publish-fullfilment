using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;

namespace FULFILLMENT.H4U.API.Service
{
    public class ManagementMenuService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();

        #region Menu Header
        public async Task<Response> GET_ALL(FilterModel param)
        {
            Response resp = new Response();

            List<SysMenuGroup> viewData = new List<SysMenuGroup>();

            try
            {
                viewData = await Task.Run(() => _dbContext.SysMenuGroups.OrderBy(x => x.MenuSequence).ThenBy(x => x.CreateBy).ToList());

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

            SysMenuGroup viewData = new SysMenuGroup();

            try
            {
                viewData = await Task.Run(() => _dbContext.SysMenuGroups.Where(x => x.Id == id).FirstOrDefault());

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

        public async Task<Response> INSERT(SysMenuGroup param)
        {
            Response resp = new Response();


            bool validate = true;

            try
            {
                using (_dbContext)
                {
                    var checkDuplication = _dbContext.SysMenuGroups.Where(x => x.MenuCode == param.MenuCode || x.MenuName == param.MenuName).FirstOrDefault();

                    if (checkDuplication != null)
                    {
                        validate = false;
                        resp.message = Constants.INSERT_DUPLICATE;
                    }

                    if (validate == true)
                    {

                        param.CreateDate = helper.GetDateTimeNow();
                        param.UpdateDate = helper.GetDateTimeNow();

                        await Task.Run(() => _dbContext.SysMenuGroups.Add(param));

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

        public async Task<Response> UPDATE(SysMenuGroup param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {
                    var checkDuplication = _dbContext.SysMenuGroups.Where(x => x.MenuCode == param.MenuCode && x.MenuName == param.MenuName && x.Id != param.Id).FirstOrDefault();

                    if (checkDuplication != null)
                    {
                        validate = false;
                        resp.message = Constants.UPDATE_DUPLICATE;
                    }

                    if (validate == true)
                    {
                        var update = await Task.Run(() => _dbContext.SysMenuGroups.Where(x => x.Id == param.Id).FirstOrDefault());

                        if (update != null)
                        {
                            update.MenuName = param.MenuName;
                            update.MenuDrsciption = param.MenuDrsciption;
                            update.MenuSequence = param.MenuSequence;
                            update.MenuIcon = param.MenuIcon;

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




        #region Menu List
        public async Task<Response> LIST_GET_ALL()
        {
            Response resp = new Response();

            List<SysMenuList> viewData = new List<SysMenuList>();

            try
            {
                viewData = await Task.Run(() => _dbContext.SysMenuLists.OrderBy(x => x.MenuGroup).ThenBy(x => x.MenuSequence).ThenBy(x => x.CreateBy).ToList());

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

        public async Task<Response> LIST_GET_DETAIL(int id)
        {
            Response resp = new Response();

            SysMenuList viewData = new SysMenuList();

            try
            {
                viewData = await Task.Run(() => _dbContext.SysMenuLists.Where(x => x.Id == id).FirstOrDefault());

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

        public async Task<Response> LIST_INSERT(SysMenuList param)
        {
            Response resp = new Response();

            List<SysRoleList> roleList = new List<SysRoleList>();

            bool validate = true;

            try
            {

                var checkDuplication = _dbContext.SysMenuLists.Where(x => x.MenuCode == param.MenuCode || x.MenuName == param.MenuName).FirstOrDefault();

                if (checkDuplication != null)
                {
                    validate = false;
                    resp.message = Constants.INSERT_DUPLICATE;
                }

                if (validate == true)
                {
                    param.CreateDate = helper.GetDateTimeNow();
                    param.UpdateDate = helper.GetDateTimeNow();

                    await Task.Run(() => _dbContext.SysMenuLists.Add(param));

                    _dbContext.SaveChanges();



                    var getAllroleGroup = _dbContext.SysRoleGroups.ToList();

                    foreach (var item in getAllroleGroup)
                    {
                        SysRoleList menu = new SysRoleList();

                        menu.RoleGroupId = item.Id;
                        menu.MenuGroupId = param.MenuGroup;
                        menu.MenuId = param.Id;
                        menu.CreateDate = helper.GetDateTimeNow();
                        menu.UpdateDate = helper.GetDateTimeNow();
                        menu.IsActive = true;

                        roleList.Add(menu);
                    }


                    await Task.Run(() => _dbContext.SysRoleLists.AddRangeAsync(roleList));

                    _dbContext.SaveChangesAsync();


                    resp.status = true;

                    resp.message = Constants.INSERT_SUCCESS;

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

        public async Task<Response> LIST_UPDATE(SysMenuList param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {
                    var checkDuplication = _dbContext.SysMenuLists.Where(x => x.MenuCode == param.MenuCode && x.MenuName == param.MenuName && x.Id != param.Id).FirstOrDefault();

                    if (checkDuplication != null)
                    {
                        validate = false;
                        resp.message = Constants.UPDATE_DUPLICATE;
                    }

                    if (validate == true)
                    {
                        var update = await Task.Run(() => _dbContext.SysMenuLists.Where(x => x.Id == param.Id).FirstOrDefault());

                        if (update != null)
                        {
                            update.MenuName = param.MenuName;
                            update.MenuDesciption = param.MenuDesciption;
                            update.MenuSequence = param.MenuSequence;
                            update.MenuIcon = param.MenuIcon;
                            update.Controller = param.Controller;
                            update.Action = param.Action;
                            update.UpdateDate = helper.GetDateTimeNow();
                            update.IsActive = param.IsActive;
                            update.MenuGroup = param.MenuGroup;

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
    }
}
