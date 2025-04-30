using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Reponse;
using System.Diagnostics;
using FULFILLMENT.H4U.API.Service.Helper;
using System.ComponentModel.DataAnnotations;
using FULFILLMENT.H4U.API.Model.Custom;

namespace FULFILLMENT.H4U.API.Service
{
    public class MasterShippingService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();
        public async Task<Response> GET_ALL(FilterModel param)
        {
            Response resp = new Response();

            try
            {
                var queryable = await Task.Run(() => _dbContext.MasterShippings.AsQueryable());

                if (!string.IsNullOrEmpty(param.name))
                {
                    queryable = queryable.Where(x => param.name.Contains(x.NameEn) || param.name.Contains(x.NameTh)).AsQueryable();
                }


                if (param.isActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.isActive).AsQueryable();
                }

                var getData = queryable.ToList();

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
                var getData = await Task.Run(() => _dbContext.MasterShippings.Where(x => x.Id == id).FirstOrDefault());

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
        public async Task<Response> INSERT(MasterShipping param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {
                    var checkDuplication = _dbContext.MasterShippings.Where(x => x.Value == param.Value && x.NameEn == param.NameEn && x.NameTh == param.NameTh).FirstOrDefault();

                    if (checkDuplication != null)
                    {
                        validate = false;

                        resp.message = Constants.INSERT_DUPLICATE;
                    }


                    if (validate == true)
                    {
                        param.CreateDate = helper.GetDateTimeNow();

                        param.UpdateDate = helper.GetDateTimeNow();

                        await Task.Run(() => _dbContext.MasterShippings.Add(param));

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
        public async Task<Response> UPDATE(MasterShipping param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {
                    var checkDuplication = _dbContext.MasterShippings.Where(x => x.Value == param.Value && x.Id != param.Id).FirstOrDefault();

                    if (checkDuplication != null)
                    {
                        validate = false;

                        resp.message = Constants.UPDATE_DUPLICATE;
                    }

                    if (validate == true)
                    {
                        var update = await Task.Run(() => _dbContext.MasterShippings.Where(x => x.Id == param.Id).FirstOrDefault());

                        if (update != null)
                        {
                            update.NameTh = param.NameTh;
                            update.NameEn = param.NameEn;
                            //update.CreateBy = param.CreateBy;
                            //update.CreateDate = param.CreateDate;
                            update.UpdateBy = param.UpdateBy;
                            update.UpdateDate = helper.GetDateTimeNow();
                            update.IsActive = param.IsActive;
                            update.Url = param.Url;


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
                    var delete = await Task.Run(() => _dbContext.MasterShippings.Find(id));

                    if (delete != null)
                    {
                        _dbContext.MasterShippings.Remove(delete);

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
