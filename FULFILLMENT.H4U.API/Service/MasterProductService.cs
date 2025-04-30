using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Reponse;
using System.Diagnostics;
using FULFILLMENT.H4U.API.Service.Helper;
using System.ComponentModel.DataAnnotations;
using FULFILLMENT.H4U.API.Model.Custom;

namespace FULFILLMENT.H4U.API.Service
{
    public class MasterProductService
    {
        readonly DataContext _dbContext = new();

        HelperService helper = new HelperService();

        public async Task<Response> GET_ALL()
        {
            Response resp = new Response();

            try
            {
                var getData = await Task.Run(() => _dbContext.MasterProducts.ToList());

                if (getData.Count > 0)
                {
                    foreach (var item in getData)
                    {
                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));

                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                        item.CreateBy = getUserCrete;

                        item.UpdateBy = getUserUpdate;
                    }

                    resp.status = true;

                    resp.message = Constants.GET_DATA_SUCCESS;

                    resp.output_data = getData;
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

        public async Task<Response> GET_ACTIVE()
        {
            Response resp = new Response();

            try
            {
                var getData = await Task.Run(() => _dbContext.MasterProducts.Where(x => x.IsActive == true).ToList());

                if (getData.Count > 0)
                {
                    foreach (var item in getData)
                    {
                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));

                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                        item.CreateBy = getUserCrete;

                        item.UpdateBy = getUserUpdate;
                    }

                    resp.status = true;

                    resp.message = Constants.GET_DATA_SUCCESS;

                    resp.output_data = getData;
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

        public async Task<Response> GET_ALL_BY_VENDOR(int vendorId)
        {
            Response resp = new Response();

            try
            {
                var getData = await Task.Run(() => _dbContext.MasterProducts.Where(x => x.VendorId == vendorId).ToList());

                if (getData.Count > 0)
                {
                    foreach (var item in getData)
                    {
                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));

                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                        item.CreateBy = getUserCrete;

                        item.UpdateBy = getUserUpdate;
                    }

                    resp.status = true;

                    resp.message = Constants.GET_DATA_SUCCESS;

                    resp.output_data = getData;
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

        public async Task<Response> GET_FILTER(FilterModel param)
        {
            Response resp = new Response();

            List<ViewMasterProductModel> viewData = new List<ViewMasterProductModel>();



            try
            {
                var queryable = await Task.Run(() => _dbContext.MasterProducts.AsQueryable());

                if (!string.IsNullOrEmpty(param.name))
                {
                    queryable = queryable.Where(x => param.name.Contains(x.ProductName)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.description))
                {
                    queryable = queryable.Where(x => param.description.Contains(x.ProductDescription)).AsQueryable();
                }

                if (param.vendorId != null)
                {
                    queryable = queryable.Where(x => x.VendorId == param.vendorId).AsQueryable();
                }
          
                if (param.isActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.isActive).AsQueryable();
                }

                var getData = queryable.ToList();

                if (getData.Count > 0)
                {
                    var queryableVendorMaster = _dbContext.MasterVendors.AsQueryable();
                    var queryableProductYpe = _dbContext.MasterProductsTypes.AsQueryable();

                    foreach (var item in getData)
                    {
                        ViewMasterProductModel viewModel = new ViewMasterProductModel();

                        var DataVendor = queryableVendorMaster.Where(x => x.VendorId == item.VendorId).FirstOrDefault();
                        var DataProductType = queryableProductYpe.Where(x => x.Id == item.ProductTypeId).FirstOrDefault();

                        var getUserCrete = helper.GetUserId(Convert.ToInt32(item.CreateBy));
                        var getUserUpdate = helper.GetUserId(Convert.ToInt32(item.UpdateBy));

                        viewModel.CreateBy = getUserCrete;
                        viewModel.UpdateBy = getUserUpdate;
                        viewModel.CreateDate = item.CreateDate;
                        viewModel.UpdateDate = item.UpdateDate;

                        viewModel.ProductId = item.ProductId;
                        
                        viewModel.ProductSku = item.ProductSku;
                        viewModel.ProductName = item.ProductName;
                        viewModel.ProductDescription = item.ProductDescription;
                        viewModel.ProductColor = item.ProductColor;
                        viewModel.ProductHeight = item.ProductHeight;
                        viewModel.ProductWidth = item.ProductWidth;
                        viewModel.ProductDimension = item.ProductDimension;
                        viewModel.ProductWeight = item.ProductWeight;
                        viewModel.UnitOfDimension = item.UnitOfDimension;
                        viewModel.UnitOfWeight = item.UnitOfWeight;
                        viewModel.IsActive = item.IsActive;
                        viewModel.FileLocation = item.FileLocation;
                        viewModel.ProductLength = item.ProductLength;
                        viewModel.VendorSku = item.VendorSku;

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


                        viewModel.ProductTypeName = DataProductType.Value;
                        

                        viewData.Add(viewModel);
                    }

                    resp.status = true;

                    resp.message = Constants.GET_DATA_SUCCESS;

                    resp.output_data = viewData.OrderBy(x=>x.ProductSku);
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

            try
            {
                var getData = await Task.Run(() => _dbContext.MasterProducts.Where(x => x.ProductId == id).FirstOrDefault());

                if (getData != null)
                {

                    var getUserCrete = helper.GetUserId(Convert.ToInt32(getData.CreateBy));

                    var getUserUpdate = helper.GetUserId(Convert.ToInt32(getData.UpdateBy));

                    getData.CreateBy = getUserCrete;

                    getData.UpdateBy = getUserUpdate;


                    resp.status = true;

                    resp.message = Constants.GET_DATA_SUCCESS;

                    resp.output_data = getData;
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

        public async Task<Response> SEARCH(string textSearch)
        {
            Response resp = new Response();

            List<MasterProduct> listData = new List<MasterProduct>();

            try
            {
                if (!string.IsNullOrEmpty(textSearch))
                {
                    listData = await Task.Run(() => _dbContext.MasterProducts.Where(x =>
                    (x.ProductId + x.ProductSku + x.ProductWidth + x.ProductWeight + x.ProductDescription + x.ProductName + x.ProductDimension + x.CreateBy + x.UpdateBy)
                    .Contains(textSearch)).ToList());
                }
                else
                {
                    listData = await Task.Run(() => _dbContext.MasterProducts.ToList());
                }

                if (listData != null)
                {
                    resp.status = true;

                    resp.message = Constants.GET_DATA_SUCCESS;

                    resp.output_data = listData;
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

        public async Task<Response> INSERT(MasterProduct param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {

                using (_dbContext)
                {
                    if (string.IsNullOrEmpty(param.ProductSku))
                    {
                        param.ProductSku = param.VendorSku;
                    }

                    var checkDuplication = _dbContext.MasterProducts.Where(x => x.VendorSku == param.VendorSku).FirstOrDefault();

                    if (checkDuplication != null)
                    {
                        validate = false;

                        resp.message = Constants.INSERT_DUPLICATE;

                        return resp;
                    }

                    if (param.ProductWidth <= 0 || param.ProductHeight <= 0)
                    {
                        validate = false;

                        resp.message = Constants.FILL_OUT_THE_REQUIRE;

                        return resp;
                    }



                    if (validate == true)
                    {
                        

                        param.ProductDimension = param.ProductWidth + "x" + param.ProductHeight;

                        param.CreateDate = helper.GetDateTimeNow();

                        param.UpdateDate = helper.GetDateTimeNow();

                        await Task.Run(() => _dbContext.MasterProducts.Add(param));

                        _dbContext.SaveChanges();

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

        public async Task<Response> UPDATE(MasterProduct param)
        {
            Response resp = new Response();

            bool validate = true;

            try
            {
                using (_dbContext)
                {

                    var checkDuplication = _dbContext.MasterProducts.Where(x => x.ProductSku == param.ProductSku && x.ProductId != param.ProductId).FirstOrDefault();

                    if (checkDuplication != null || param.ProductWidth <= 0 || param.ProductHeight <= 0)
                    {
                        validate = false;

                        resp.message = Constants.UPDATE_DUPLICATE;
                    }


                    if (validate == true)
                    {
                        var update = await Task.Run(() => _dbContext.MasterProducts.Where(x => x.ProductId == param.ProductId).FirstOrDefault());

                        if (update != null)
                        {
                            update.ProductName = param.ProductName;
                            update.ProductDescription = param.ProductDescription;
                            update.ProductColor = param.ProductColor;
                            update.ProductHeight = param.ProductHeight;
                            update.ProductWidth = param.ProductWidth;
                            update.ProductDimension = param.ProductWidth + "x" + param.ProductHeight;
                            update.ProductWeight = param.ProductWeight;
                            update.ProductLength = param.ProductLength;
                            update.UnitOfDimension = param.UnitOfDimension;
                            update.UnitOfWeight = param.UnitOfWeight;
                            //update.CreateBy = param.CreateBy;
                            //update.CreateDate = param.CreateDate;
                            update.UpdateBy = param.UpdateBy;
                            update.UpdateDate = helper.GetDateTimeNow();
                            update.IsActive = param.IsActive;
                            update.ProductTypeId = param.ProductTypeId;


                            if (!string.IsNullOrEmpty(param.FileLocation))
                            {
                                update.FileLocation = param.FileLocation;
                            }

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

        public async Task<Response> DELETE(int id)
        {
            Response resp = new Response();

            try
            {
                using (_dbContext)
                {
                    var delete = await Task.Run(() => _dbContext.MasterProducts.Find(id));

                    if (delete != null)
                    {
                        _dbContext.MasterProducts.Remove(delete);

                        _dbContext.SaveChanges();

                        resp.status = true;
                        resp.message = Constants.DELETE_SUCCESS;
                    }
                    else
                    {
                        resp.status = false;
                        resp.message = Constants.DELETE_ERROR;
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
