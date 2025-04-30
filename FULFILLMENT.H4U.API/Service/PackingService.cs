using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;
using System.Diagnostics;

namespace FULFILLMENT.H4U.API.Service
{
    public class PackingService
    {
        HelperService helper = new HelperService();

        readonly DataContext _dbContext = new();

        public async Task<Response> GET_ALL(FilterModel param)
        {
            Response resp = new Response();

            List<ViewPackingModel> viewData = new List<ViewPackingModel>();

            List<Packing> getData = new List<Packing>();

            try
            {
                var queryable = await Task.Run(() => _dbContext.Packings.AsQueryable());

                if (!string.IsNullOrEmpty(param.documentNo))
                {
                    queryable = queryable.Where(x => param.documentNo.Contains(x.DocumentNo)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.documentStatus))
                {
                    queryable = queryable.Where(x => param.documentStatus.Contains(x.DocumentStatus)).AsQueryable();
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

                    var queryableMasterBox = _dbContext.MasterBoxes.AsQueryable();
                    var queryableMasterPo = _dbContext.PurchaseOrderHeaders.AsQueryable();
                    var queryableMasterPoItem = _dbContext.PurchaseOrderItems.AsQueryable();
                    var queryableMasterPicklist = _dbContext.PicklistHeaders.AsQueryable();
                    var queryableMasterPicklistItem = _dbContext.PicklistItems.AsQueryable();

                    foreach (var item in getData)
                    {
                        ViewPackingModel viewModel = new ViewPackingModel();

                        var DataVendor = queryableMasterVendor.Where(x => x.VendorId == item.VendorId).FirstOrDefault();
                        var DataMasterBox = queryableMasterBox.Where(x => x.Id == item.BoxId).FirstOrDefault();

                        var DataPurchaseOrder = queryableMasterPo.Where(x => x.Id == item.PurchaseOrderHeaderId).FirstOrDefault();
                        var DataPurchaseOrderItem = queryableMasterPoItem.Where(x => x.HeaderId == DataPurchaseOrder.Id).ToList();

                        var DataPicklistHeader = queryableMasterPicklist.Where(x => x.Id == item.PicklistHeaderId).FirstOrDefault();
                        var DataPicklistItem = queryableMasterPicklistItem.Where(x => x.HeaderId == DataPicklistHeader.Id).ToList();


                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                        viewModel.Id = item.Id;
                        viewModel.DocumentNo = item.DocumentNo;
                        viewModel.DocumentStatus = item.DocumentStatus;
                        viewModel.Remark = item.Remark;
                        viewModel.BinId = item.BinId;
                        viewModel.ShippingPrice = item.ShippingPrice;
                        viewModel.BoxId = item.BoxId;
                        viewModel.masterBox = DataMasterBox;
                        viewModel.TrackingNumber = DataPurchaseOrder.TrackingNumber;

                        viewModel.purchaseOrderHeader = DataPurchaseOrder;

                        foreach (var poItem in DataPurchaseOrderItem)
                        {
                            var getProductName = _dbContext.MasterProducts.Where(x => x.ProductId == poItem.ProductId).FirstOrDefault();

                            ViewPurchaseOrderItemModel viewPurchaseOrderItemModel = new ViewPurchaseOrderItemModel();

                            viewPurchaseOrderItemModel.Amount = poItem.Amount;
                            viewPurchaseOrderItemModel.productName = getProductName.ProductName;

                            viewModel.purchaseOrderItems.Add(viewPurchaseOrderItemModel);
                        }

                        viewModel.picklistHeader = DataPicklistHeader;

                        foreach (var picklistItem in DataPicklistItem)
                        {
                            viewModel.picklistItems.Add(picklistItem);
                        }

                        viewModel.VendorId = item.VendorId;
                        viewModel.VendorCode = DataVendor.VendorCode;
                        viewModel.VendorName = DataVendor.VendorName;
                        viewModel.TaxId = DataVendor.TaxId;
                        viewModel.ContactName = DataVendor.ContactName;
                        viewModel.ContactTel = DataVendor.ContactTel;
                        viewModel.ContactAddress = DataVendor.ContactAddress;
                        viewModel.Email = DataVendor.Email;

                        viewModel.CreateBy = getUserCrete;
                        viewModel.CreateDate = item.CreateDate;
                        viewModel.UpdateBy = getUserUpdate;
                        viewModel.UpdateDate = item.UpdateDate;

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

            ViewPackingModel viewModel = new ViewPackingModel();

            Packing item = new Packing();

            try
            {
                var queryable = await Task.Run(() => _dbContext.Packings.Where(x => x.Id == id).AsQueryable());

                item = queryable.FirstOrDefault();

                if (item != null)
                {
                    var queryableMasterVendor = _dbContext.MasterVendors.AsQueryable();

                    var queryableMasterBox = _dbContext.MasterBoxes.AsQueryable();
                    var queryableMasterPo = _dbContext.PurchaseOrderHeaders.AsQueryable();
                    var queryableMasterPoItem = _dbContext.PurchaseOrderItems.AsQueryable();
                    var queryableMasterPicklist = _dbContext.PicklistHeaders.AsQueryable();
                    var queryableMasterPicklistItem = _dbContext.PicklistItems.AsQueryable();

                    var DataVendor = queryableMasterVendor.Where(x => x.VendorId == item.VendorId).FirstOrDefault();
                    var DataMasterBox = queryableMasterBox.Where(x => x.Id == item.BoxId).FirstOrDefault();
                    var DataPurchaseOrder = queryableMasterPo.Where(x => x.Id == item.PurchaseOrderHeaderId).FirstOrDefault();
                    var DataPurchaseOrderItem = queryableMasterPoItem.Where(x => x.HeaderId == DataPurchaseOrder.Id).ToList();
                    var DataPicklistHeader = queryableMasterPicklist.Where(x => x.Id == item.PicklistHeaderId).FirstOrDefault();
                    var DataPicklistItem = queryableMasterPicklistItem.Where(x => x.HeaderId == DataPicklistHeader.Id).ToList();


                    var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                    var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                    viewModel.Id = item.Id;
                    viewModel.DocumentNo = item.DocumentNo;
                    viewModel.DocumentStatus = item.DocumentStatus;
                    viewModel.Remark = item.Remark;
                    viewModel.BinId = item.BinId;
                    viewModel.ShippingPrice = item.ShippingPrice;
                    viewModel.TrackingNumber = DataPurchaseOrder.TrackingNumber;

                    viewModel.masterBox = DataMasterBox;


                    viewModel.purchaseOrderHeader = DataPurchaseOrder;

                    foreach (var poItem in DataPurchaseOrderItem)
                    {
                        var getProductName = _dbContext.MasterProducts.Where(x => x.ProductId == poItem.ProductId).FirstOrDefault();

                        ViewPurchaseOrderItemModel viewPurchaseOrderItemModel = new ViewPurchaseOrderItemModel();

                        viewPurchaseOrderItemModel.Amount = poItem.Amount;
                        viewPurchaseOrderItemModel.productName = getProductName.ProductName;
                        viewPurchaseOrderItemModel.productSku = getProductName.ProductSku;
                        viewPurchaseOrderItemModel.productDescription = getProductName.ProductDescription;
                        viewPurchaseOrderItemModel.productColor = getProductName.ProductColor;


                        viewModel.purchaseOrderItems.Add(viewPurchaseOrderItemModel);
                    }

                    viewModel.picklistHeader = DataPicklistHeader;

                    foreach (var picklistItem in DataPicklistItem)
                    {
                        viewModel.picklistItems.Add(picklistItem);
                    }

                    viewModel.VendorId = item.VendorId;
                    viewModel.VendorCode = DataVendor.VendorCode;
                    viewModel.VendorName = DataVendor.VendorName;
                    viewModel.TaxId = DataVendor.TaxId;
                    viewModel.ContactName = DataVendor.ContactName;
                    viewModel.ContactTel = DataVendor.ContactTel;
                    viewModel.ContactAddress = DataVendor.ContactAddress;
                    viewModel.Email = DataVendor.Email;

                    viewModel.CreateBy = getUserCrete;
                    viewModel.CreateDate = item.CreateDate;
                    viewModel.UpdateBy = getUserUpdate;
                    viewModel.UpdateDate = item.UpdateDate;

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
