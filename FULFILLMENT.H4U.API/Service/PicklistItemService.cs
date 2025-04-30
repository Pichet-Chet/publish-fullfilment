using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;
using System.Diagnostics;

namespace FULFILLMENT.H4U.API.Service
{
    public class PicklistItemService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();
        public async Task<Response> GET_ALL_BY_HEADER(int headerId)
        {
            Response resp = new Response();

            List<ViewPicklistItemModel> viewData = new List<ViewPicklistItemModel>();

            List<PicklistItem> getData = new List<PicklistItem>();

            try
            {
                var queryable = await Task.Run(() => _dbContext.PicklistItems.Where(x => x.HeaderId == headerId).AsQueryable());

                getData = queryable.ToList();

                if (getData.Count > 0)
                {
                    var queryableMasterVendor = _dbContext.MasterVendors.AsQueryable();
                    var queryableMasterBox = _dbContext.MasterBoxes.AsQueryable();
                    var queryableProductMaster = _dbContext.MasterProducts.AsQueryable();
                    var queryableLocationMaster = _dbContext.MasterLocations.AsQueryable();
                    var queryableSheftMaster = _dbContext.MasterShefts.AsQueryable();
                    var queryableBinMaster = _dbContext.MasterBins.AsQueryable();

                    foreach (var item in getData)
                    {
                        ViewPicklistItemModel viewModel = new ViewPicklistItemModel();

                        var dataProductMaster = queryableProductMaster.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                        var dataLocationAtStock = queryableLocationMaster.Where(x => x.LocationId == item.AtLocation).FirstOrDefault();
                        var dataSheftAtStock = queryableSheftMaster.Where(x => x.SheftId == item.AtSheft).FirstOrDefault();
                        var dataBinAtStock = queryableBinMaster.Where(x => x.BinId == item.AtBin).FirstOrDefault();

                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));


                        viewModel.Id = item.Id;
                        viewModel.HeaderId = item.HeaderId;
                        viewModel.CreateBy = getUserCrete;
                        viewModel.CreateDate = item.CreateDate;
                        viewModel.UpdateBy = getUserUpdate;
                        viewModel.UpdateDate = item.UpdateDate;

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





                        //Mapping Location

                        viewModel.locationDescription = dataLocationAtStock.LocationDescription;
                        viewModel.locationName = dataLocationAtStock.LocationName;
                        viewModel.locationType = dataLocationAtStock.LocationType;
                        viewModel.AtLocation = dataLocationAtStock.LocationId;

                        //========= End Mapping




                        //Mapping Sheft

                        viewModel.sheftDescription = dataSheftAtStock.SheftDescription;
                        viewModel.sheftName = dataSheftAtStock.SheftName;
                        viewModel.AtSheft = dataSheftAtStock.SheftId;

                        //========= End Mapping




                        //Mapping Bin

                        viewModel.binNumber = dataBinAtStock.BinNumber;
                        viewModel.binName = dataBinAtStock.BinName;
                        viewModel.binDescription = dataBinAtStock.BinDescription;
                        viewModel.binStatus = dataBinAtStock.BinStatus;
                        viewModel.binType = dataBinAtStock.BinType;
                        viewModel.AtBin = dataBinAtStock.BinId;

                        //========= End Mapping



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

            PicklistItem item = new PicklistItem();

            try
            {
                var queryable = await Task.Run(() => _dbContext.PicklistItems.Where(x => x.Id == id).AsQueryable());

                item = queryable.FirstOrDefault();

                if (item != null)
                {
                    var queryableMasterVendor = _dbContext.MasterVendors.AsQueryable();
                    var queryableMasterBox = _dbContext.MasterBoxes.AsQueryable();
                    var queryableProductMaster = _dbContext.MasterProducts.AsQueryable();
                    var queryableLocationMaster = _dbContext.MasterLocations.AsQueryable();
                    var queryableSheftMaster = _dbContext.MasterShefts.AsQueryable();
                    var queryableBinMaster = _dbContext.MasterBins.AsQueryable();

                    ViewPicklistItemModel viewModel = new ViewPicklistItemModel();

                    var dataProductMaster = queryableProductMaster.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                    var dataLocationAtStock = queryableLocationMaster.Where(x => x.LocationId == item.AtLocation).FirstOrDefault();
                    var dataSheftAtStock = queryableSheftMaster.Where(x => x.SheftId == item.AtSheft).FirstOrDefault();
                    var dataBinAtStock = queryableBinMaster.Where(x => x.BinId == item.AtBin).FirstOrDefault();

                    var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                    var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));


                    viewModel.Id = item.Id;
                    viewModel.HeaderId = item.HeaderId;
                    viewModel.CreateBy = getUserCrete;
                    viewModel.CreateDate = item.CreateDate;
                    viewModel.UpdateBy = getUserUpdate;
                    viewModel.UpdateDate = item.UpdateDate;

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





                    //Mapping Location

                    viewModel.locationDescription = dataLocationAtStock.LocationDescription;
                    viewModel.locationName = dataLocationAtStock.LocationName;
                    viewModel.locationType = dataLocationAtStock.LocationType;
                    viewModel.AtLocation = dataLocationAtStock.LocationId;

                    //========= End Mapping




                    //Mapping Sheft

                    viewModel.sheftDescription = dataSheftAtStock.SheftDescription;
                    viewModel.sheftName = dataSheftAtStock.SheftName;
                    viewModel.AtSheft = dataSheftAtStock.SheftId;

                    //========= End Mapping




                    //Mapping Bin

                    viewModel.binNumber = dataBinAtStock.BinNumber;
                    viewModel.binName = dataBinAtStock.BinName;
                    viewModel.binDescription = dataBinAtStock.BinDescription;
                    viewModel.binStatus = dataBinAtStock.BinStatus;
                    viewModel.binType = dataBinAtStock.BinType;
                    viewModel.AtBin = dataBinAtStock.BinId;

                    //========= End Mapping

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
