using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;
using FULFILLMENT.H4U.API.Service.Helper;
using System.Globalization;

namespace FULFILLMENT.H4U.API.Service.transaction
{
    public class transactionPurchaseOrderService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();

        public async Task<Response> INSERT(transactionPurchaseOrder param)
        {
            Response resp = new Response();

            List<PurchaseOrderItem> listItem = new List<PurchaseOrderItem>();

            bool validate = true;

            string typeDocument = Constants.DOCUMENT_TYPE_PURCHASE_ORDER;

            try
            {
                if (param.header != null && param.item.Count > 0)
                {
                    var queryableShipping = await Task.Run(() => _dbContext.MasterShippings.Where(x => x.Value == param.header.ShippingValue).AsQueryable());

                    var getDataVendor = await Task.Run(() => _dbContext.MasterVendors.Where(x => x.VendorId == param.header.VendorId).FirstOrDefault());

                    param.header.DocumentNo = await helper.GenerateDocumentNo(typeDocument, (int)param.header.VendorId);
                    param.header.DocumentStatus = Constants.PURCHASE_ORDER_STATUS_PENDING;
                    param.header.CreateDate = helper.GetDateTimeNow();
                    param.header.UpdateDate = helper.GetDateTimeNow();
                    param.header.TrackingNumber = await helper.GenerateTrackingNo();

                    if (param.header.VendorFlagAddress == true)
                    {
                        param.header.FromName = getDataVendor.ContactName;
                        param.header.FromAddress = getDataVendor.ContactAddress;
                        param.header.FromTel = getDataVendor.ContactTel;
                        param.header.FromEmail = getDataVendor.Email;
                    }

                    if (string.IsNullOrEmpty(param.header.ToName) || string.IsNullOrEmpty(param.header.ToAddress) || string.IsNullOrEmpty(param.header.ToTel))
                    {
                        resp.status = false;
                        resp.message = Constants.PURCHASE_ORDER_INFORMATION_CUSTOMER_IS_REQUIRE;

                        return resp;
                    }

                    validate = queryableShipping.FirstOrDefault() == null ? false : true;

                    if (validate == true)
                    {
                        SysDocument sysDocument = new SysDocument();

                        sysDocument.Tpye = Constants.DOCUMENT_TYPE_PURCHASE_ORDER;
                        sysDocument.VendorId = param.header.VendorId;
                        sysDocument.DocumentNo = param.header.DocumentNo;
                        sysDocument.DocumentYear = helper.GetYear();
                        sysDocument.DocumentMonth = helper.GetMonth();
                        sysDocument.DocumentDay = helper.GetDay();
                        sysDocument.CreateDate = helper.GetDateTimeNow();

                        await Task.Run(() => _dbContext.SysDocuments.Add(sysDocument));
                        await Task.Run(() => _dbContext.PurchaseOrderHeaders.Add(param.header));

                        _dbContext.SaveChangesAsync();

                        var queryableStock = await Task.Run(() => _dbContext.Stocks.AsQueryable());
                        var queryableProduct = await Task.Run(() => _dbContext.MasterProducts.AsQueryable());


                        foreach (var item in param.item)
                        {

                            item.HeaderId = param.header.Id;

                            var getMasterProduct = queryableProduct.Where(x => x.ProductId == item.ProductId && x.VendorId == param.header.VendorId).FirstOrDefault();

                            if (getMasterProduct == null)
                            {
                                resp.status = false;
                                resp.message = Constants.NOT_FOUND_PRODUCT_WITH_IN_YOUR_MASTER_PRODUCT;

                                return resp;
                            }

                            var getDataStock = queryableStock.Where(x => x.ProductId == item.ProductId).FirstOrDefault();

                            if (getDataStock == null)
                            {
                                resp.status = false;
                                resp.message = Constants.NOT_FOUND_PRODUCT_WITH_IN_STOCK;

                                return resp;
                            }

                            if (getDataStock.Amount <= 0)
                            {
                                resp.status = false;
                                resp.message = Constants.ITEM_STOCK_NOT_ENOUGH;

                                return resp;
                            }

                            item.CreateDate = helper.GetDateTimeNow();
                            item.UpdateDate = helper.GetDateTimeNow();


                            listItem.Add(item);

                        }

                        if (listItem.Count > 0)
                        {
                            await Task.Run(() => _dbContext.PurchaseOrderItems.AddRangeAsync(listItem));

                            await _dbContext.SaveChangesAsync();

                            resp.status = true;
                            resp.message = Constants.INSERT_SUCCESS;
                        }
                    }
                    else
                    {
                        resp.status = false;
                        resp.message = Constants.PURCHASE_ORDER_INFORMATION_SHIPPNNG_IS_REQUIRE;

                        return resp;
                    }
                }

                else
                {
                    resp.status = false;
                    resp.message = Constants.INSERT_ERROR;
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

        public async Task<Response> UPDATE(transactionPurchaseOrder param)
        {
            Response resp = new Response();

            try
            {
                PurchaseOrderHeader headerData = new PurchaseOrderHeader();

                if (param.header != null && param.item.Count > 0)
                {
                    var queryable = await Task.Run(() => _dbContext.PurchaseOrderItems.AsQueryable());
                    var queryableStock = await Task.Run(() => _dbContext.Stocks.AsQueryable());

                    headerData = param.header;

                    foreach (var item in param.item)
                    {
                        if (headerData.DocumentStatus != "PENDING" && item.StatusValue != "PENDING")
                        {
                            var updateModel = await Task.Run(() => queryable.Where(x => x.Id == item.Id).FirstOrDefault());

                            var checkStock = queryableStock.Where(x => x.ProductId == item.ProductId && x.VendorId == param.header.VendorId).FirstOrDefault();

                            if (item.Amount <= checkStock.Amount)
                            {
                                updateModel.Amount = item.Amount;

                                _dbContext.Update(updateModel);

                            }
                            else
                            {
                                resp.status = false;
                                resp.message = Constants.UPDATE_DATA_INVALID;

                                return resp;
                            }
                        }
                        else
                        {
                            resp.status = false;
                            resp.message = Constants.UPDATE_DATA_INVALID;


                            return resp;
                        }


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

        public async Task<Response> CANCEL(transactionPurchaseOrder param)
        {
            Response resp = new Response();

            try
            {
                if (param.header != null)
                {
                    PurchaseOrderHeader queryableHeaderGr = new PurchaseOrderHeader();
                    List<PurchaseOrderItem> queryableItemGr = new List<PurchaseOrderItem>();

                    queryableHeaderGr = await Task.Run(() => _dbContext.PurchaseOrderHeaders.Where(x => x.Id == param.header.Id).FirstOrDefault());
                    queryableItemGr = await Task.Run(() => _dbContext.PurchaseOrderItems.Where(x => x.HeaderId == param.header.Id).ToList());

                    foreach (var item in queryableItemGr)
                    {
                        if (queryableHeaderGr.DocumentStatus == "PENDING" && item.StatusValue == "PENDING")
                        {

                            item.UpdateBy = param.header.UpdateBy;
                            item.UpdateDate = helper.GetDateTimeNow();
                            item.StatusValue = Constants.PURCHASE_ORDER_STATUS_CANCEL;

                            _dbContext.SaveChangesAsync();

                            resp.status = true;
                            resp.message = Constants.UPDATE_SUCCESS;
                        }
                        else
                        {
                            resp.status = false;
                            resp.message = Constants.UPDATE_DATA_INVALID;


                            return resp;
                        }
                    }

                    queryableHeaderGr.UpdateBy = param.header.UpdateBy;
                    queryableHeaderGr.UpdateDate = helper.GetDateTimeNow();
                    queryableHeaderGr.DocumentStatus = Constants.PURCHASE_ORDER_STATUS_CANCEL;

                    _dbContext.SaveChangesAsync();

                    resp.status = true;
                    resp.message = Constants.UPDATE_SUCCESS;
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

        public async Task<Response> UPDATE_PICKLIST(transactionPurchaseOrder param)
        {
            Response resp = new Response();

            try
            {
                PurchaseOrderHeader headerData = new PurchaseOrderHeader();

                List<PurchaseOrderItem> itemList = new List<PurchaseOrderItem>();

                if (param.header != null && param.item.Count > 0)
                {
                    var queryableHeader = await Task.Run(() => _dbContext.PurchaseOrderHeaders.AsQueryable());

                    var queryableItem = await Task.Run(() => _dbContext.PurchaseOrderItems.AsQueryable());

                    var queryableStock = await Task.Run(() => _dbContext.Stocks.AsQueryable());

                    var updateHeader = await Task.Run(() => queryableHeader.Where(x => x.Id == param.header.Id).FirstOrDefault());

                    foreach (var item in param.item)
                    {
                        if (headerData.DocumentStatus == Constants.PURCHASE_ORDER_STATUS_PENDING && item.StatusValue == Constants.PURCHASE_ORDER_STATUS_PENDING)
                        {
                            var updateItem = await Task.Run(() => queryableItem.Where(x => x.Id == item.Id).FirstOrDefault());

                            updateItem.StatusValue = Constants.PURCHASE_ORDER_STATUS_PICKLIST;

                            updateItem.UpdateDate = helper.GetDateTimeNow();
                            //updateItem.PicklistBy = item.PicklistBy;
                            //updateItem.PicklistDate = helper.GetDateTimeNow();

                            _dbContext.Update(updateItem);

                            _dbContext.SaveChanges();
                        }
                        else
                        {
                            resp.status = false;
                            resp.message = Constants.UPDATE_DATA_INVALID;

                            return resp;
                        }
                    }

                    var getItemAfterUpdate = await Task.Run(() => _dbContext.PurchaseOrderItems.Where(x => x.HeaderId == param.header.Id).AsQueryable());

                    itemList = getItemAfterUpdate.ToList();

                    int idxPicklist = 0;

                    foreach (var item in itemList)
                    {
                        if (item.StatusValue == Constants.PURCHASE_ORDER_STATUS_PENDING)
                        {
                            idxPicklist++;
                        }
                    }

                    if (idxPicklist == 0)
                    {
                        updateHeader.DocumentStatus = Constants.PURCHASE_ORDER_STATUS_PICKLIST;


                        _dbContext.Update(updateHeader);

                        _dbContext.SaveChanges();
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

        public async Task<Response> UPDATE_PACKING(transactionPurchaseOrder param)
        {
            Response resp = new Response();

            try
            {
                PurchaseOrderHeader headerData = new PurchaseOrderHeader();

                List<PurchaseOrderItem> itemList = new List<PurchaseOrderItem>();

                if (param.header != null && param.item.Count > 0)
                {
                    var queryableHeader = await Task.Run(() => _dbContext.PurchaseOrderHeaders.AsQueryable());

                    var queryableItem = await Task.Run(() => _dbContext.PurchaseOrderItems.AsQueryable());

                    var queryableStock = await Task.Run(() => _dbContext.Stocks.AsQueryable());

                    var updateHeader = await Task.Run(() => queryableHeader.Where(x => x.Id == param.header.Id).FirstOrDefault());

                    foreach (var item in param.item)
                    {
                        if (headerData.DocumentStatus == Constants.PURCHASE_ORDER_STATUS_PICKLIST && item.StatusValue == Constants.PURCHASE_ORDER_STATUS_PICKLIST)
                        {
                            var updateItem = await Task.Run(() => queryableItem.Where(x => x.Id == item.Id).FirstOrDefault());

                            updateItem.StatusValue = Constants.PURCHASE_ORDER_STATUS_PACKING;
                            updateItem.UpdateDate = helper.GetDateTimeNow();
                            //updateItem.PackingBy = item.PackingBy;
                            //updateItem.PackingDate = helper.GetDateTimeNow();

                            _dbContext.Update(updateItem);

                            _dbContext.SaveChanges();
                        }
                        else
                        {
                            resp.status = false;
                            resp.message = Constants.UPDATE_DATA_INVALID;

                            return resp;
                        }
                    }

                    var getItemAfterUpdate = await Task.Run(() => _dbContext.PurchaseOrderItems.Where(x => x.HeaderId == param.header.Id).AsQueryable());

                    itemList = getItemAfterUpdate.ToList();

                    int idxPicklist = 0;

                    foreach (var item in itemList)
                    {
                        if (item.StatusValue == Constants.PURCHASE_ORDER_STATUS_PICKLIST)
                        {
                            idxPicklist++;
                        }
                    }

                    if (idxPicklist == 0)
                    {
                        updateHeader.DocumentStatus = Constants.PURCHASE_ORDER_STATUS_PACKING;

                        _dbContext.Update(updateHeader);

                        _dbContext.SaveChanges();
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

        public async Task<Response> UPDATE_SHIPPED(transactionPurchaseOrder param)
        {
            Response resp = new Response();

            try
            {
                PurchaseOrderHeader headerData = new PurchaseOrderHeader();

                List<PurchaseOrderItem> itemList = new List<PurchaseOrderItem>();

                if (param.header != null && param.item.Count > 0)
                {
                    var queryableHeader = await Task.Run(() => _dbContext.PurchaseOrderHeaders.AsQueryable());

                    var queryableItem = await Task.Run(() => _dbContext.PurchaseOrderItems.AsQueryable());

                    var queryableStock = await Task.Run(() => _dbContext.Stocks.AsQueryable());

                    var updateHeader = await Task.Run(() => queryableHeader.Where(x => x.Id == param.header.Id).FirstOrDefault());

                    foreach (var item in param.item)
                    {
                        if (headerData.DocumentStatus == Constants.PURCHASE_ORDER_STATUS_PACKING && item.StatusValue == Constants.PURCHASE_ORDER_STATUS_PACKING)
                        {
                            var updateItem = await Task.Run(() => queryableItem.Where(x => x.Id == item.Id).FirstOrDefault());

                            updateItem.StatusValue = Constants.PURCHASE_ORDER_STATUS_SHIPPED;
                            updateItem.UpdateDate = helper.GetDateTimeNow();
                            //updateItem.ShippedBy = item.ShippedBy;
                            //updateItem.ShippedDate = helper.GetDateTimeNow();

                            _dbContext.Update(updateItem);

                            _dbContext.SaveChanges();
                        }
                        else
                        {
                            resp.status = false;
                            resp.message = Constants.UPDATE_DATA_INVALID;

                            return resp;
                        }
                    }

                    var getItemAfterUpdate = await Task.Run(() => _dbContext.PurchaseOrderItems.Where(x => x.HeaderId == param.header.Id).AsQueryable());

                    itemList = getItemAfterUpdate.ToList();

                    int idxPicklist = 0;

                    foreach (var item in itemList)
                    {
                        if (item.StatusValue == Constants.PURCHASE_ORDER_STATUS_PACKING)
                        {
                            idxPicklist++;
                        }
                    }

                    if (idxPicklist == 0)
                    {
                        updateHeader.DocumentStatus = Constants.PURCHASE_ORDER_STATUS_SHIPPED;


                        _dbContext.Update(updateHeader);

                        _dbContext.SaveChanges();
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
