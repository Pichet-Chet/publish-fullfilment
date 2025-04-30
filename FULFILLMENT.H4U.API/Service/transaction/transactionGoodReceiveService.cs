using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;
using FULFILLMENT.H4U.API.Service.Helper;
using System.Globalization;
using System.Net.WebSockets;

namespace FULFILLMENT.H4U.API.Service.transaction
{
    public class transactionGoodReceiveService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();

        public async Task<Response> INSERT(transactionGoodReceive param)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

            Response resp = new Response();

            List<GoodReceiveItem> listItem = new List<GoodReceiveItem>();

            List<Stock> listStock = new List<Stock>();

            bool validate = true;

            string typeDocument = Constants.DOCUMENT_TYPE_GOOD_RECEIVE;
            string lotDocument = Constants.DOCUMENT_TYPE_LOT;

            try
            {
                //Begin - Validate

                if (param.header != null &&
                    param.item.Count > 0 &&
                    //param.header.ReceiveDate != null &&
                    param.header.InboundHeaderId != null &&
                    param.header.VendorId != null)
                {
                    validate = true;

                    foreach (var item in param.item)
                    {
                        var checkProductMasterVendor = await Task.Run(() => _dbContext.MasterProducts.Where(x => x.VendorSku == item.ProductSku).FirstOrDefault());

                        param.header.VendorId = checkProductMasterVendor.VendorId;

                        if (item.Amount <= 0 || item.Amount == null)
                        {
                            resp.status = false;
                            resp.message = Constants.AMOUNT_IS_REQUIRE;

                            return resp;
                        }
                    }
                }
                else
                {
                    resp.status = false;
                    resp.message = Constants.DATA_FORM_ERROR;

                    return resp;
                }

                //End - Validate 

                if (validate == true)
                {

                    var checkDataInboundHeader = await Task.Run(() => _dbContext.InboundHeaders.Where(x => x.Id == param.header.InboundHeaderId && x.ArrivedDate != null).FirstOrDefault());

                    var checkDataVendor = await Task.Run(() => _dbContext.MasterVendors.Where(x => x.VendorId == param.header.VendorId).FirstOrDefault());

                    if (checkDataVendor == null)
                    {
                        validate = false;
                        resp.message = Constants.INSERT_DATA_INVALID;
                    }

                    if (checkDataInboundHeader == null)
                    {
                        validate = false;
                        resp.message = Constants.INSERT_DATA_INVALID;
                    }

                    if (validate == true)
                    {

                        param.header.DocumentNo = await helper.GenerateDocumentNo(typeDocument, (int)param.header.VendorId);

                        param.header.CreateDate = param.header.CreateDate == null ? helper.GetDateTimeNow() : param.header.CreateDate;
                        param.header.UpdateDate = helper.GetDateTimeNow();
                        param.header.ReceiveStatus = Constants.GOOD_RECEIVE_STATUS_PENDING;



                        // [Begin] Generate Document ===============
                        SysDocument sysDocument = new SysDocument();

                        sysDocument.Tpye = Constants.DOCUMENT_TYPE_GOOD_RECEIVE;
                        sysDocument.VendorId = param.header.VendorId;
                        sysDocument.DocumentNo = param.header.DocumentNo;
                        sysDocument.DocumentYear = helper.GetYear();
                        sysDocument.DocumentMonth = helper.GetMonth();
                        sysDocument.DocumentDay = helper.GetDay();
                        sysDocument.CreateDate = helper.GetDateTimeNow();

                        // [End] Generate Document ===============

                        var checkOldDataGrHeader = await Task.Run(() => _dbContext.GoodReceiveHeaders.Where(x => x.InboundHeaderId == param.header.InboundHeaderId).FirstOrDefault());

                        if (checkOldDataGrHeader == null)
                        {
                            await Task.Run(() => _dbContext.GoodReceiveHeaders.Add(param.header));
                            await Task.Run(() => _dbContext.SysDocuments.Add(sysDocument));
                        }
                        else
                        {
                            param.header.Id = checkOldDataGrHeader.Id;
                        }


                        await _dbContext.SaveChangesAsync();


                        var getDataAmountOfInbound = await Task.Run(() => _dbContext.InboundItems.AsQueryable());
                        var getMasterProduct = await Task.Run(() => _dbContext.MasterProducts.AsQueryable());
                        var getMasterLocation = await Task.Run(() => _dbContext.MasterLocations.AsQueryable());
                        var getMasterSheft = await Task.Run(() => _dbContext.MasterShefts.AsQueryable());
                        var getMastesBin = await Task.Run(() => _dbContext.MasterBins.AsQueryable());
                        var getTransactionGrHeader = await Task.Run(() => _dbContext.GoodReceiveHeaders.AsQueryable());
                        var getTransactionGrItem = await Task.Run(() => _dbContext.GoodReceiveItems.AsQueryable());
                        var getDataStock = await Task.Run(() => _dbContext.Stocks.AsQueryable());

                        var getMaxSeqItem = await Task.Run(() => _dbContext.GoodReceiveItems.Where(x => x.HeaderId == param.header.Id).Max(x => x.Seq));

                        if (getMaxSeqItem == null)
                        {
                            getMaxSeqItem = 0;
                        }

                        getMaxSeqItem++;



                        foreach (var item in param.item)
                        {
                            var findProductMaster = new MasterProduct();
                            if (item.ReceiveStatus != "CANCEL")
                            {
                                findProductMaster = getMasterProduct.Where(x => x.ProductSku == item.ProductSku).FirstOrDefault();

                                if (findProductMaster == null)
                                {
                                    validate = false;

                                    resp.status = false;
                                    resp.message = "ไม่พบรายการสินค้าที่ทำรายการ";

                                    return resp;
                                }
                            }

                            item.Id = 0;
                            item.HeaderId = param.header.Id;
                            item.ReceiveStatus = item.ReceiveStatus;
                            item.CreateDate = item.CreateDate == null ? helper.GetDateTimeNow() : item.CreateDate;
                            item.UpdateDate = helper.GetDateTimeNow();
                            item.ProductId = item.ReceiveStatus == "CANCEL" ? item.ProductId : findProductMaster.ProductId;
                            item.Lot = await helper.GenerateLotNo(lotDocument, (int)getMaxSeqItem, (int)item.ProductId, (int)param.header.VendorId);
                            item.Seq = getMaxSeqItem;

                            var getAmountInbound = getDataAmountOfInbound.Where(x => x.HeaderId == param.header.InboundHeaderId && x.ProductId == item.ProductId).Sum(x => x.Amount);

                            if (getAmountInbound >= 1)
                            {
                                var getAmontGr = await Task.Run(() => getTransactionGrItem.Where(x => x.HeaderId == param.header.Id && x.ProductId == item.ProductId).Sum(x => x.Amount));

                                var checkDataProduct = await Task.Run(() => getMasterProduct.Where(x => x.ProductId == item.ProductId && x.VendorId == param.header.VendorId).FirstOrDefault());

                                var checkDataLocation = await Task.Run(() => getMasterLocation.Where(x => x.LocationId == item.LocationId).FirstOrDefault());

                                var checkDataSheft = await Task.Run(() => getMasterSheft.Where(x => x.SheftId == item.SheftId).FirstOrDefault());

                                var checkDataBin = await Task.Run(() => getMastesBin.Where(x => x.BinId == item.BinId).FirstOrDefault());



                                if (getAmontGr >= getAmountInbound)
                                {
                                    validate = false;

                                    resp.status = false;
                                    resp.message = Constants.ITEM_GR_IS_ALREADY;

                                    return resp;
                                }

                                if (getAmontGr + item.Amount > getAmountInbound)
                                {
                                    validate = false;

                                    resp.status = false;
                                    resp.message = Constants.AMOUNT_FOR_GR_MORE_THAN_AMOUNT_OF_ITEM_INBOUND;

                                    return resp;
                                }

                                if (checkDataProduct == null)
                                {
                                    if (item.ReceiveStatus == Constants.GOOD_RECEIVE_STATUS_RECEIVED)
                                    {
                                        resp.status = false;
                                        resp.message = Constants.NOT_FOUND_PRODUCT_WITH_IN_YOUR_MASTER_PRODUCT;

                                        return resp;
                                    }
                                }

                                if (checkDataLocation == null)
                                {
                                    if (item.ReceiveStatus == Constants.GOOD_RECEIVE_STATUS_RECEIVED)
                                    {
                                        resp.status = false;
                                        resp.message = Constants.NOT_FOUND_PRODUCT_WITH_IN_YOUR_MASTER_PRODUCT;

                                        return resp;
                                    }
                                }

                                //if (checkDataSheft == null)
                                //{
                                //    if (item.ReceiveStatus == Constants.GOOD_RECEIVE_STATUS_RECEIVED)
                                //    {
                                //        if (item.ReceiveStatus.Trim() != Constants.GOOD_RECEIVE_STATUS_CANCEL.Trim())
                                //        {
                                //            resp.status = false;
                                //            resp.message = Constants.NOT_FOUND_PRODUCT_WITH_IN_YOUR_MASTER_PRODUCT;

                                //            return resp;
                                //        }
                                //    }
                                //}

                                if (item.Amount <= 0)
                                {
                                    validate = false;

                                    resp.status = false;
                                    resp.message = Constants.AMOUNT_FOR_GR_IS_REQUIRE;

                                    return resp;
                                }

                                if (item.ReceiveStatus == Constants.GOOD_RECEIVE_STATUS_RECEIVED)
                                {
                                    if (checkDataProduct.ProductWidth >= Constants.CONDITION_WITH_PRODUCT)
                                    {
                                        validate = true;
                                    }

                                    else
                                    {
                                        if (checkDataBin == null)
                                        {
                                            validate = false;

                                            resp.status = false;
                                            resp.message = Constants.INSERT_DATA_INVALID;

                                            return resp;
                                        }
                                        else
                                        {
                                            validate = true;
                                        }
                                    }
                                }

                                if (validate == true)
                                {
                                    // Update Data bin

                                    if (checkDataBin != null)
                                    {
                                        checkDataBin.SheftId = item.SheftId;
                                    }


                                    listItem.Add(item);
                                }
                            }

                            else
                            {
                                validate = false;

                                resp.message = Constants.INSERT_DATA_INVALID;

                            }
                        }

                        if (validate == true)
                        {
                            if (listItem.Count > 0)
                            {
                                if (param.header.ReceiveStatus == Constants.GOOD_RECEIVE_STATUS_PENDING)
                                {
                                    foreach (var item in listItem)
                                    {
                                        if (item.ReceiveStatus == Constants.GOOD_RECEIVE_STATUS_PENDING)
                                        {
                                            item.ReceiveStatus = Constants.GOOD_RECEIVE_STATUS_RECEIVED;

                                            Stock stock = new Stock();

                                            var checkOldStock = getDataStock.Where(x => x.ProductId == item.ProductId && x.LocationId == item.LocationId && x.SheftId == item.SheftId && x.BinId == item.BinId && x.VendorId == param.header.VendorId).FirstOrDefault();

                                            if (checkOldStock == null)
                                            {
                                                stock.ProductId = item.ProductId;
                                                stock.VendorId = param.header.VendorId;
                                                stock.Amount = item.Amount;
                                                stock.LocationId = item.LocationId;
                                                stock.SheftId = item.SheftId;
                                                stock.BinId = item.BinId;
                                                stock.Status = "ACTIVE";

                                                stock.CreateBy = param.header.CreateBy;
                                                stock.CreateDate = stock.CreateDate == null ? helper.GetDateTimeNow() : stock.CreateDate;
                                                stock.UpdateBy = param.header.CreateBy;
                                                stock.UpdateDate = helper.GetDateTimeNow();
                                                stock.IsActive = true;

                                                //listStock.Add(stock);

                                                //_dbContext.Add(stock);

                                                //_dbContext.SaveChanges();
                                            }
                                            else
                                            {
                                                checkOldStock.Amount = checkOldStock.Amount + item.Amount;

                                                //_dbContext.Update(checkOldStock);

                                                //_dbContext.SaveChanges();
                                            }

                                        }
                                    }
                                }

                                //await Task.Run(() => _dbContext.Stocks.AddRangeAsync(listStock));

                                await Task.Run(() => _dbContext.GoodReceiveItems.AddRangeAsync(listItem));

                                await _dbContext.SaveChangesAsync();

                                await TICKER_UPDATE();

                                resp.status = true;

                                resp.message = Constants.INSERT_SUCCESS;

                            }
                            else
                            {
                                resp.status = false;

                                resp.message = Constants.INSERT_ERROR;
                            }


                        }

                        else
                        {
                            resp.status = false;

                            resp.message = Constants.INSERT_ERROR;
                        }
                    }
                    else
                    {
                        resp.status = false;

                        resp.message = Constants.INSERT_DATA_INVALID;
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

        public async Task<Response> UPDATE(transactionGoodReceive param)
        {
            Response resp = new Response();

            GoodReceiveItem dataModel = new GoodReceiveItem();

            try
            {
                if (param.item.Count > 0)
                {
                    var getDataStock = await Task.Run(() => _dbContext.Stocks.AsQueryable());

                    foreach (var item in param.item)
                    {
                        var getReceiveItem = await Task.Run(() => _dbContext.GoodReceiveItems.Where(x => x.Id == item.Id).FirstOrDefault());

                        var checkProductMasterVendor = await Task.Run(() => _dbContext.MasterProducts.Where(x => x.ProductSku == getReceiveItem.ProductSku).FirstOrDefault());

                        param.header.VendorId = checkProductMasterVendor.VendorId;

                        dataModel = await Task.Run(() => _dbContext.GoodReceiveItems.Where(x => x.Id == item.Id).FirstOrDefault());

                        if (dataModel != null)
                        {

                            dataModel.SheftId = item.SheftId;
                            dataModel.Remark = item.Remark == null ? dataModel.Remark : item.Remark;
                            dataModel.UpdateBy = item.UpdateBy;
                            dataModel.UpdateDate = helper.GetDateTimeNow();

                            await _dbContext.SaveChangesAsync();


                            if (dataModel.ReceiveStatus == Constants.GOOD_RECEIVE_STATUS_RECEIVED)
                            {

                                Stock stock = new Stock();

                                dataModel.BinId = dataModel.BinId == null ? 0 : dataModel.BinId;

                                var checkOldStock = getDataStock.Where(x => x.ProductId == dataModel.ProductId && x.LocationId == dataModel.LocationId && x.SheftId == item.SheftId && x.BinId == dataModel.BinId && x.VendorId == param.header.VendorId).FirstOrDefault();

                                if (checkOldStock != null)
                                {
                                    checkOldStock.Amount = checkOldStock.Amount + dataModel.Amount;

                                    await _dbContext.SaveChangesAsync();

                                    resp.status = true;
                                    resp.message = Constants.UPDATE_SUCCESS;
                                }
                                else
                                {
                                    //New Stock

                                    stock.ProductId = dataModel.ProductId;
                                    stock.Amount = dataModel.Amount;
                                    stock.LocationId = dataModel.LocationId;
                                    stock.SheftId = item.SheftId;
                                    stock.BinId = dataModel.BinId == null ? 0 : dataModel.BinId;
                                    stock.Status = "ACTIVE";
                                    stock.CreateBy = param.header.CreateBy;
                                    stock.UpdateBy = param.header.UpdateBy;
                                    stock.CreateDate = helper.GetDateTimeNow();
                                    stock.UpdateDate = helper.GetDateTimeNow();
                                    stock.IsActive = true;
                                    stock.VendorId = param.header.VendorId;

                                    await _dbContext.Stocks.AddAsync(stock);
                                    await _dbContext.SaveChangesAsync();

                                    resp.status = true;
                                    resp.message = Constants.UPDATE_SUCCESS;
                                }
                            }
                        }

                    }

                }
                else
                {
                    resp.status = false;
                    resp.message = Constants.DATA_FORM_ERROR;
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

        public async Task<Response> TICKER_UPDATE()
        {
            Response resp = new Response();

            List<InboundHeader> getDataInboundHeader = new List<InboundHeader>();

            List<InboundItem> getDataInboundItem = new List<InboundItem>();

            GoodReceiveHeader goodReceiveHeader = new GoodReceiveHeader();

            List<GoodReceiveItem> goodReceiveItems = new List<GoodReceiveItem>();

            var updateHeader = true;

            try
            {
                getDataInboundHeader = await Task.Run(() => _dbContext.InboundHeaders.ToList());

                if (getDataInboundHeader != null && getDataInboundHeader.Count > 0)
                {
                    foreach (var item in getDataInboundHeader)
                    {
                        //-- Lookup data Inbound Item

                        int InboundItemSum = 0;
                        int GrItemSum = 0;


                        getDataInboundItem = await Task.Run(() => _dbContext.InboundItems.Where(x => x.HeaderId == item.Id).ToList());

                        InboundItemSum = getDataInboundItem.Sum(x => x.Amount).GetValueOrDefault();

                        //-- Lookup data GR Item

                        goodReceiveHeader = await Task.Run(() => _dbContext.GoodReceiveHeaders.Where(x => x.InboundHeaderId == item.Id).FirstOrDefault());

                        if (goodReceiveHeader != null)
                        {
                            goodReceiveItems = await Task.Run(() => _dbContext.GoodReceiveItems.Where(x => x.HeaderId == goodReceiveHeader.Id).ToList());

                            foreach (var grItem in goodReceiveItems)
                            {
                                if (grItem.SheftId == null)
                                {
                                    updateHeader = false;
                                }
                            }

                            GrItemSum = goodReceiveItems.Sum(x => x.Amount).GetValueOrDefault();

                            if (InboundItemSum == GrItemSum)
                            {
                                var updateInboundHeader = await Task.Run(() => _dbContext.InboundHeaders.Where(x => x.Id == item.Id).FirstOrDefault());
                                updateInboundHeader.DocumentStatus = Constants.INBOUND_STATUS_RECEIVED;

                                var updateInboundItem = await Task.Run(() => _dbContext.InboundItems.Where(x => x.HeaderId == item.Id).ToList());
                                updateInboundItem.ForEach(x => x.ItemStatus = Constants.INBOUND_STATUS_RECEIVED);

                                if (updateHeader == true)
                                {
                                    goodReceiveHeader.ReceiveDate = goodReceiveHeader.ReceiveDate == null ? helper.GetDateTimeNow() : goodReceiveHeader.ReceiveDate;
                                    goodReceiveHeader.ReceiveStatus = Constants.GOOD_RECEIVE_STATUS_SUCCESS;
                                }

                                await Task.Run(() => _dbContext.GoodReceiveHeaders.Update(goodReceiveHeader));
                                await Task.Run(() => _dbContext.InboundHeaders.Update(updateInboundHeader));
                                await Task.Run(() => _dbContext.InboundItems.UpdateRange(updateInboundItem));

                                _dbContext.SaveChangesAsync();
                            }

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


    }
}
