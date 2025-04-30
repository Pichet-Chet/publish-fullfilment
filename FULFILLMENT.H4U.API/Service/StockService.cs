using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;
using System.Diagnostics;

namespace FULFILLMENT.H4U.API.Service
{
    public class StockService
    {
        HelperService helper = new HelperService();

        readonly DataContext _dbContext = new();

        public async Task<Response> GET_ALL(FilterModel param)
        {
            Response resp = new Response();


            List<ViewStockModel> viewData = new List<ViewStockModel>();

            List<Stock> getData = new List<Stock>();

            try
            {
                var queryable = await Task.Run(() => _dbContext.Stocks.AsQueryable());

                var queryableProductMaster = _dbContext.MasterProducts.AsQueryable();

                var queryableVendorMaster = _dbContext.MasterVendors.AsQueryable();

                var queryableLocationMaster = _dbContext.MasterLocations.AsQueryable();

                var queryableSheftMaster = _dbContext.MasterShefts.AsQueryable();

                var queryableBinMaster = _dbContext.MasterBins.AsQueryable();


                if (param.vendorId != null)
                {
                    queryable = queryable.Where(x => x.VendorId == param.vendorId).AsQueryable();
                }

                if (param.productId != null)
                {
                    queryable = queryable.Where(x => x.ProductId == param.productId).AsQueryable();
                }

                if (param.locationId != null)
                {
                    queryable = queryable.Where(x => x.LocationId == param.locationId).AsQueryable();
                }

                if (param.sheftId != null)
                {
                    queryable = queryable.Where(x => x.SheftId == param.sheftId).AsQueryable();
                }

                if (param.binId != null)
                {
                    queryable = queryable.Where(x => x.BinId == param.binId).AsQueryable();
                }

                if (param.dateFrom != null)
                {
                    queryable = queryable.Where(x => x.UpdateDate >= param.dateFrom).AsQueryable();
                }

                if (param.dateTo != null)
                {
                    queryable = queryable.Where(x => x.UpdateDate <= param.dateFrom).AsQueryable();
                }

                getData = queryable.ToList();

                if (getData.Count > 0)
                {
                    foreach (var item in getData)
                    {
                        ViewStockModel viewModel = new ViewStockModel();

                        var dataProductMaster = queryableProductMaster.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                        var DataVendor = queryableVendorMaster.Where(x => x.VendorId == item.VendorId).FirstOrDefault();
                        var dataLocationMaster = queryableLocationMaster.Where(x => x.LocationId == item.LocationId).FirstOrDefault();
                        var dataSheftMaster = queryableSheftMaster.Where(x => x.SheftId == item.SheftId).FirstOrDefault();
                        var dataBinMaster = queryableBinMaster.Where(x => x.BinId == item.BinId).FirstOrDefault();

                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));





                        //Not Mapping

                        viewModel.Id = item.Id;
                        viewModel.Amount = item.Amount;
                        viewModel.Status = item.Status;
                        viewModel.CreateBy = getUserCrete;
                        viewModel.CreateDate = item.CreateDate;
                        viewModel.UpdateBy = getUserUpdate;
                        viewModel.UpdateDate = item.UpdateDate;
                        viewModel.IsActive = item.IsActive;

                        //================


                        // Mapping Vendor

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

                        // ========= End Mapping




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





                        // Mapping Location

                        viewModel.locationDescription = dataLocationMaster.LocationDescription;
                        viewModel.locationName = dataLocationMaster.LocationName;
                        viewModel.locationType = dataLocationMaster.LocationType;
                        viewModel.LocationId = dataLocationMaster.LocationId;

                        // ========= End Mapping




                        // Mapping Sheft

                        viewModel.sheftDescription = dataSheftMaster.SheftDescription;
                        viewModel.sheftName = dataSheftMaster.SheftName;
                        viewModel.SheftId = dataSheftMaster.SheftId;

                        // ========= End Mapping




                        // Mapping Bin

                        viewModel.binNumber = dataBinMaster == null ? "N/A" : dataBinMaster.BinNumber;
                        viewModel.binName = dataBinMaster == null ? "N/A" : dataBinMaster.BinName;
                        viewModel.binDescription = dataBinMaster == null ? "N/A" : dataBinMaster.BinDescription;
                        viewModel.binStatus = dataBinMaster == null ? "N/A" : dataBinMaster.BinStatus;
                        viewModel.binType = dataBinMaster == null ? "N/A" : dataBinMaster.BinType;
                        viewModel.BinId = dataBinMaster == null ? 0 : dataBinMaster.BinId;

                        // ========= End Mapping

                        viewData.Add(viewModel);


                    }

                    resp.status = true;

                    resp.message = Constants.GET_DATA_SUCCESS;

                    resp.output_data = viewData.OrderBy(x=>x.sheftName);

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
        public async Task<Response> GET_STOCK_MOVE_MENT(FilterModel param)
        {
            Response resp = new Response();

            List<ViewStockMovementModel> viewData = new List<ViewStockMovementModel>();

            List<StockManualLog> getData = new List<StockManualLog>();

            try
            {
                var queryable = await Task.Run(() => _dbContext.StockManualLogs.AsQueryable());

                var queryableProductMaster = _dbContext.MasterProducts.AsQueryable();

                var queryableVendorMaster = _dbContext.MasterVendors.AsQueryable();

                var queryableLocationMaster = _dbContext.MasterLocations.AsQueryable();

                var queryableSheftMaster = _dbContext.MasterShefts.AsQueryable();

                var queryableBinMaster = _dbContext.MasterBins.AsQueryable();

                getData = queryable.ToList();

                if (getData.Count > 0)
                {
                    foreach (var item in getData)
                    {
                        ViewStockMovementModel viewModel = new ViewStockMovementModel();

                        var getDataStock = await Task.Run(() => _dbContext.Stocks.Where(x => x.Id == item.StockId).FirstOrDefault());

                        var dataProductMaster = queryableProductMaster.Where(x => x.ProductId == getDataStock.ProductId).FirstOrDefault();
                        var DataVendor = queryableVendorMaster.Where(x => x.VendorId == getDataStock.VendorId).FirstOrDefault();
                        var dataLocationMaster = queryableLocationMaster.Where(x => x.LocationId == getDataStock.LocationId).FirstOrDefault();
                        var dataSheftMaster = queryableSheftMaster.Where(x => x.SheftId == getDataStock.SheftId).FirstOrDefault();
                        var dataBinMaster = queryableBinMaster.Where(x => x.BinId == getDataStock.BinId).FirstOrDefault();

                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));



                        //Not Mapping

                        viewModel.Id = item.Id;
                        viewModel.Type = item.Type;
                        viewModel.AmountOld = item.AmountOld;
                        viewModel.AmountChange = item.AmountChange;
                        viewModel.Type = item.Type;
                        viewModel.Reason = item.Reason;
                        viewModel.CreateBy = getUserCrete;
                        viewModel.CreateDate = item.CreateDate;
                        viewModel.UpdateBy = getUserUpdate;
                        viewModel.UpdateDate = item.UpdateDate;

                        //================


                        // Mapping Vendor

                        viewModel.VendorId = DataVendor.VendorId;
                        viewModel.VendorCode = DataVendor.VendorCode;
                        viewModel.VendorName = DataVendor.VendorName;
                        viewModel.TaxId = DataVendor.TaxId;
                        viewModel.ContactName = DataVendor.ContactName;
                        viewModel.ContactTel = DataVendor.ContactTel;
                        viewModel.ContactAddress = DataVendor.ContactAddress;
                        viewModel.Email = DataVendor.Email;
                        viewModel.LineId = DataVendor.LineId;
                        viewModel.Website = DataVendor.Website;

                        // ========= End Mapping




                        // Mapping Product

                        viewModel.ProductDescription = dataProductMaster.ProductDescription;
                        viewModel.ProductDimension = dataProductMaster.ProductDimension;
                        viewModel.ProductHeight = dataProductMaster.ProductHeight;
                        viewModel.ProductName = dataProductMaster.ProductName;
                        viewModel.ProductSku = dataProductMaster.ProductSku;
                        viewModel.ProductWeight = dataProductMaster.ProductWeight;
                        viewModel.ProductWidth = dataProductMaster.ProductWidth;
                        viewModel.ProductId = dataProductMaster.ProductId;

                        // ========= End Mapping





                        // Mapping Location

                        viewModel.LocationDescription = dataLocationMaster.LocationDescription;
                        viewModel.LocationName = dataLocationMaster.LocationName;
                        viewModel.LocationType = dataLocationMaster.LocationType;
                        viewModel.LocationId = dataLocationMaster.LocationId;

                        // ========= End Mapping




                        // Mapping Sheft

                        viewModel.SheftDescription = dataSheftMaster.SheftDescription;
                        viewModel.SheftName = dataSheftMaster.SheftName;
                        viewModel.SheftId = dataSheftMaster.SheftId;

                        // ========= End Mapping




                        // Mapping Bin

                        viewModel.BinNumber = dataBinMaster == null ? "N/A" : dataBinMaster.BinNumber;
                        viewModel.BinName = dataBinMaster == null ? "N/A" : dataBinMaster.BinName;
                        viewModel.BinDescription = dataBinMaster == null ? "N/A" : dataBinMaster.BinDescription;
                        viewModel.BinStatus = dataBinMaster == null ? "N/A" : dataBinMaster.BinStatus;
                        viewModel.BinType = dataBinMaster == null ? "N/A" : dataBinMaster.BinType;
                        viewModel.BinId = dataBinMaster == null ? 0 : dataBinMaster.BinId;

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

            Stock item = new Stock();

            ViewStockModel viewModel = new ViewStockModel();

            try
            {
                item = await Task.Run(() => _dbContext.Stocks.Where(x => x.Id == id).FirstOrDefault());

                if (item != null)
                {

                    var queryableProductMaster = _dbContext.MasterProducts.AsQueryable();

                    var queryableVendorMaster = _dbContext.MasterVendors.AsQueryable();

                    var queryableLocationMaster = _dbContext.MasterLocations.AsQueryable();

                    var queryableSheftMaster = _dbContext.MasterShefts.AsQueryable();

                    var queryableBinMaster = _dbContext.MasterBins.AsQueryable();


                    var dataProductMaster = queryableProductMaster.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                    var DataVendor = queryableVendorMaster.Where(x => x.VendorId == item.VendorId).FirstOrDefault();
                    var dataLocationMaster = queryableLocationMaster.Where(x => x.LocationId == item.LocationId).FirstOrDefault();
                    var dataSheftMaster = queryableSheftMaster.Where(x => x.SheftId == item.SheftId).FirstOrDefault();
                    var dataBinMaster = queryableBinMaster.Where(x => x.BinId == item.BinId).FirstOrDefault();

                    var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                    var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));


                    //Not Mapping

                    viewModel.Id = item.Id;
                    viewModel.Amount = item.Amount;
                    viewModel.Status = item.Status;
                    viewModel.CreateBy = getUserCrete;
                    viewModel.CreateDate = item.CreateDate;
                    viewModel.UpdateBy = getUserUpdate;
                    viewModel.UpdateDate = item.UpdateDate;
                    viewModel.IsActive = item.IsActive;

                    //================


                    // Mapping Vendor

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

                    // ========= End Mapping




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

                    viewModel.locationDescription = dataLocationMaster.LocationDescription;
                    viewModel.locationName = dataLocationMaster.LocationName;
                    viewModel.locationType = dataLocationMaster.LocationType;
                    viewModel.LocationId = dataLocationMaster.LocationId;

                    // ========= End Mapping




                    // Mapping Sheft

                    viewModel.sheftDescription = dataSheftMaster.SheftDescription;
                    viewModel.sheftName = dataSheftMaster.SheftName;
                    viewModel.SheftId = dataSheftMaster.SheftId;

                    // ========= End Mapping




                    // Mapping Bin

                    viewModel.binNumber = dataBinMaster == null ? "N/A" : dataBinMaster.BinNumber;
                    viewModel.binName = dataBinMaster == null ? "N/A" : dataBinMaster.BinName;
                    viewModel.binDescription = dataBinMaster == null ? "N/A" : dataBinMaster.BinDescription;
                    viewModel.binStatus = dataBinMaster == null ? "N/A" : dataBinMaster.BinStatus;
                    viewModel.binType = dataBinMaster == null ? "N/A" : dataBinMaster.BinType;
                    viewModel.BinId = dataBinMaster == null ? 0 : dataBinMaster.BinId;


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
        public async Task<Response> GET_STOCK_BALANCE_ITEM(int productId)
        {
            Response resp = new Response();

            int result = 0;

            try
            {
                var getListOrderCreated = await Task.Run(() => _dbContext.PurchaseOrderItems.Where(x => x.ProductId == productId && x.StatusValue == Constants.PURCHASE_ORDER_STATUS_PENDING).ToList());

                var getListData = await Task.Run(() => _dbContext.Stocks.Where(x => x.ProductId == productId).ToList());

                if (getListData != null && getListData.Count > 0)
                {
                    if (getListData != null && getListData.Count > 0)
                    {
                        result = (getListData.Sum(x => x.Amount).Value - getListOrderCreated.Sum(x => x.Amount).Value);

                    }
                    else
                    {
                        result = getListData.Sum(x => x.Amount).Value;

                    }


                    resp.status = true;

                    resp.message = Constants.GET_DATA_SUCCESS;

                    resp.output_data = result;
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
        public async Task<Response> UPDATE(StockManualLog param)
        {
            Response resp = new Response();

            try
            {
                var update = await Task.Run(() => _dbContext.Stocks.Where(x => x.Id == param.Id).FirstOrDefault());


                if (update != null)
                {
                    if (string.IsNullOrEmpty(param.Type))
                    {
                        if (param.Type != Constants.STOCK_MANUAL_TYPE_ADD || param.Type != Constants.STOCK_MANUAL_TYPE_ADD)
                        {

                            param.Id = 0;
                            param.StockId = update.Id;
                            param.AmountOld = update.Amount;
                            param.AmountChange = param.AmountChange;
                            param.Reason = param.Reason;
                            param.Type = param.AmountChange >= update.Amount ? "ADD" : "DELETE";
                            param.CreateDate = helper.GetDateTimeNow();
                            param.UpdateDate = helper.GetDateTimeNow();

                            if (param.AmountChange <= 0)
                            {
                                var getDataStock = await Task.Run(() => _dbContext.Stocks.Where(x => x.Id == param.StockId).FirstOrDefault());
                                var getDataBin = await Task.Run(() => _dbContext.MasterBins.Where(x => x.BinId == getDataStock.BinId).FirstOrDefault());

                                getDataBin.SheftId = null;
                            }

                            await Task.Run(() => _dbContext.StockManualLogs.Add(param));

                            _dbContext.SaveChanges();

                        }
                    }


                    update.UpdateBy = param.UpdateBy;
                    update.UpdateDate = helper.GetDateTimeNow();
                    update.Amount = param.AmountChange;

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
