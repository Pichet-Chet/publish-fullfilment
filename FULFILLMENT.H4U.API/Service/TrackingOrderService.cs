using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;
using System.Diagnostics;

namespace FULFILLMENT.H4U.API.Service
{
    public class TrackingOrderService
    {
        HelperService helper = new HelperService();

        readonly DataContext _dbContext = new();
        public async Task<Response> GET_ALL(FilterModel param)
        {
            Response resp = new Response();

            ViewTrackingOrderModel viewModel = new ViewTrackingOrderModel();

            try
            {
                var queryable = await Task.Run(() => _dbContext.PurchaseOrderHeaders.Where(x => x.TrackingNumber == param.purchaseOrderNo).FirstOrDefault());

                if (queryable != null)
                {
                    var queryableMasterVendor = _dbContext.MasterVendors.AsQueryable();
                    var queryablePicklist = _dbContext.PicklistHeaders.AsQueryable();
                    var queryablePacking = _dbContext.Packings.AsQueryable();
                    var queryableMasterShipping= _dbContext.MasterShippings.AsQueryable();


                    var DataVendor = queryableMasterVendor.Where(x => x.VendorId == queryable.VendorId).FirstOrDefault();

                    viewModel.VendorCode = DataVendor.VendorCode;
                    viewModel.VendorName = DataVendor.VendorName;
                    viewModel.ContactName = DataVendor.ContactName;
                    viewModel.ContactTel = DataVendor.ContactTel;
                    viewModel.ContactAddress = DataVendor.ContactAddress;
                    viewModel.Email = DataVendor.Email;

                    viewModel.PurchaseOrderNo = queryable.DocumentNo;
                    viewModel.PurchaseOrderBy = helper.GetUserId(Convert.ToInt32(queryable.CreateBy));
                    viewModel.PurchaseOrderDate = queryable.CreateDate;
                    viewModel.PurchaseOrderShipping = queryable.ShippingValue;

                    var DataPicklist = queryablePicklist.Where(x => x.PurchaseOrderHeaderId == queryable.Id).FirstOrDefault();

                    if (DataPicklist != null)
                    {
                        viewModel.PicklistNo = DataPicklist.DocumentNo;
                        viewModel.PicklistBy = helper.GetUserId(Convert.ToInt32(DataPicklist.CreateBy));
                        viewModel.PicklistDate = DataPicklist.CreateDate;
                    }

                    var DataPacking = queryablePacking.Where(x => x.PurchaseOrderHeaderId == queryable.Id).FirstOrDefault();


                    if (DataPacking != null)
                    {
                        viewModel.PackingNo = DataPacking.DocumentNo;
                        viewModel.PackingBy = helper.GetUserId(Convert.ToInt32(DataPacking.CreateBy));
                        viewModel.PackingDate = DataPacking.CreateDate;
                    }

                    var DataShipped = queryablePacking.Where(x => x.PurchaseOrderHeaderId == queryable.Id && !string.IsNullOrEmpty(x.TrackingNumber)).FirstOrDefault();

                    if (DataShipped != null)
                    {
                        viewModel.ShippedBy = helper.GetUserId(Convert.ToInt32(DataShipped.CreateBy));
                        viewModel.ShippedDate = DataShipped.UpdateDate;
                        viewModel.ShippedTracking = DataShipped.TrackingNumber;
                        viewModel.ShippedWebsite = queryableMasterShipping.Where(x => x.Value == queryable.ShippingValue).FirstOrDefault().Url;
                    }

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
