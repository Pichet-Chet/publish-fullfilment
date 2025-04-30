using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;
using System.Diagnostics;

namespace FULFILLMENT.H4U.API.Service
{
    public class PurchaseOrderItemService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();

        public async Task<Response> GET_ALL_BY_HEADER(int headerId)
        {
            Response resp = new Response();

            List<ViewPurchaseOrderItemModel> viewData = new List<ViewPurchaseOrderItemModel>();

            List<PurchaseOrderItem> getData = new List<PurchaseOrderItem>();

            try
            {
                var queryable = await Task.Run(() => _dbContext.PurchaseOrderItems.Where(x => x.HeaderId == headerId).AsQueryable());

                getData = queryable.ToList();

                if (getData.Count > 0)
                {
                    var queryableMasterVendor = _dbContext.MasterVendors.AsQueryable();
                    var queryableProductMaster = _dbContext.MasterProducts.AsQueryable();
                    var queryablePoHeader = _dbContext.PurchaseOrderHeaders.AsQueryable();

                    foreach (var item in getData)
                    {
                        ViewPurchaseOrderItemModel viewModel = new ViewPurchaseOrderItemModel();

                        var dataProductMaster = queryableProductMaster.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                        var dataPoHeader = queryablePoHeader.Where(x => x.Id == item.HeaderId).FirstOrDefault();


                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));


                        viewModel.Id = item.Id;
                        viewModel.CreateBy = getUserCrete;
                        viewModel.CreateDate = item.CreateDate;
                        viewModel.UpdateBy = getUserUpdate;
                        viewModel.UpdateDate = item.UpdateDate;
                        viewModel.StatusValue = item.StatusValue;
                        viewModel.Amount = item.Amount;


                        // Mapping Header 

                        viewModel.headerDocumentNo = dataPoHeader.DocumentNo;
                        viewModel.HeaderId = item.HeaderId;
                        viewModel.VendorFlagAddress = dataPoHeader.VendorFlagAddress;
                        viewModel.FromName = dataPoHeader.FromName;
                        viewModel.FromAddress = dataPoHeader.FromAddress;
                        viewModel.FromTel = dataPoHeader.FromTel;
                        viewModel.FromEmail = dataPoHeader.FromEmail;
                        viewModel.ToName = dataPoHeader.ToName;
                        viewModel.ToAddress = dataPoHeader.ToAddress;
                        viewModel.ToTel = dataPoHeader.ToTel;
                        viewModel.ToEmail = dataPoHeader.ToEmail;
                        viewModel.ShippingValue = dataPoHeader.ShippingValue;
                        viewModel.PlatformValue = dataPoHeader.PlatformValue;
                        viewModel.Remark = dataPoHeader.Remark;


                        // End

                        // Mapping Product

                        viewModel.productDescription = dataProductMaster.ProductDescription;
                        viewModel.productDimension = dataProductMaster.ProductDimension;
                        viewModel.productHeight = dataProductMaster.ProductHeight;
                        viewModel.productName = dataProductMaster.ProductName;
                        viewModel.productSku = dataProductMaster.ProductSku;
                        viewModel.productWeight = dataProductMaster.ProductWeight;
                        viewModel.productWidth = dataProductMaster.ProductWidth;
                        viewModel.ProductId = dataProductMaster.ProductId;
                        viewModel.productColor = dataProductMaster.ProductColor;

                        // ========= End Mapping

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

            ViewPurchaseOrderItemModel viewData = new ViewPurchaseOrderItemModel();

            PurchaseOrderItem item = new PurchaseOrderItem();

            try
            {
                var queryable = await Task.Run(() => _dbContext.PurchaseOrderItems.Where(x => x.Id == id).AsQueryable());

                item = queryable.FirstOrDefault();

                if (item != null)
                {
                    var queryableMasterVendor = _dbContext.MasterVendors.AsQueryable();
                    var queryablePoHeader = _dbContext.PurchaseOrderHeaders.AsQueryable();
                    var queryableProductMaster = _dbContext.MasterProducts.AsQueryable();


                    ViewPurchaseOrderItemModel viewModel = new ViewPurchaseOrderItemModel();

                    var dataProductMaster = queryableProductMaster.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                    var dataPoHeader = queryablePoHeader.Where(x => x.Id == item.HeaderId).FirstOrDefault();

                    var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                    var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));


                    viewModel.Id = item.Id;
                    viewModel.headerDocumentNo = dataPoHeader.DocumentNo;
                    viewModel.HeaderId = item.HeaderId;
                    viewModel.CreateBy = getUserCrete;
                    viewModel.CreateDate = item.CreateDate;
                    viewModel.UpdateBy = getUserUpdate;
                    viewModel.UpdateDate = item.UpdateDate;
                    viewModel.StatusValue = item.StatusValue;
                    viewModel.Amount = item.Amount;


                    viewModel.VendorFlagAddress = dataPoHeader.VendorFlagAddress;
                    viewModel.FromName = dataPoHeader.FromName;
                    viewModel.FromAddress = dataPoHeader.FromAddress;
                    viewModel.FromTel = dataPoHeader.FromTel;
                    viewModel.FromEmail = dataPoHeader.FromEmail;
                    viewModel.ToName = dataPoHeader.ToName;
                    viewModel.ToAddress = dataPoHeader.ToAddress;
                    viewModel.ToTel = dataPoHeader.ToTel;
                    viewModel.ToEmail = dataPoHeader.ToEmail;
                    viewModel.ShippingValue = dataPoHeader.ShippingValue;
                    viewModel.Remark = dataPoHeader.Remark;

                    // Mapping Product

                    viewModel.productDescription = dataProductMaster.ProductDescription;
                    viewModel.productDimension = dataProductMaster.ProductDimension;
                    viewModel.productHeight = dataProductMaster.ProductHeight;
                    viewModel.productName = dataProductMaster.ProductName;
                    viewModel.productSku = dataProductMaster.ProductSku;
                    viewModel.productWeight = dataProductMaster.ProductWeight;
                    viewModel.productWidth = dataProductMaster.ProductWidth;
                    viewModel.ProductId = dataProductMaster.ProductId;

                    // ========= End Mapping





                    // Mapping Location

                    //viewModel.locationDescription = dataLocationAtStock.LocationDescription;
                    //viewModel.locationName = dataLocationAtStock.LocationName;
                    //viewModel.locationType = dataLocationAtStock.LocationType;
                    //viewModel.AtStockLocationId = dataLocationAtStock.LocationId;

                    // ========= End Mapping




                    // Mapping Sheft

                    //viewModel.sheftDescription = dataSheftAtStock.SheftDescription;
                    //viewModel.sheftName = dataSheftAtStock.SheftName;
                    //viewModel.AtStockSheftId = dataSheftAtStock.SheftId;

                    // ========= End Mapping




                    // Mapping Bin

                    //viewModel.binNumber = dataBinAtStock.BinNumber;
                    //viewModel.binName = dataBinAtStock.BinName;
                    //viewModel.binDescription = dataBinAtStock.BinDescription;
                    //viewModel.binStatus = dataBinAtStock.BinStatus;
                    //viewModel.binType = dataBinAtStock.BinType;
                    //viewModel.AtStockBinId = dataBinAtStock.BinId;

                    // ========= End Mapping


                    // Mapping Bin

                    //viewModel.picklistBinNumber = dataBinPicklist.BinNumber;
                    //viewModel.picklistBinName = dataBinPicklist.BinName;
                    //viewModel.picklistBinDescription = dataBinPicklist.BinDescription;
                    //viewModel.picklistBinStatus = dataBinPicklist.BinStatus;
                    //viewModel.picklistBinType = dataBinPicklist.BinType;
                    //viewModel.PicklistBinId = dataBinPicklist.BinId;

                    // ========= End Mapping



                    // Mapping Bin

                    //viewModel.packingBinNumber = dataBinPacking.BinNumber;
                    //viewModel.packingBinName = dataBinPacking.BinName;
                    //viewModel.packingBinDescription = dataBinPacking.BinDescription;
                    //viewModel.packingBinStatus = dataBinPacking.BinStatus;
                    //viewModel.packingBinType = dataBinPacking.BinType;
                    //viewModel.PackingBinId = dataBinPacking.BinId;

                    // ========= End Mapping


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

