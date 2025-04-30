using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;
using FULFILLMENT.H4U.API.Service.Helper;
using System.Globalization;

namespace FULFILLMENT.H4U.API.Service.transaction
{
    public class transactionPicklistService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();

        public async Task<Response> INSERT(TransactionPicklist param)
        {
            Response resp = new Response();

            try
            {
                List<PicklistItem> listItem = new List<PicklistItem>();

                bool validate = true;

                string typeDocument = Constants.DOCUMENT_TYPE_PICK_LIST;

                var getDataVendor = await Task.Run(() => _dbContext.MasterVendors.Where(x => x.VendorId == param.header.VendorId).FirstOrDefault());

                if (getDataVendor != null)
                {
                    

                    if (param.header != null && param.item.Count > 0)
                    {
                        var queryableStock = await Task.Run(() => _dbContext.Stocks.AsQueryable());

                        var queryableProduct = await Task.Run(() => _dbContext.MasterProducts.AsQueryable());

                        var queryableBin = await Task.Run(() => _dbContext.MasterBins.AsQueryable());

                        foreach (var item in param.item)
                        {
                            if (item.AtSheft != null && item.AtBin != null)
                            {
                                item.CreateDate = helper.GetDateTimeNow();
                                item.UpdateDate = helper.GetDateTimeNow();

                                var getMasterProduct = queryableProduct.Where(x => x.ProductId == item.ProductId && x.VendorId == param.header.VendorId).FirstOrDefault();

                                var getDataStock = queryableStock.Where(x => x.ProductId == item.ProductId && x.LocationId == item.AtLocation && x.SheftId == item.AtSheft && x.BinId == item.AtBin && x.VendorId == param.header.VendorId).FirstOrDefault();

                                if (getMasterProduct == null)
                                {
                                    resp.status = false;
                                    resp.message = Constants.NOT_FOUND_PRODUCT_WITH_IN_YOUR_MASTER_PRODUCT;

                                    return resp;
                                }

                                if (getDataStock == null)
                                {
                                    resp.status = false;
                                    resp.message = Constants.NOT_FOUND_PRODUCT_WITH_IN_STOCK;

                                    return resp;
                                }

                                if (item.Amount > getDataStock.Amount)
                                {
                                    resp.status = false;
                                    resp.message = Constants.ITEM_STOCK_NOT_ENOUGH;

                                    return resp;
                                }

                                if (getDataStock.Amount <= 0)
                                {
                                    resp.status = false;
                                    resp.message = Constants.ITEM_STOCK_NOT_ENOUGH;

                                    return resp;
                                }

                                //-- Start : Stock Update

                                getDataStock.Amount = getDataStock.Amount - item.Amount;

                                if (getDataStock.Amount <= 0)
                                {
                                    var findBinMaster = await Task.Run(() => _dbContext.MasterBins.Where(x => x.BinId == item.AtBin && x.SheftId == item.AtSheft).FirstOrDefault());

                                    if (findBinMaster != null)
                                    {
                                        findBinMaster.SheftId = null;

                                        await _dbContext.SaveChangesAsync();
                                    }
                                }

                                //-- END : Stock Update

                                listItem.Add(item);
                            }
                            else
                            {
                                resp.status = false;
                                resp.message = Constants.ITEM_STOCK_NOT_ENOUGH;

                                return resp;
                            }
                        }

                        if (listItem.Count > 0)
                        {
                            param.header.Id = 0;
                            param.header.DocumentNo = await helper.GenerateDocumentNo(typeDocument, (int)param.header.VendorId);
                            param.header.DocumentStatus = Constants.PURCHASE_ORDER_STATUS_PENDING;
                            param.header.CreateDate = helper.GetDateTimeNow();
                            param.header.UpdateDate = helper.GetDateTimeNow();

                            var getDataBin = await Task.Run(() => queryableBin.Where(x => x.BinId == param.header.BinId).FirstOrDefault());

                            var getDataPurchaseOrder = await Task.Run(() => _dbContext.PurchaseOrderHeaders.Where(x => x.Id == param.header.PurchaseOrderHeaderId).FirstOrDefault());

                            if (getDataPurchaseOrder != null)
                            {
                                SysDocument sysDocument = new SysDocument();

                                sysDocument.Tpye = Constants.DOCUMENT_TYPE_PICK_LIST;
                                sysDocument.VendorId = param.header.VendorId;
                                sysDocument.DocumentNo = param.header.DocumentNo;
                                sysDocument.DocumentYear = helper.GetYear();
                                sysDocument.DocumentMonth = helper.GetMonth();
                                sysDocument.DocumentDay = helper.GetDay();
                                sysDocument.CreateDate = helper.GetDateTimeNow();

                                //-- Start PO Update Status 

                                getDataPurchaseOrder.DocumentStatus = Constants.PURCHASE_ORDER_STATUS_PICKLIST;

                                var getDataPurchaseOrderItem = await Task.Run(() => _dbContext.PurchaseOrderItems.Where(x => x.HeaderId == getDataPurchaseOrder.Id).ToList());

                                if (getDataPurchaseOrderItem != null && getDataPurchaseOrderItem.Count > 0)
                                {
                                    foreach (var poItem in getDataPurchaseOrderItem)
                                    {
                                        poItem.StatusValue = Constants.PURCHASE_ORDER_STATUS_PICKLIST;
                                    }
                                }


                                //-- End PO Update Status

                                await Task.Run(() => _dbContext.SysDocuments.Add(sysDocument));
                                await Task.Run(() => _dbContext.PicklistHeaders.Add(param.header));

                                _dbContext.SaveChangesAsync();

                                foreach (var item in listItem)
                                {
                                    item.HeaderId = param.header.Id;
                                }

                                getDataBin.PicklistId = param.header.Id;

                                await Task.Run(() => _dbContext.PicklistItems.AddRangeAsync(listItem));

                                await _dbContext.SaveChangesAsync();

                                resp.status = true;
                                resp.message = Constants.INSERT_SUCCESS;
                            }
                            else
                            {
                                resp.status = false;
                                resp.message = Constants.PICK_LIST_PO_NOT_FOUND;

                                return resp;
                            }
                        }

                        else
                        {
                            resp.status = false;
                            resp.message = Constants.INSERT_ERROR;

                            return resp;
                        }



                    }

                    else
                    {
                        resp.status = false;
                        resp.message = Constants.INSERT_ERROR;

                        return resp;
                    }
                }

                else
                {
                    resp.status = false;
                    resp.message = Constants.NOT_FOUND_VENDOR_WITH_IN_MASTER;

                    return resp;
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
