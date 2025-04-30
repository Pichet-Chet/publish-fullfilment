using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;
using FULFILLMENT.H4U.API.Service.Helper;

namespace FULFILLMENT.H4U.API.Service.transaction
{
    public class transactionInboundService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();

        public async Task<Response> INSERT(transactionInbound param)
        {
            Response resp = new Response();

            List<InboundItem> listItem = new List<InboundItem>();

            bool validate = true;

            string typeDocument = Constants.DOCUMENT_TYPE_INBOUND;

            try
            {
                //Begin - Validate 

                if (param.header != null && param.item.Count > 0)
                {
                    validate = true;

                    foreach (var item in param.item)
                    {
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
                }

                //End - Validate 


                if (validate == true)
                {

                    var setVendorId = param.header.VendorId;

                    var checkDataVendor = _dbContext.MasterVendors.Where(x => x.VendorId == setVendorId).FirstOrDefault();

                    if (checkDataVendor == null)
                    {
                        validate = false;
                        resp.message = Constants.INSERT_DATA_INVALID;
                    }


                    if (validate == true)
                    {

                        param.header.DocumentNo = await helper.GenerateDocumentNo(typeDocument, (int)param.header.VendorId);

                        SysDocument sysDocument = new SysDocument();

                        sysDocument.Tpye = Constants.DOCUMENT_TYPE_INBOUND;
                        sysDocument.VendorId = param.header.VendorId;
                        sysDocument.DocumentNo = param.header.DocumentNo;
                        sysDocument.DocumentYear = helper.GetYear();
                        sysDocument.DocumentMonth = helper.GetMonth();
                        sysDocument.DocumentDay = helper.GetDay();
                        sysDocument.CreateDate = helper.GetDateTimeNow();

                        await Task.Run(() => _dbContext.SysDocuments.Add(sysDocument));

                        param.header.DocumentStatus = Constants.INBOUND_STATUS_SHIPPING;

                        param.header.CreateDate = helper.GetDateTimeNow();
                        param.header.UpdateDate = helper.GetDateTimeNow();


                        await Task.Run(() => _dbContext.InboundHeaders.Add(param.header));

                        _dbContext.SaveChangesAsync();

                        foreach (var item in param.item)
                        {
                            item.HeaderId = param.header.Id;
                            item.CreateDate = helper.GetDateTimeNow();
                            item.UpdateBy = item.CreateBy;
                            item.UpdateDate = helper.GetDateTimeNow();
                            item.ItemStatus = Constants.INBOUND_STATUS_SHIPPING;

                            listItem.Add(item);
                        }

                        await Task.Run(() => _dbContext.InboundItems.AddRangeAsync(listItem));

                        _dbContext.SaveChangesAsync();


                        resp.status = true;

                        resp.message = Constants.INSERT_SUCCESS;
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
