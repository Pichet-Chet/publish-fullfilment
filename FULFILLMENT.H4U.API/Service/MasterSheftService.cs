using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Reponse;
using System.Diagnostics;
using FULFILLMENT.H4U.API.Service.Helper;
using FULFILLMENT.H4U.API.Model.Custom;

namespace FULFILLMENT.H4U.API.Service
{
    public class MasterSheftService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();

        public async Task<Response> GET_ALL()
        {
            Response resp = new Response();

            try
            {
                var getData = await Task.Run(() => _dbContext.MasterShefts.ToList());

                if (getData.Count > 0)
                {
                    foreach (var item in getData)
                    {
                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));

                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                        item.CreateBy = getUserCrete;

                        item.UpdateBy = getUserUpdate;
                    }

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
        public async Task<Response> GET_FILTER(FilterModel param)
        {
            Response resp = new Response();

            List<ViewMasterSheftModel> viewData = new List<ViewMasterSheftModel>();


            try
            {
                var queryable = await Task.Run(() => _dbContext.MasterShefts.AsQueryable());

                if (!string.IsNullOrEmpty(param.name))
                {
                    queryable = queryable.Where(x => param.name.Contains(x.SheftName)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.description))
                {
                    queryable = queryable.Where(x => param.description.Contains(x.SheftDescription)).AsQueryable();
                }

                if (param.isActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.isActive).AsQueryable();
                }

                var getData = queryable.ToList();

                if (getData.Count > 0)
                {
                    var queryableMasterLocation = _dbContext.MasterLocations.AsQueryable();

                    foreach (var item in getData)
                    {
                        ViewMasterSheftModel viewModel = new ViewMasterSheftModel();

                        var DataLocation = queryableMasterLocation.Where(x => x.LocationId == item.LocationId).FirstOrDefault();

                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));

                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));


                        viewModel.SheftId = item.SheftId;
                        viewModel.SheftName = item.SheftName;
                        viewModel.SheftDescription = item.SheftDescription;
                        viewModel.CreateDate = item.CreateDate;
                        viewModel.UpdateDate = item.UpdateDate;
                        viewModel.CreateBy = getUserCrete;
                        viewModel.UpdateBy = getUserUpdate;
                        viewModel.IsActive = item.IsActive;
                        viewModel.LocationId = item.LocationId;

                        viewModel.LocationName = DataLocation.LocationName;
                        viewModel.LocationDescription = DataLocation.LocationDescription;
                        viewModel.LocationType = DataLocation.LocationType;


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
        public async Task<Response> GET_ACTIVE()
        {
            Response resp = new Response();

            try
            {
                var getData = await Task.Run(() => _dbContext.MasterShefts.Where(x => x.IsActive == true).ToList());

                if (getData.Count > 0)
                {
                    foreach (var item in getData)
                    {
                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));

                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                        item.CreateBy = getUserCrete;

                        item.UpdateBy = getUserUpdate;
                    }

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
        public async Task<Response> GET_DETAIL(int id)
        {
            Response resp = new Response();

            try
            {
                var getData = await Task.Run(() => _dbContext.MasterShefts.Where(x => x.SheftId == id).FirstOrDefault());

                if (getData != null)
                {
                    var getUserCrete = helper.GetUserId(Convert.ToInt32(getData.CreateBy));

                    var getUserUpdate = helper.GetUserId(Convert.ToInt32(getData.UpdateBy));

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
        public async Task<Response> SEARCH(string textSearch)
        {
            Response resp = new Response();

            List<MasterSheft> listData = new List<MasterSheft>();

            try
            {
                if (!string.IsNullOrEmpty(textSearch))
                {
                    listData = await Task.Run(() => _dbContext.MasterShefts.Where(x =>
                    (x.SheftId + x.SheftName + x.SheftDescription + x.CreateBy + x.UpdateBy)
                    .Contains(textSearch)).ToList());
                }
                else
                {
                    listData = await Task.Run(() => _dbContext.MasterShefts.ToList());
                }

                if (listData != null)
                {
                    resp.status = true;

                    resp.message = Constants.GET_DATA_SUCCESS;

                    resp.output_data = listData;
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
        public async Task<Response> INSERT(MasterSheft param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {
                    var checkDuplication = _dbContext.MasterShefts.Where(x => x.SheftName == param.SheftName).FirstOrDefault();

                    var checkLocation = _dbContext.MasterLocations.Where(x => x.LocationId == param.LocationId).FirstOrDefault();


                    if (checkDuplication != null)
                    {
                        validate = false;

                        resp.message = Constants.INSERT_DUPLICATE;
                    }

                    if (checkLocation == null)
                    {
                        validate = false;

                        resp.message = Constants.INSERT_DATA_INVALID;
                    }


                    if (validate == true)
                    {
                        param.CreateDate = helper.GetDateTimeNow();

                        param.UpdateDate = helper.GetDateTimeNow();

                        await Task.Run(() => _dbContext.MasterShefts.Add(param));

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
        public async Task<Response> UPDATE(MasterSheft param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {

                    var checkDuplication = _dbContext.MasterShefts.Where(x => x.SheftName == param.SheftName && x.SheftId != param.SheftId).FirstOrDefault();

                    var checkLocation = _dbContext.MasterLocations.Where(x => x.LocationId == param.LocationId).FirstOrDefault();

                    if (checkDuplication != null)
                    {
                        validate = false;

                        resp.message = Constants.UPDATE_DUPLICATE;
                    }

                    if (checkLocation == null)
                    {
                        validate = false;

                        resp.message = Constants.INSERT_DATA_INVALID;
                    }


                    if (validate == true)
                    {
                        var update = await Task.Run(() => _dbContext.MasterShefts.Where(x => x.SheftId == param.SheftId).FirstOrDefault());

                        if (update != null)
                        {
                            update.LocationId = param.LocationId;
                            //update.SheftName = param.SheftName;
                            update.SheftDescription = param.SheftDescription;
                            //update.CreateBy = param.CreateBy;
                            //update.CreateDate = param.CreateDate;
                            update.UpdateBy = param.UpdateBy;
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
        public async Task<Response> DELETE(int id)
        {
            Response resp = new Response();

            try
            {
                using (_dbContext)
                {
                    var delete = await Task.Run(() => _dbContext.MasterShefts.Find(id));

                    if (delete != null)
                    {
                        _dbContext.MasterShefts.Remove(delete);

                        _dbContext.SaveChanges();

                        resp.status = true;
                        resp.message = Constants.DELETE_SUCCESS;
                    }
                    else
                    {
                        resp.status = false;
                        resp.message = Constants.DELETE_ERROR;
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
