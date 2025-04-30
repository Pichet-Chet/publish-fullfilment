using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;
using System.Diagnostics;

namespace FULFILLMENT.H4U.API.Service
{
    public class PicklistHeaderService
    {
        HelperService helper = new HelperService();

        readonly DataContext _dbContext = new();
        public async Task<Response> GET_ALL(FilterModel param)
        {
            Response resp = new Response();

            List<ViewPicklistHeaderModel> viewData = new List<ViewPicklistHeaderModel>();

            List<PicklistHeader> getData = new List<PicklistHeader>();

            try
            {
                var queryable = await Task.Run(() => _dbContext.PicklistHeaders.AsQueryable());

                if (!string.IsNullOrEmpty(param.documentNo))
                {
                    queryable = queryable.Where(x => param.documentNo.Contains(x.DocumentNo)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.documentStatus))
                {
                    queryable = queryable.Where(x => param.documentStatus.Contains(x.DocumentStatus)).AsQueryable();
                }

                if (param.vendorId != null)
                {
                    queryable = queryable.Where(x => x.VendorId == param.vendorId).AsQueryable();
                }

                if (param.dateFrom != null)
                {
                    queryable = queryable.Where(x => x.CreateDate >= param.dateFrom).AsQueryable();
                }

                if (param.dateTo != null)
                {
                    queryable = queryable.Where(x => x.CreateDate <= param.dateFrom).AsQueryable();
                }

                getData = queryable.ToList();

                if (getData.Count > 0)
                {
                    var queryableMasterVendor = _dbContext.MasterVendors.AsQueryable();
                    var queryableMasterPo = _dbContext.PurchaseOrderHeaders.AsQueryable();

                    foreach (var item in getData)
                    {
                        ViewPicklistHeaderModel viewModel = new ViewPicklistHeaderModel();

                        var DataVendor = queryableMasterVendor.Where(x => x.VendorId == item.VendorId).FirstOrDefault();
                        var DataPurchaseOrder = queryableMasterPo.Where(x => x.Id == item.PurchaseOrderHeaderId).FirstOrDefault();

                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                        viewModel.Id = item.Id;
                        viewModel.BinId = item.BinId;
                        viewModel.DocumentNo = item.DocumentNo;
                        viewModel.PurchaseOrderHeaderId = item.PurchaseOrderHeaderId;
                        viewModel.PurchaseOrderDocument = DataPurchaseOrder.DocumentNo;
                        viewModel.DocumentStatus = item.DocumentStatus;
                        viewModel.FromAddress = DataPurchaseOrder.FromAddress;
                        viewModel.FromEmail = DataPurchaseOrder.FromEmail;
                        viewModel.FromName = DataPurchaseOrder.FromName;
                        viewModel.FromTel = DataPurchaseOrder.FromTel;
                        viewModel.ToAddress = DataPurchaseOrder.ToAddress;
                        viewModel.ToEmail = DataPurchaseOrder.ToEmail;
                        viewModel.ToName = DataPurchaseOrder.ToName;
                        viewModel.ToTel = DataPurchaseOrder.ToTel;


                        viewModel.CreateBy = getUserCrete;
                        viewModel.CreateDate = item.CreateDate;
                        viewModel.UpdateBy = getUserUpdate;
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

        public async Task<Response> GET_DETAIL(int id)
        {
            Response resp = new Response();

            ViewPicklistHeaderModel viewModel = new ViewPicklistHeaderModel();

            PicklistHeader item = new PicklistHeader();

            try
            {
                var queryable = await Task.Run(() => _dbContext.PicklistHeaders.Where(x => x.Id == id).AsQueryable());

                item = queryable.FirstOrDefault();

                if (item != null)
                {
                    var queryableMasterVendor = _dbContext.MasterVendors.AsQueryable();
                    var queryableMasterPo = _dbContext.PurchaseOrderHeaders.AsQueryable();

                    var DataVendor = queryableMasterVendor.Where(x => x.VendorId == item.VendorId).FirstOrDefault();
                    var DataPurchaseOrder = queryableMasterPo.Where(x => x.Id == item.Id).FirstOrDefault();

                    var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                    var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                    viewModel.Id = item.Id;
                    viewModel.DocumentNo = item.DocumentNo;
                    viewModel.DocumentStatus = item.DocumentStatus;
                    viewModel.FromAddress = DataPurchaseOrder.FromAddress;
                    viewModel.FromEmail = DataPurchaseOrder.FromEmail;
                    viewModel.FromName = DataPurchaseOrder.FromName;
                    viewModel.FromTel = DataPurchaseOrder.FromTel;
                    viewModel.ToAddress = DataPurchaseOrder.ToAddress;
                    viewModel.ToEmail = DataPurchaseOrder.ToEmail;
                    viewModel.ToName = DataPurchaseOrder.ToName;
                    viewModel.ToTel = DataPurchaseOrder.ToTel;


                    viewModel.CreateBy = getUserCrete;
                    viewModel.CreateDate = item.CreateDate;
                    viewModel.UpdateBy = getUserUpdate;
                    viewModel.UpdateDate = item.UpdateDate;

                    viewModel.VendorId = item.VendorId;
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

    }
}
