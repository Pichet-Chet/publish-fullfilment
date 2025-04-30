using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;
using Org.BouncyCastle.Asn1.X509;
using System.Diagnostics;

namespace FULFILLMENT.H4U.API.Service
{
    public class InboundItemService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();


        public async Task<Response> GET_ALL_BY_HEADER(int headerId)
        {
            Response resp = new Response();

            List<MasterProduct> productMasrer = new List<MasterProduct>();

            List<ViewInboundItemModel> viewData = new List<ViewInboundItemModel>();

            List<InboundItem> getData = new List<InboundItem>();

            try
            {


                getData = await Task.Run(() => _dbContext.InboundItems.Where(x => x.HeaderId == headerId).ToList());

                if (getData.Count > 0)
                {
                    productMasrer = _dbContext.MasterProducts.ToList();

                    foreach (var item in getData)
                    {
                        ViewInboundItemModel viewModel = new ViewInboundItemModel();

                        var checkGr = await Task.Run(() => _dbContext.GoodReceiveHeaders.Where(x => x.InboundHeaderId == item.HeaderId).FirstOrDefault());

                        if (checkGr != null)
                        {
                            viewModel.Received = await Task.Run(() => _dbContext.GoodReceiveItems.Where(x => x.HeaderId == checkGr.Id && x.ProductId == item.ProductId).Sum(x => x.Amount));
                        }


                        var DataProduct = productMasrer.Where(x => x.ProductId == item.ProductId).FirstOrDefault();

                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));

                        viewModel.CreateBy = getUserCrete;
                        viewModel.CreateDate = item.CreateDate;

                        viewModel.UpdateBy = getUserCrete;
                        viewModel.UpdateDate = item.UpdateDate;

                        viewModel.Id = item.Id;
                        viewModel.HeaderId = item.HeaderId;
                        viewModel.Amount = item.Amount;
                        viewModel.ItemStatus = item.ItemStatus;
                        viewModel.Remark = item.Remark;

                        viewModel.ProductId = item.ProductId;

                        viewModel.ProductSku = DataProduct.ProductSku;
                        viewModel.ProductName = DataProduct.ProductName;
                        viewModel.ProductDescription = DataProduct.ProductDescription;
                        viewModel.ProductColor = DataProduct.ProductColor;
                        viewModel.ProductHeight = DataProduct.ProductHeight;
                        viewModel.ProductWidth = DataProduct.ProductWidth;
                        viewModel.ProductDimension = DataProduct.ProductDimension;
                        viewModel.ProductWeight = DataProduct.ProductWeight;

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

            List<MasterProduct> productMasrer = new List<MasterProduct>();

            ViewInboundItemModel viewModel = new ViewInboundItemModel();

            InboundItem item = new InboundItem();

            try
            {
                item = await Task.Run(() => _dbContext.InboundItems.Where(x => x.Id == id).FirstOrDefault());

                if (item != null)
                {
                    productMasrer = _dbContext.MasterProducts.ToList();

                    var DataProduct = productMasrer.Where(x => x.ProductId == item.ProductId).FirstOrDefault();

                    var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));

                    viewModel.CreateBy = getUserCrete;
                    viewModel.CreateDate = item.CreateDate;

                    viewModel.UpdateBy = getUserCrete;
                    viewModel.UpdateDate = item.UpdateDate;

                    viewModel.Id = item.Id;
                    viewModel.HeaderId = item.HeaderId;
                    viewModel.Amount = item.Amount;
                    viewModel.ItemStatus = item.ItemStatus;
                    viewModel.Remark = item.Remark;

                    viewModel.ProductId = item.ProductId;

                    viewModel.ProductSku = DataProduct.ProductSku;
                    viewModel.ProductName = DataProduct.ProductName;
                    viewModel.ProductDescription = DataProduct.ProductDescription;
                    viewModel.ProductColor = DataProduct.ProductColor;
                    viewModel.ProductHeight = DataProduct.ProductHeight;
                    viewModel.ProductWidth = DataProduct.ProductWidth;
                    viewModel.ProductDimension = DataProduct.ProductDimension;
                    viewModel.ProductWeight = DataProduct.ProductWeight;


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

        public async Task<Response> UPDATE(InboundItem param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {
                    var chkSatus = await Task.Run(() => _dbContext.InboundItems.Where(x => x.Id == param.Id && x.ItemStatus == Constants.INBOUND_STATUS_SHIPPING).FirstOrDefault());

                    if (chkSatus != null)
                    {
                        var update = await Task.Run(() => _dbContext.InboundItems.Where(x => x.Id == param.Id).FirstOrDefault());

                        if (update != null)
                        {
                            update.Amount = param.Amount;
                            update.ItemStatus = param.ItemStatus;
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

    }
}
