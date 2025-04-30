using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;
using FULFILLMENT.H4U.API.Service.transaction;
using System.Diagnostics;
using System.Linq;

namespace FULFILLMENT.H4U.API.Service
{
    public class InboundHeaderService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();

        transactionGoodReceiveService transaction = new transactionGoodReceiveService();

        public async Task<Response> GET_ALL()
        {
            Response resp = new Response();

            List<ViewInboundHeaderModel> viewData = new List<ViewInboundHeaderModel>();

            List<InboundHeader> getData = new List<InboundHeader>();

            try
            {
                getData = await Task.Run(() => _dbContext.InboundHeaders.ToList());

                if (getData.Count > 0)
                {
                    var queryableVendorMasrer = _dbContext.MasterVendors.AsQueryable();

                    foreach (var item in getData)
                    {
                        ViewInboundHeaderModel viewModel = new ViewInboundHeaderModel();

                        var DataVendor = queryableVendorMasrer.Where(x => x.VendorId == item.VendorId).FirstOrDefault();


                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));

                        viewModel.CreateBy = getUserCrete;

                        viewModel.UpdateBy = getUserCrete;

                        viewModel.Id = item.Id;
                        viewModel.DocumentNo = item.DocumentNo;
                        viewModel.DocumentStatus = item.DocumentStatus;
                        viewModel.CarNumber = item.CarNumber;
                        viewModel.Remark = item.Remark;
                        viewModel.ShippingDate = item.ShippingDate;
                        viewModel.ArrivedDate = item.ArrivedDate;
                        viewModel.ShippingValue = item.ShippingValue;


                        viewModel.CreateBy = item.CreateBy;
                        viewModel.CreateDate = item.CreateDate;

                        viewModel.UpdateBy = item.UpdateBy;
                        viewModel.UpdateDate = item.UpdateDate;

                        viewModel.VendorId = item.VendorId;
                        viewModel.VendorCode = DataVendor.VendorCode;
                        viewModel.VendorName = DataVendor.VendorName;
                        viewModel.TaxId = DataVendor.TaxId;
                        viewModel.ContactName = DataVendor.ContactName;
                        viewModel.ContactTel = DataVendor.ContactTel;
                        viewModel.ContactAddress = DataVendor.ContactAddress;
                        viewModel.Email = DataVendor.Email;


                        viewData.Add(viewModel);

                    }

                    resp.status = true;

                    resp.message = Constants.GET_DATA_SUCCESS;

                    resp.output_data = viewData.OrderByDescending(x => x.Id).ToList();
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

        public async Task<Response> GET_ALL_BY_VENDOR(int vendorId)
        {
            Response resp = new Response();

            List<MasterVendor> vendorMasrer = new List<MasterVendor>();

            List<ViewInboundHeaderModel> viewData = new List<ViewInboundHeaderModel>();

            List<InboundHeader> getData = new List<InboundHeader>();

            try
            {
                getData = await Task.Run(() => _dbContext.InboundHeaders.Where(x => x.VendorId == vendorId).ToList());

                if (getData.Count > 0)
                {
                    vendorMasrer = _dbContext.MasterVendors.ToList();

                    foreach (var item in getData)
                    {
                        ViewInboundHeaderModel viewModel = new ViewInboundHeaderModel();

                        var DataVendor = vendorMasrer.Where(x => x.VendorId == item.VendorId).FirstOrDefault();


                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));

                        viewModel.CreateBy = getUserCrete;
                        viewModel.CreateDate = item.CreateDate;

                        viewModel.UpdateBy = getUserCrete;
                        viewModel.CreateDate = item.UpdateDate;


                        viewModel.Id = item.Id;
                        viewModel.DocumentNo = item.DocumentNo;
                        viewModel.DocumentStatus = item.DocumentStatus;
                        viewModel.CarNumber = item.CarNumber;
                        viewModel.Remark = item.Remark;
                        viewModel.CreateBy = item.CreateBy;
                        viewModel.CreateDate = item.CreateDate;
                        viewModel.UpdateBy = item.UpdateBy;
                        viewModel.UpdateDate = item.UpdateDate;

                        viewModel.VendorId = item.VendorId;
                        viewModel.VendorCode = DataVendor.VendorCode;
                        viewModel.VendorName = DataVendor.VendorName;
                        viewModel.TaxId = DataVendor.TaxId;
                        viewModel.ContactName = DataVendor.ContactName;
                        viewModel.ContactTel = DataVendor.ContactTel;
                        viewModel.ContactAddress = DataVendor.ContactAddress;
                        viewModel.Email = DataVendor.Email;


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

        public async Task<Response> GET_FILTER(FilterModel param)
        {
            Response resp = new Response();

            List<MasterVendor> vendorMasrer = new List<MasterVendor>();

            List<ViewInboundHeaderModel> viewData = new List<ViewInboundHeaderModel>();

            List<InboundHeader> getData = new List<InboundHeader>();

            try
            {
                transaction.TICKER_UPDATE();

                var queryable = await Task.Run(() => _dbContext.InboundHeaders.AsQueryable());

                if (!string.IsNullOrEmpty(param.documentNo))
                {
                    queryable = queryable.Where(x => x.DocumentNo == param.documentNo).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.documentStatus))
                {
                    queryable = queryable.Where(x => x.DocumentStatus == param.documentStatus).AsQueryable();
                }

                if (param.vendorId != null)
                {
                    queryable = queryable.Where(x => x.VendorId == param.vendorId).AsQueryable();
                }



                if (param.dateFrom != null)
                {
                    queryable = queryable.Where(x => x.ShippingDate >= param.dateFrom).AsQueryable();
                }

                if (param.dateTo != null)
                {
                    queryable = queryable.Where(x => x.ShippingDate <= param.dateFrom).AsQueryable();
                }

                getData = queryable.ToList();

                if (getData.Count > 0)
                {
                    vendorMasrer = _dbContext.MasterVendors.ToList();

                    foreach (var item in getData)
                    {
                        ViewInboundHeaderModel viewModel = new ViewInboundHeaderModel();

                        var DataVendor = vendorMasrer.Where(x => x.VendorId == item.VendorId).FirstOrDefault();


                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                        item.CreateBy = getUserCrete;
                        viewModel.CreateDate = item.CreateDate;

                        item.UpdateBy = getUserUpdate;
                        viewModel.CreateDate = item.UpdateDate;

                        viewModel.ShippingDate = item.ShippingDate;
                        viewModel.ArrivedDate = item.ArrivedDate;
                        viewModel.ShippingValue = item.ShippingValue;


                        viewModel.Id = item.Id;
                        viewModel.DocumentNo = item.DocumentNo;
                        viewModel.DocumentStatus = item.DocumentStatus;
                        viewModel.CarNumber = item.CarNumber;
                        viewModel.Remark = item.Remark;
                        viewModel.CreateBy = item.CreateBy;
                        viewModel.CreateDate = item.CreateDate;


                        var getDateCreate = item.CreateDate.Value.Day;
                        var getMonthCreate = await Task.Run(() => helper.convertMonthNoToMonthName(item.CreateDate.Value.Month));
                        var getYearCreate = item.CreateDate.Value.Year;

                        viewModel.CreateDateDisplay = Convert.ToString(getDateCreate + " " + getMonthCreate + " " + getYearCreate);

                        viewModel.UpdateBy = item.UpdateBy;
                        viewModel.UpdateDate = item.UpdateDate;

                        viewModel.VendorId = item.VendorId;
                        viewModel.VendorCode = DataVendor.VendorCode;
                        viewModel.VendorName = DataVendor.VendorName;
                        viewModel.TaxId = DataVendor.TaxId;
                        viewModel.ContactName = DataVendor.ContactName;
                        viewModel.ContactTel = DataVendor.ContactTel;
                        viewModel.ContactAddress = DataVendor.ContactAddress;
                        viewModel.Email = DataVendor.Email;


                        viewData.Add(viewModel);

                    }

                    resp.status = true;

                    resp.message = Constants.GET_DATA_SUCCESS;

                    resp.output_data = viewData.OrderByDescending(x => x.Id).ToList();
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

            ViewInboundHeaderModel viewModel = new ViewInboundHeaderModel();

            List<MasterVendor> vendorMasrer = new List<MasterVendor>();

            try
            {



                var item = await Task.Run(() => _dbContext.InboundHeaders.Where(x => x.Id == id).FirstOrDefault());



                if (item != null)
                {
                    vendorMasrer = _dbContext.MasterVendors.ToList();

                    var DataVendor = vendorMasrer.Where(x => x.VendorId == item.VendorId).FirstOrDefault();
                    var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));

                    viewModel.Id = item.Id;
                    viewModel.DocumentNo = item.DocumentNo;
                    viewModel.DocumentStatus = item.DocumentStatus;
                    viewModel.CarNumber = item.CarNumber;
                    viewModel.Remark = item.Remark;
                    viewModel.CreateBy = getUserCrete;
                    viewModel.CreateDate = item.CreateDate;
                    viewModel.UpdateBy = getUserCrete;
                    viewModel.UpdateDate = item.UpdateDate;
                    viewModel.ShippingDate = item.ShippingDate;
                    viewModel.ArrivedDate = item.ArrivedDate;
                    viewModel.ShippingValue = item.ShippingValue;

                    viewModel.VendorId = item.VendorId;
                    viewModel.VendorCode = DataVendor.VendorCode;
                    viewModel.VendorName = DataVendor.VendorName;
                    viewModel.TaxId = DataVendor.TaxId;
                    viewModel.ContactName = DataVendor.ContactName;
                    viewModel.ContactTel = DataVendor.ContactTel;
                    viewModel.ContactAddress = DataVendor.ContactAddress;
                    viewModel.Email = DataVendor.Email;

                    item.CreateBy = getUserCrete;

                    item.UpdateBy = getUserCrete;

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

        public async Task<Response> UPDATE(InboundHeader param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {
                    var chkSatus = await Task.Run(() => _dbContext.InboundHeaders.Where(x => x.Id == param.Id && x.DocumentStatus == Constants.INBOUND_STATUS_SHIPPING).FirstOrDefault());

                    if (chkSatus != null)
                    {
                        var update = await Task.Run(() => _dbContext.InboundHeaders.Where(x => x.Id == param.Id).FirstOrDefault());

                        if (update != null)
                        {
                            update.CarNumber = param.CarNumber;
                            update.ShippingDate = param.ShippingDate;
                            update.Remark = param.Remark;

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
                    else
                    {
                        resp.status = false;
                        resp.message = Constants.INBOUND_UPDATE_DOCUMENT_FAILD;
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

        public async Task<Response> ARRIVED_TIME(InboundHeader param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {
                    var chkSatus = await Task.Run(() => _dbContext.InboundHeaders.Where(x => x.DocumentNo == param.DocumentNo && x.DocumentStatus == Constants.INBOUND_STATUS_SHIPPING).FirstOrDefault());

                    if (chkSatus != null)
                    {
                        var update = await Task.Run(() => _dbContext.InboundHeaders.Where(x => x.DocumentNo == param.DocumentNo).FirstOrDefault());

                        if (update != null)
                        {
                            update.ArrivedDate = helper.GetDateTimeNow();
                            update.DocumentStatus = Constants.INBOUND_STATUS_ARRIVED;

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
                    else
                    {
                        resp.status = false;
                        resp.message = Constants.INBOUND_UPDATE_DOCUMENT_FAILD;
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

        public async Task<Response> SOFT_DELETE(InboundHeader param)
        {
            Response resp = new Response();

            InboundHeader dataHeader = new InboundHeader();

            List<InboundItem> dataItem = new List<InboundItem>();

            try
            {
                dataHeader = await Task.Run(() => _dbContext.InboundHeaders.Where(x => x.Id == param.Id && x.DocumentStatus == Constants.INBOUND_STATUS_SHIPPING).FirstOrDefault());

                dataItem = await Task.Run(() => _dbContext.InboundItems.Where(x => x.HeaderId == param.Id).ToList());

                if (dataHeader != null && dataItem.Count > 0)
                {

                    if (string.IsNullOrEmpty(param.Remark))
                    {
                        resp.status = false;
                        resp.message = Constants.FILL_OUT_THE_REQUIRE;

                        return resp;
                    }


                    dataHeader.DocumentStatus = Constants.INBOUND_STATUS_CANCEL;
                    dataHeader.Remark = param.Remark;

                    foreach (var item in dataItem)
                    {
                        item.ItemStatus = Constants.INBOUND_STATUS_CANCEL;
                    }

                    await _dbContext.SaveChangesAsync();

                    resp.status = true;
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
