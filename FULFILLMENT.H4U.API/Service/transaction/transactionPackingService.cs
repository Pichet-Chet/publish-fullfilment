using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;
using FULFILLMENT.H4U.API.Service.Helper;
using System.Globalization;

namespace FULFILLMENT.H4U.API.Service.transaction
{
    public class transactionPackingService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();

        public async Task<Response> INSERT(Packing param)
        {
            Response resp = new Response();

            PurchaseOrderHeader getDataPo = new PurchaseOrderHeader();
            PicklistHeader getData = new PicklistHeader();

            Packing packing = new Packing();

            string typeDocument = Constants.DOCUMENT_TYPE_PACKING;

            try
            {
                var queryablePo = await Task.Run(() => _dbContext.PurchaseOrderHeaders.AsQueryable());
                var queryable = await Task.Run(() => _dbContext.PicklistHeaders.AsQueryable());

                if (param.BinId != null)
                {
                    queryable = queryable.Where(x => x.BinId == param.BinId).AsQueryable();
                }

                getData = queryable.OrderByDescending(x => x.Id).FirstOrDefault();

                getDataPo = queryablePo.Where(x => x.Id == getData.PurchaseOrderHeaderId && x.DocumentStatus == Constants.PURCHASE_ORDER_STATUS_PICKLIST).FirstOrDefault();




                if (getData != null)
                {
                    if (getDataPo != null)
                    {
                        //-- Update PO
                        var updatePurchaseOrder = await Task.Run(() => _dbContext.PurchaseOrderHeaders.Where(x => x.Id == getData.PurchaseOrderHeaderId).FirstOrDefault());

                        updatePurchaseOrder.DocumentStatus = Constants.PURCHASE_ORDER_STATUS_PACKING;


                        //-- Adding Packing
                        packing.DocumentNo = await helper.GenerateDocumentNo(typeDocument, (int)updatePurchaseOrder.VendorId);

                        SysDocument sysDocument = new SysDocument();

                        sysDocument.Tpye = typeDocument;
                        sysDocument.VendorId = (int)updatePurchaseOrder.VendorId;
                        sysDocument.DocumentNo = packing.DocumentNo;
                        sysDocument.DocumentYear = helper.GetYear();
                        sysDocument.DocumentMonth = helper.GetMonth();
                        sysDocument.DocumentDay = helper.GetDay();
                        sysDocument.CreateDate = helper.GetDateTimeNow();



                        packing.DocumentStatus = Constants.PURCHASE_ORDER_STATUS_PACKING;
                        packing.PurchaseOrderHeaderId = getData.PurchaseOrderHeaderId;
                        packing.PicklistHeaderId = getData.Id;
                        packing.Remark = "";
                        packing.VendorId = (int)updatePurchaseOrder.VendorId;
                        packing.BinId = param.BinId;
                        packing.ShippingPrice = 0;
                        packing.CreateBy = param.CreateBy;
                        packing.UpdateBy = param.UpdateBy;
                        packing.CreateDate = helper.GetDateTimeNow();
                        packing.UpdateDate = helper.GetDateTimeNow();

                        await Task.Run(() => _dbContext.PurchaseOrderHeaders.Update(updatePurchaseOrder));
                        await Task.Run(() => _dbContext.Packings.Add(packing));
                        await Task.Run(() => _dbContext.SysDocuments.Add(sysDocument));


                        // -- Update Picklist

                        var updatePicklist = await Task.Run(() => _dbContext.PicklistHeaders.Where(x => x.Id == getData.Id).FirstOrDefault());

                        updatePicklist.DocumentStatus = Constants.PURCHASE_ORDER_STATUS_SHIPPED;

                        // -- end

                        await _dbContext.SaveChangesAsync();

                        resp.status = true;

                        resp.message = Constants.GET_DATA_SUCCESS;
                    }
                    else
                    {
                        resp.status = false;
                        resp.message = Constants.NOT_FOUND_PURCHASE_ORDER;
                        resp.output_data = null;
                        return resp;
                    }
                }
                else
                {
                    resp.status = false;
                    resp.message = Constants.NOT_FOUND_BIN_IN_PICKLIST_PROCESS;
                    resp.output_data = null;
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

        public async Task<Response> UPDATE(Packing param)
        {
            Response resp = new Response();

            PurchaseOrderHeader getDataPo = new PurchaseOrderHeader();

            string typeDocument = Constants.DOCUMENT_TYPE_PACKING;

            try
            {

                if (param != null)
                {
                    var checkDataPacking = await Task.Run(() => _dbContext.Packings.Where(x => x.Id == param.Id).FirstOrDefault());

                    if (checkDataPacking != null)
                    {
                        var queryablePo = await Task.Run(() => _dbContext.PurchaseOrderHeaders.AsQueryable());

                        getDataPo = queryablePo.Where(x => x.Id == checkDataPacking.PurchaseOrderHeaderId).FirstOrDefault();

                        if (getDataPo != null)
                        {
                            if (getDataPo.DocumentStatus == Constants.PURCHASE_ORDER_STATUS_PACKING)
                            {
                                var updatePurchaseOrder = await Task.Run(() => _dbContext.PurchaseOrderHeaders.Where(x => x.Id == getDataPo.Id).FirstOrDefault());


                                if (updatePurchaseOrder != null)
                                {
                                    // -- Update PO
                                    updatePurchaseOrder.DocumentStatus = Constants.PURCHASE_ORDER_STATUS_SHIPPED;
                                    //updatePurchaseOrder.TrackingNumber = param.TrackingNumber;
                                    // --


                                    //-- Update Packing

                                    var updatePacking = await Task.Run(() => _dbContext.Packings.Where(x => x.Id == param.Id).FirstOrDefault());
                                    updatePacking.BoxId = param.BoxId;
                                    updatePacking.DocumentStatus = Constants.PURCHASE_ORDER_STATUS_SHIPPED;
                                    updatePacking.Remark = param.Remark;
                                    updatePacking.TrackingNumber = param.TrackingNumber;
                                    updatePacking.ShippingPrice = param.ShippingPrice;
                                    updatePacking.UpdateDate = helper.GetDateTimeNow();

                                    //-- End


                                    //-- Update bin

                                    var getDataMasterBin = await Task.Run(() => _dbContext.MasterBins.Where(x => x.BinId == updatePacking.BinId).FirstOrDefault());

                                    getDataMasterBin.PicklistId = null;

                                    // -- end

                                    // -- Update Picklist

                                    var updatePicklist = await Task.Run(() => _dbContext.PicklistHeaders.Where(x => x.Id == updatePacking.PicklistHeaderId).FirstOrDefault());

                                    updatePicklist.DocumentStatus = Constants.PURCHASE_ORDER_STATUS_SHIPPED;

                                    // -- end

                                    await Task.Run(() => _dbContext.PurchaseOrderHeaders.Update(updatePurchaseOrder));
                                    await Task.Run(() => _dbContext.Packings.Update(updatePacking));

                                    await _dbContext.SaveChangesAsync();

                                    resp.status = true;

                                    resp.message = Constants.GET_DATA_SUCCESS;
                                }
                                else
                                {
                                    resp.status = false;
                                    resp.message = Constants.DATA_NOT_FOUND;
                                    resp.output_data = null;
                                    return resp;
                                }
                            }

                            else
                            {
                                resp.status = false;
                                resp.message = Constants.PURCHASE_ORDER_INFORMATION_IS_SHIPPED;
                                resp.output_data = null;
                                return resp;
                            }
                            //-- Update PO

                        }
                        else
                        {
                            resp.status = false;
                            resp.message = Constants.DATA_NOT_FOUND;
                            resp.output_data = null;
                            return resp;
                        }
                    }


                }
                else
                {
                    resp.status = false;
                    resp.message = Constants.DATA_NOT_FOUND;
                    resp.output_data = null;
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
