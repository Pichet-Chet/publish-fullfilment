using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Reponse;
using System.Diagnostics;

namespace FULFILLMENT.H4U.API.Service
{
    public class SysApplicationService
    {
        readonly DataContext _dbContext = new();
        public async Task<Response> GET_ALL()
        {
            Response resp = new Response();

            try
            {
                var getData = await Task.Run(() => _dbContext.SysApplications.ToList());

                if (getData.Count > 0)
                {
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
                var getData = await Task.Run(() => _dbContext.SysApplications.Where(x => x.ApplicationId == id).FirstOrDefault());

                if (getData != null)
                {
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

            List<SysApplication> listData = new List<SysApplication>();

            try
            {
                if (!string.IsNullOrEmpty(textSearch))
                {
                    listData = await Task.Run(() => _dbContext.SysApplications.Where(x =>
                                (x.ApplicationId + x.ApplicationName + x.ApplicationDescription)
                                .Contains(textSearch)).ToList());
                }
                else
                {
                    listData = await Task.Run(() => _dbContext.SysApplications.ToList());
                }

                if (listData.Count > 0)
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
        public async Task<Response> INSERT(SysApplication param)
        {
            Response resp = new Response();

            try
            {
                using (_dbContext)
                {
                    var checkDuplication = _dbContext.SysApplications.Where(x => x.ApplicationId == param.ApplicationId || x.ApplicationName == param.ApplicationName).FirstOrDefault();

                    if (checkDuplication == null)
                    {

                        await Task.Run(() => _dbContext.SysApplications.Add(param));

                        _dbContext.SaveChanges();

                        resp.status = true;

                        resp.message = Constants.INSERT_SUCCESS;
                    }
                    else
                    {
                        resp.status = false;

                        resp.message = Constants.INSERT_DUPLICATE;
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
        public async Task<Response> UPDATE(SysApplication param)
        {
            Response resp = new Response();

            try
            {
                using (_dbContext)
                {
                    var update = await Task.Run(() => _dbContext.SysApplications.Where(x => x.ApplicationId == param.ApplicationId).FirstOrDefault());

                    if (update != null)
                    {
                        update.ApplicationDescription = param.ApplicationDescription;

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
                    var delete = await Task.Run(() => _dbContext.SysApplications.Find(id));

                    if (delete != null)
                    {
                        _dbContext.SysApplications.Remove(delete);
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
