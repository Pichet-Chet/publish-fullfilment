using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;
using System.Diagnostics;

namespace FULFILLMENT.H4U.API.Service
{
    public class PurchaseOrderHeaderService
    {
        HelperService helper = new HelperService();

        readonly DataContext _dbContext = new();

        public async Task<Response> GET_ALL(FilterModel param)
        {
            Response resp = new Response();

            List<ViewPurchaseOrderHeaderModel> viewData = new List<ViewPurchaseOrderHeaderModel>();

            List<PurchaseOrderHeader> getData = new List<PurchaseOrderHeader>();

            try
            {
                var queryable = await Task.Run(() => _dbContext.PurchaseOrderHeaders.AsQueryable());

                if (!string.IsNullOrEmpty(param.documentNo))
                {
                    queryable = queryable.Where(x => param.documentNo.Contains(x.DocumentNo)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.documentStatus))
                {
                    queryable = queryable.Where(x => param.documentStatus.Contains(x.DocumentStatus)).AsQueryable();
                }
                if (!string.IsNullOrEmpty(param.shipping))
                {
                    queryable = queryable.Where(x => param.shipping.Contains(x.ShippingValue)).AsQueryable();
                }
                if (!string.IsNullOrEmpty(param.platform))
                {
                    queryable = queryable.Where(x => param.platform.Contains(x.PlatformValue)).AsQueryable();
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

                var queryableBoxMaster = _dbContext.MasterBoxes.AsQueryable();

                if (getData.Count > 0)
                {
                    var queryableMasterVendor = _dbContext.MasterVendors.AsQueryable();
                    var queryablePoStatus = _dbContext.PurchaseOrderStatuses.AsQueryable();

                    foreach (var item in getData)
                    {
                        ViewPurchaseOrderHeaderModel viewModel = new ViewPurchaseOrderHeaderModel();

                        var DataVendor = queryableMasterVendor.Where(x => x.VendorId == item.VendorId).FirstOrDefault();

                        var DataStatusDocument = queryablePoStatus.Where(x => x.Value == item.DocumentStatus).FirstOrDefault();

                        var findOrderPacking = await Task.Run(() => _dbContext.Packings.Where(x => x.PurchaseOrderHeaderId == item.Id).FirstOrDefault());
                        //var findOrderShipping = await Task.Run(() => _dbContext.Packings.Where(x => x.PurchaseOrderHeaderId == item.Id).FirstOrDefault());


                        if (findOrderPacking != null)
                        {
                            var findPackingSuccess = queryableBoxMaster.Where(x => x.Id == findOrderPacking.BoxId).FirstOrDefault();

                            var findBoxMaster = queryableBoxMaster.Where(x => x.Id == findOrderPacking.BoxId).FirstOrDefault();

                            if (findBoxMaster != null)
                            {
                                viewModel.BoxName = findBoxMaster.Name;
                            }

                            viewModel.ShippingDate = findOrderPacking.DocumentStatus == "SHIPPED" ? findOrderPacking.UpdateDate : null;
                            viewModel.BoxId = findOrderPacking.BoxId;
                            viewModel.ShippingPrice = findOrderPacking.ShippingPrice;
                        }

                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                        viewModel.Id = item.Id;
                        viewModel.DocumentNo = item.DocumentNo;
                        viewModel.DocumentStatus = item.DocumentStatus;
                        viewModel.VendorFlagAddress = item.VendorFlagAddress;
                        viewModel.FromAddress = item.FromAddress;
                        viewModel.FromEmail = item.FromEmail;
                        viewModel.FromName = item.FromName;
                        viewModel.FromTel = item.FromTel;
                        viewModel.ToAddress = item.ToAddress;
                        viewModel.ToEmail = item.ToEmail;
                        viewModel.ToName = item.ToName;
                        viewModel.ToTel = item.ToTel;
                        viewModel.ShippingValue = item.ShippingValue;
                        viewModel.PlatformValue = item.PlatformValue;
                        viewModel.TrackingNumber = item.TrackingNumber;


                        //viewModel.color = DataStatusDocument.ClassColor;


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

                    resp.output_data = viewData.OrderByDescending(x => x.CreateDate);
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

            PurchaseOrderHeader item = new PurchaseOrderHeader();

            try
            {
                var queryable = await Task.Run(() => _dbContext.PurchaseOrderHeaders.Where(x => x.Id == id).AsQueryable());


                item = queryable.FirstOrDefault();

                if (item != null)
                {
                    var queryableMasterVendor = _dbContext.MasterVendors.AsQueryable();
                    var queryableMasterBox = _dbContext.MasterBoxes.AsQueryable();

                    ViewPurchaseOrderHeaderModel viewModel = new ViewPurchaseOrderHeaderModel();

                    var DataVendor = queryableMasterVendor.Where(x => x.VendorId == item.VendorId).FirstOrDefault();

                    var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                    var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                    viewModel.Id = item.Id;
                    viewModel.DocumentNo = item.DocumentNo;
                    viewModel.DocumentStatus = item.DocumentStatus;
                    viewModel.VendorFlagAddress = item.VendorFlagAddress;
                    viewModel.FromAddress = item.FromAddress;
                    viewModel.FromEmail = item.FromEmail;
                    viewModel.FromName = item.FromName;
                    viewModel.FromTel = item.FromTel;
                    viewModel.ToAddress = item.ToAddress;
                    viewModel.ToEmail = item.ToEmail;
                    viewModel.ToName = item.ToName;
                    viewModel.ToTel = item.ToTel;
                    viewModel.ShippingValue = item.ShippingValue;
                    viewModel.PlatformValue = item.PlatformValue;


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
