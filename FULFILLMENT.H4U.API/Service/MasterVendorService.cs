using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Reponse;
using System.Diagnostics;
using FULFILLMENT.H4U.API.Service.Helper;
using System.ComponentModel.DataAnnotations;
using FULFILLMENT.H4U.API.Model.Custom;

namespace FULFILLMENT.H4U.API.Service
{
    public class MasterVendorService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();

        public async Task<Response> GET_ALL()
        {
            Response resp = new Response();

            try
            {
                var getData = await Task.Run(() => _dbContext.MasterVendors.ToList());

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

            try
            {
                var queryable = await Task.Run(() => _dbContext.MasterVendors.AsQueryable());

                if (!string.IsNullOrEmpty(param.name))
                {
                    queryable = queryable.Where(x => param.name.Contains(x.VendorName)).AsQueryable();
                }

                if (param.vendorId != null)
                {
                    queryable = queryable.Where(x => x.VendorId == param.vendorId).AsQueryable();
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
        public async Task<Response> GET_BALANCE_MOVEMENT(FilterModel param)
        {
            Response resp = new Response();

            List<ViewVendorBalanceLogModel> viewData = new List<ViewVendorBalanceLogModel>();

            try
            {
                var queryable = await Task.Run(() => _dbContext.VendorBalanceLogs.AsQueryable());

                if (param.vendorId != null)
                {
                    queryable = queryable.Where(x => x.VendorId == param.vendorId).AsQueryable();
                }

                var getData = queryable.ToList();

                if (getData.Count > 0)
                {
                    var queryableVendorMaster = _dbContext.MasterVendors.AsQueryable();

                    foreach (var item in getData)
                    {
                        ViewVendorBalanceLogModel viewModel = new ViewVendorBalanceLogModel();

                        var DataVendor = queryableVendorMaster.Where(x => x.VendorId == item.VendorId).FirstOrDefault();

                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));

                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                        viewModel.Id = item.Id;
                        viewModel.Type = item.Type;
                        viewModel.AmountOld = item.AmountOld;
                        viewModel.AmountChange = item.AmountChange;
                        viewModel.CreateBy = getUserCrete;
                        viewModel.CreateDate = item.CreateDate;
                        viewModel.UpdateBy = getUserCrete;
                        viewModel.UpdateDate = item.UpdateDate;

                        viewModel.VendorId = item.VendorId;
                        viewModel.VendorCode = DataVendor.VendorCode;
                        viewModel.VendorName = DataVendor.VendorName;
                        viewModel.TaxId = DataVendor.TaxId;
                        viewModel.ContactName = DataVendor.ContactName;
                        viewModel.ContactTel = DataVendor.ContactTel;
                        viewModel.ContactAddress = DataVendor.ContactAddress;
                        viewModel.Email = DataVendor.Email;
                        viewModel.LineId = DataVendor.LineId;
                        viewModel.Website = DataVendor.Website;

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
                var getData = await Task.Run(() => _dbContext.MasterVendors.Where(x => x.IsActive == true).ToList());

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
                var getData = await Task.Run(() => _dbContext.MasterVendors.Where(x => x.VendorId == id).FirstOrDefault());

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

            List<MasterVendor> listData = new List<MasterVendor>();

            try
            {
                if (!string.IsNullOrEmpty(textSearch))
                {
                    listData = await Task.Run(() => _dbContext.MasterVendors.Where(x =>
                    (x.VendorCode + x.VendorName + x.ContactAddress + x.ContactName + x.ContactTel + x.TaxId + x.CreateBy + x.UpdateBy)
                    .Contains(textSearch)).ToList());
                }
                else
                {
                    listData = await Task.Run(() => _dbContext.MasterVendors.ToList());
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
        public async Task<Response> INSERT(MasterVendor param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {
                    var checkDuplication = _dbContext.MasterVendors.Where(x => x.VendorCode == param.VendorCode).FirstOrDefault();
                    var checkDuplication1 = _dbContext.MasterVendors.Where(x => x.TaxId == param.TaxId && x.VendorName == param.VendorName).FirstOrDefault();

                    if (checkDuplication != null)
                    {
                        validate = false;

                        resp.message = Constants.INSERT_DUPLICATE;
                    }

                    if (checkDuplication1 != null)
                    {
                        validate = false;

                        resp.message = Constants.INSERT_DUPLICATE;
                    }


                    if (validate == true)
                    {
                        param.CreateDate = helper.GetDateTimeNow();

                        param.UpdateDate = helper.GetDateTimeNow();

                        await Task.Run(() => _dbContext.MasterVendors.Add(param));

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
        public async Task<Response> UPDATE(MasterVendor param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {

                    var checkDuplication = _dbContext.MasterVendors.Where(x => x.VendorCode == param.VendorCode && x.VendorId != param.VendorId).FirstOrDefault();
                    var checkDuplication1 = _dbContext.MasterVendors.Where(x => x.TaxId == param.TaxId && x.VendorName == param.VendorName && x.VendorId != param.VendorId).FirstOrDefault();

                    if (checkDuplication != null)
                    {
                        validate = false;

                        resp.message = Constants.UPDATE_DUPLICATE;
                    }

                    if (checkDuplication1 != null)
                    {
                        validate = false;

                        resp.message = Constants.UPDATE_DUPLICATE;
                    }


                    if (validate == true)
                    {
                        var update = await Task.Run(() => _dbContext.MasterVendors.Where(x => x.VendorId == param.VendorId).FirstOrDefault());

                        if (update != null)
                        {
                            update.VendorName = param.VendorName == null ? update.VendorName : param.VendorName;
                            update.TaxId = param.TaxId == null ? update.TaxId : param.TaxId;
                            update.ContactName = param.ContactName == null ? update.ContactName : param.ContactName;
                            update.ContactTel = param.ContactTel == null ? update.ContactTel : param.ContactTel;
                            update.ContactAddress = param.ContactAddress == null ? update.ContactAddress : param.ContactAddress;
                            update.LineId = param.LineId == null ? update.LineId : param.LineId;
                            update.Website = param.Website == null ? update.Website : param.Website;
                            update.SerectKey = param.SerectKey == null ? update.SerectKey : param.SerectKey;
                            //update.CreateBy = param.CreateBy;
                            //update.CreateDate = param.CreateDate;
                            update.UpdateBy = param.UpdateBy == null ? update.UpdateBy : param.UpdateBy;
                            update.UpdateDate = helper.GetDateTimeNow();
                            update.IsActive = param.Balance == null ? param.IsActive : update.IsActive;

                            if (param.Balance != null)
                            {
                                VendorBalanceLog vendorBalanceLog = new VendorBalanceLog();

                                vendorBalanceLog.VendorId = param.VendorId;
                                vendorBalanceLog.Type = param.Balance >= update.Balance ? "ADD" : "DELETE";
                                vendorBalanceLog.CreateBy = param.CreateBy == null ? update.CreateBy : param.CreateBy;
                                vendorBalanceLog.UpdateBy = param.UpdateBy == null ? update.UpdateBy : param.UpdateBy;
                                vendorBalanceLog.AmountOld = update.Balance;
                                vendorBalanceLog.AmountChange = param.Balance;
                                vendorBalanceLog.CreateDate = helper.GetDateTimeNow();
                                vendorBalanceLog.UpdateDate = helper.GetDateTimeNow();

                                _dbContext.VendorBalanceLogs.Add(vendorBalanceLog);

                                update.Balance = param.Balance == null ? update.Balance : param.Balance;

                            }

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
                    var delete = await Task.Run(() => _dbContext.MasterVendors.Find(id));

                    if (delete != null)
                    {
                        _dbContext.MasterVendors.Remove(delete);

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
