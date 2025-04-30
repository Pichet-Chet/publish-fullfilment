using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;
using System.Diagnostics;

namespace FULFILLMENT.H4U.API.Service
{
    public class GoodReceiveItemService
    {
        HelperService helper = new HelperService();

        readonly DataContext _dbContext = new();

        public async Task<Response> GET_ALL_BY_HEADER(int headerId)
        {
            Response resp = new Response();

            List<ViewGoodReceiveItemModel> viewData = new List<ViewGoodReceiveItemModel>();

            List<GoodReceiveItem> getData = new List<GoodReceiveItem>();

            try
            {
                var queryable = await Task.Run(() => _dbContext.GoodReceiveItems.Where(x => x.HeaderId == headerId).AsQueryable());

                getData = queryable.ToList();

                if (getData.Count > 0)
                {
                    var queryableGoodReceiveHeader = _dbContext.GoodReceiveHeaders.AsQueryable();
                    var queryableProductMaster = _dbContext.MasterProducts.AsQueryable();
                    var queryableLocationMaster = _dbContext.MasterLocations.AsQueryable();
                    var queryableSheftMaster = _dbContext.MasterShefts.AsQueryable();
                    var queryableBinMaster = _dbContext.MasterBins.AsQueryable();


                    foreach (var item in getData)
                    {
                        ViewGoodReceiveItemModel viewModel = new ViewGoodReceiveItemModel();

                        var dataGoodReceiveHeader = queryableGoodReceiveHeader.Where(x => x.Id == item.HeaderId).FirstOrDefault();
                        var dataProductMaster = queryableProductMaster.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                        var dataLocationMaster = queryableLocationMaster.Where(x => x.LocationId == item.LocationId).FirstOrDefault();
                        var dataSheftMaster = queryableSheftMaster.Where(x => x.SheftId == item.SheftId).FirstOrDefault();
                        var dataBinMaster = queryableBinMaster.Where(x => x.BinId == item.BinId).FirstOrDefault();

                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                        //Not Mapping

                        viewModel.Id = item.Id;
                        viewModel.Amount = item.Amount;
                        viewModel.ReceiveStatus = item.ReceiveStatus;
                        viewModel.Remark = item.Remark;
                        viewModel.CreateBy = getUserCrete;
                        viewModel.CreateDate = item.CreateDate;
                        viewModel.UpdateBy = getUserUpdate;
                        viewModel.UpdateDate = item.UpdateDate;
                        viewModel.Lot = item.Lot;
                        viewModel.Seq = item.Seq;

                        //================


                        // Mapping Header

                        viewModel.HeaderId = item.HeaderId;
                        viewModel.headerDocumentNo = dataGoodReceiveHeader.DocumentNo;
                        viewModel.headerReceiveDate = dataGoodReceiveHeader.ReceiveDate;
                        viewModel.headerReceiveStatus = dataGoodReceiveHeader.ReceiveStatus;
                        viewModel.headerRemark = dataGoodReceiveHeader.Remark;

                        // ========= End Mapping




                        // Mapping Product

                        viewModel.productDescription = dataProductMaster.ProductDescription;
                        viewModel.productDimension = dataProductMaster.ProductDimension;
                        viewModel.productHeight = dataProductMaster.ProductHeight;
                        viewModel.productName = dataProductMaster.ProductName;
                        //viewModel.productSku = dataProductMaster.ProductSku;
                        viewModel.productWeight = dataProductMaster.ProductWeight;
                        viewModel.productWidth = dataProductMaster.ProductWidth;
                        viewModel.ProductId = dataProductMaster.ProductId;
                        viewModel.ProductSku = dataProductMaster.ProductSku;

                        // ========= End Mapping





                        // Mapping Location

                        viewModel.locationDescription = dataLocationMaster != null ? dataLocationMaster.LocationDescription : Constants.NULL;
                        viewModel.locationName = dataLocationMaster != null ? dataLocationMaster.LocationName : Constants.NULL;
                        viewModel.locationType = dataLocationMaster != null ? dataLocationMaster.LocationType : Constants.NULL;
                        viewModel.LocationId = dataLocationMaster != null ? dataLocationMaster.LocationId : 0;
                        // ========= End Mapping




                        // Mapping Sheft

                        viewModel.sheftDescription = dataSheftMaster != null ? dataSheftMaster.SheftDescription : Constants.NULL;
                        viewModel.sheftName = dataSheftMaster != null ? dataSheftMaster.SheftName : Constants.NULL;
                        viewModel.SheftId = dataSheftMaster != null ? dataSheftMaster.SheftId : 0;

                        // ========= End Mapping




                        // Mapping Bin

                        viewModel.binNumber = dataBinMaster != null ? dataBinMaster.BinNumber : Constants.NULL;
                        viewModel.binName = dataBinMaster != null ? dataBinMaster.BinName : Constants.NULL;
                        viewModel.binDescription = dataBinMaster != null ? dataBinMaster.BinDescription : Constants.NULL;
                        viewModel.binStatus = dataBinMaster != null ? dataBinMaster.BinStatus : Constants.NULL;
                        viewModel.binType = dataBinMaster != null ? dataBinMaster.BinType : Constants.NULL;
                        viewModel.BinId = dataBinMaster != null ? dataBinMaster.BinId : 0;

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

            ViewGoodReceiveItemModel viewData = new ViewGoodReceiveItemModel();

            GoodReceiveItem item = new GoodReceiveItem();

            try
            {
                var queryable = await Task.Run(() => _dbContext.GoodReceiveItems.Where(x => x.Id == id).AsQueryable());

                item = queryable.FirstOrDefault();

                if (item != null)
                {
                    var queryableGoodReceiveHeader = _dbContext.GoodReceiveHeaders.AsQueryable();
                    var queryableProductMaster = _dbContext.MasterProducts.AsQueryable();
                    var queryableLocationMaster = _dbContext.MasterLocations.AsQueryable();
                    var queryableSheftMaster = _dbContext.MasterShefts.AsQueryable();
                    var queryableBinMaster = _dbContext.MasterBins.AsQueryable();



                    ViewGoodReceiveItemModel viewModel = new ViewGoodReceiveItemModel();

                    var dataGoodReceiveHeader = queryableGoodReceiveHeader.Where(x => x.Id == item.HeaderId).FirstOrDefault();
                    var dataProductMaster = queryableProductMaster.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                    var dataLocationMaster = queryableLocationMaster.Where(x => x.LocationId == item.LocationId).FirstOrDefault();
                    var dataSheftMaster = queryableSheftMaster.Where(x => x.SheftId == item.SheftId).FirstOrDefault();
                    var dataBinMaster = queryableBinMaster.Where(x => x.BinId == item.BinId).FirstOrDefault();

                    var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                    var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                    //Not Mapping

                    viewModel.Id = item.Id;
                    viewModel.Amount = item.Amount;
                    viewModel.ReceiveStatus = item.ReceiveStatus;
                    viewModel.Remark = item.Remark;
                    viewModel.CreateBy = getUserCrete;
                    viewModel.CreateDate = item.CreateDate;
                    viewModel.UpdateBy = getUserUpdate;
                    viewModel.UpdateDate = item.UpdateDate;
                    viewModel.Lot = item.Lot;
                    viewModel.Seq = item.Seq;

                    //================


                    // Mapping Header

                    viewModel.HeaderId = item.HeaderId;
                    viewModel.headerDocumentNo = dataGoodReceiveHeader.DocumentNo;
                    viewModel.headerReceiveDate = dataGoodReceiveHeader.ReceiveDate;
                    viewModel.headerReceiveStatus = dataGoodReceiveHeader.ReceiveStatus;
                    viewModel.headerRemark = dataGoodReceiveHeader.Remark;

                    // ========= End Mapping




                    // Mapping Product

                    viewModel.productDescription = dataProductMaster.ProductDescription;
                    viewModel.productDimension = dataProductMaster.ProductDimension;
                    viewModel.productHeight = dataProductMaster.ProductHeight;
                    viewModel.productName = dataProductMaster.ProductName;
                    viewModel.ProductSku = dataProductMaster.ProductSku;
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

                    viewModel.binNumber = dataBinMaster.BinNumber;
                    viewModel.binName = dataBinMaster.BinName;
                    viewModel.binDescription = dataBinMaster.BinDescription;
                    viewModel.binStatus = dataBinMaster.BinStatus;
                    viewModel.binType = dataBinMaster.BinType;
                    viewModel.BinId = dataBinMaster.BinId;

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

        public async Task<Response> SOFT_DELETE(GoodReceiveHeader param)
        {
            Response resp = new Response();

            GoodReceiveItem dataModel = new GoodReceiveItem();

            try
            {
                if (!string.IsNullOrEmpty(param.Remark))
                {
                    dataModel = await Task.Run(() => _dbContext.GoodReceiveItems.Where(x => x.Id == param.Id).FirstOrDefault());

                    if (dataModel != null)
                    {
                        dataModel.ReceiveStatus = Constants.GOOD_RECEIVE_STATUS_CANCEL;
                        dataModel.Remark = param.Remark;
                        dataModel.UpdateBy = param.UpdateBy;
                        dataModel.UpdateDate = helper.GetDateTimeNow();

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

    }
}
