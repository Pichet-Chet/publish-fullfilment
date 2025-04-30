using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;
using FULFILLMENT.H4U.API.Service.transaction;
using Org.BouncyCastle.Asn1.X509;
using System.Diagnostics;
using System.Linq;

namespace FULFILLMENT.H4U.API.Service
{
    public class GoodReceiveHeaderService
    {
        HelperService helper = new HelperService();

        transactionGoodReceiveService transaction = new transactionGoodReceiveService();

        readonly DataContext _dbContext = new();

        public async Task<Response> GET_ALL(FilterModel param)
        {
            Response resp = new Response();

            List<ViewGoodReceiveHeaderModel> viewData = new List<ViewGoodReceiveHeaderModel>();

            List<GoodReceiveHeader> getData = new List<GoodReceiveHeader>();

            try
            {
                transaction.TICKER_UPDATE();

                var queryable = await Task.Run(() => _dbContext.GoodReceiveHeaders.AsQueryable());

                if (!string.IsNullOrEmpty(param.documentNo))
                {
                    queryable = queryable.Where(x => param.documentNo.Contains(x.DocumentNo)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.documentStatus))
                {
                    queryable = queryable.Where(x => param.documentStatus.Contains(x.ReceiveStatus)).AsQueryable();
                }


                if (param.vendorId != null)
                {
                    queryable = queryable.Where(x => x.VendorId == param.vendorId).AsQueryable();
                }


                if (param.dateFrom != null)
                {
                    queryable = queryable.Where(x => x.ReceiveDate >= param.dateFrom).AsQueryable();
                }

                if (param.dateTo != null)
                {
                    queryable = queryable.Where(x => x.ReceiveDate <= param.dateFrom).AsQueryable();
                }


                getData = queryable.ToList();

                if (getData.Count > 0)
                {
                    var queryableMasterVendor = _dbContext.MasterVendors.AsQueryable();

                    var queryableInboundHeader = _dbContext.InboundHeaders.AsQueryable();


                    foreach (var item in getData)
                    {
                        ViewGoodReceiveHeaderModel viewModel = new ViewGoodReceiveHeaderModel();

                        var DataInboundHeader = queryableInboundHeader.Where(x => x.Id == item.InboundHeaderId).FirstOrDefault();

                        var DataVendor = queryableMasterVendor.Where(x => x.VendorId == DataInboundHeader.VendorId).FirstOrDefault();


                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));


                        viewModel.Id = item.Id;
                        viewModel.DocumentNo = item.DocumentNo;
                        viewModel.ReceiveDate = item.ReceiveDate;
                        viewModel.ReceiveStatus = item.ReceiveStatus;
                        viewModel.InboundHeaderId = item.InboundHeaderId;
                        viewModel.CreateBy = getUserCrete;
                        viewModel.CreateDate = item.CreateDate;



                        viewModel.InboundHeaderDocumentNo = DataInboundHeader.DocumentNo;
                        viewModel.InboundHeaderStatus = DataInboundHeader.DocumentStatus;
                        viewModel.InboundHeaderCarNo = DataInboundHeader.CarNumber;
                        viewModel.InboundShippingDate = DataInboundHeader.ShippingDate;
                        viewModel.InboundArrivedDate = DataInboundHeader.ArrivedDate;


                        viewModel.VendorId = DataVendor.VendorId;
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

        public async Task<Response> GET_DETAIL(int id)
        {
            Response resp = new Response();

            ViewGoodReceiveHeaderModel viewModel = new ViewGoodReceiveHeaderModel();

            try
            {
                var item = await Task.Run(() => _dbContext.GoodReceiveHeaders.Where(x => x.Id == id).FirstOrDefault());

                if (item != null)
                {
                    var queryableMasterVendor = _dbContext.MasterVendors.AsQueryable();

                    var queryableInboundHeader = _dbContext.InboundHeaders.AsQueryable();

                    var DataInboundHeader = queryableInboundHeader.Where(x => x.Id == item.InboundHeaderId).FirstOrDefault();

                    var DataVendor = queryableMasterVendor.Where(x => x.VendorId == DataInboundHeader.VendorId).FirstOrDefault();


                    var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));


                    viewModel.Id = item.Id;
                    viewModel.DocumentNo = item.DocumentNo;
                    viewModel.ReceiveDate = item.ReceiveDate;
                    viewModel.ReceiveStatus = item.ReceiveStatus;
                    viewModel.InboundHeaderId = item.InboundHeaderId;
                    viewModel.CreateBy = getUserCrete;
                    viewModel.CreateDate = item.CreateDate;


                    viewModel.InboundHeaderDocumentNo = DataInboundHeader.DocumentNo;
                    viewModel.InboundHeaderStatus = DataInboundHeader.DocumentStatus;
                    viewModel.InboundHeaderCarNo = DataInboundHeader.CarNumber;
                    viewModel.InboundShippingDate = DataInboundHeader.ShippingDate;
                    viewModel.InboundArrivedDate = DataInboundHeader.ArrivedDate;


                    viewModel.VendorId = DataVendor.VendorId;
                    viewModel.VendorCode = DataVendor.VendorCode;
                    viewModel.VendorName = DataVendor.VendorName;
                    viewModel.TaxId = DataVendor.TaxId;
                    viewModel.ContactName = DataVendor.ContactName;
                    viewModel.ContactTel = DataVendor.ContactTel;
                    viewModel.ContactAddress = DataVendor.ContactAddress;
                    viewModel.Email = DataVendor.Email;


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

        public async Task<Response> SOFT_DELETE(GoodReceiveHeader param)
        {
            Response resp = new Response();

            GoodReceiveHeader dataModel = new GoodReceiveHeader();

            List<GoodReceiveItem> listModel = new List<GoodReceiveItem>();

            try
            {
                if (!string.IsNullOrEmpty(param.Remark))
                {
                    dataModel = await Task.Run(() => _dbContext.GoodReceiveHeaders.Where(x => x.Id == param.Id).FirstOrDefault());

                    if (dataModel != null)
                    {
                        listModel = await Task.Run(() => _dbContext.GoodReceiveItems.Where(x => x.HeaderId == param.Id).ToList());

                        dataModel.ReceiveStatus = Constants.GOOD_RECEIVE_STATUS_CANCEL;
                        dataModel.Remark = param.Remark;
                        dataModel.UpdateBy = param.UpdateBy;
                        dataModel.UpdateDate = helper.GetDateTimeNow();

                        foreach (var item in listModel)
                        {
                            item.ReceiveStatus = Constants.GOOD_RECEIVE_STATUS_CANCEL;
                            item.Remark = param.Remark;
                            item.UpdateBy = param.UpdateBy;
                            item.UpdateDate = helper.GetDateTimeNow();
                        }

                        await _dbContext.SaveChangesAsync();

                    }
                }
                else
                {
                    resp.status = false;
                    resp.message = Constants.FILL_OUT_THE_REQUIRE;
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

        public async Task<Response> GET_STATUS(FilterModel param)
        {
            Response resp = new Response();

            try
            {
                var queryable = await Task.Run(() => _dbContext.GoodReceiveStatuses.AsQueryable());

                if (param.isActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.isActive).AsQueryable();
                }

                var getData = queryable.ToList();

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


    }
}
