using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;
using System.Diagnostics;

namespace FULFILLMENT.H4U.API.Service
{
    public class SysUserService
    {
        HelperService helper = new HelperService();

        readonly DataContext _dbContext = new();
        public async Task<Response> GET_ALL()
        {
            Response resp = new Response();

            try
            {
                var getData = await Task.Run(() => _dbContext.MasterBins.ToList());

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
                var getData = await Task.Run(() => _dbContext.MasterBins.Where(x => x.BinId == id).FirstOrDefault());

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

            List<MasterBin> listData = new List<MasterBin>();

            try
            {
                if (!string.IsNullOrEmpty(textSearch))
                {
                    listData = await Task.Run(() => _dbContext.MasterBins.Where(x =>
                                (x.BinId + x.BinStatus + x.BinNumber + x.BinName + x.BinDescription + x.BinType + x.CreateBy + x.UpdateBy)
                                .Contains(textSearch)).ToList());
                }
                else
                {
                    listData = await Task.Run(() => _dbContext.MasterBins.ToList());
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
        public async Task<Response> INSERT(MasterBin param)
        {
            Response resp = new Response();

            try
            {
                using (_dbContext)
                {
                    var checkDuplication = _dbContext.MasterBins.Where(x => x.BinNumber == param.BinNumber || x.BinName == param.BinName).FirstOrDefault();

                    if (checkDuplication == null)
                    {
                        param.CreateDate = helper.GetDateTimeNow();

                        param.UpdateDate = helper.GetDateTimeNow();

                        await Task.Run(() => _dbContext.MasterBins.Add(param));

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
        public async Task<Response> UPDATE(MasterBin param)
        {
            Response resp = new Response();

            try
            {
                using (_dbContext)
                {
                    var update = await Task.Run(() => _dbContext.MasterBins.Where(x => x.BinId == param.BinId).FirstOrDefault());

                    if (update != null)
                    {
                        update.BinNumber = param.BinNumber;
                        update.BinName = param.BinName;
                        update.BinDescription = param.BinDescription;
                        update.BinType = param.BinType;
                        update.BinStatus = param.BinStatus;

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
                    var delete = await Task.Run(() => _dbContext.MasterBins.Find(id));

                    if (delete != null)
                    {
                        _dbContext.MasterBins.Remove(delete);
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
