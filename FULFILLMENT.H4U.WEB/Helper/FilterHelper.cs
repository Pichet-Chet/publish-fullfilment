using FULFILLMENT.H4U.API.Model.Custom;

namespace FULFILLMENT_H4U.Helper
{
    public class FilterHelper
    {

        public string filter(FilterModel filter)
        {
            List<string> filters = new List<string>();

            string result = "";

            if (!string.IsNullOrEmpty(filter.documentNo))
            {
                result = "documentNo=" + filter.documentNo;

                filters.Add(result);
            }

            if (!string.IsNullOrEmpty(filter.purchaseOrderNo))
            {
                result = "purchaseOrderNo=" + filter.purchaseOrderNo;

                filters.Add(result);
            }

            if (!string.IsNullOrEmpty(filter.binType))
            {
                result = "binType=" + filter.binType;

                filters.Add(result);
            }

            if (!string.IsNullOrEmpty(filter.documentStatus))
            {
                result = "documentStatus=" + filter.documentStatus;

                filters.Add(result);
            }


            if (filter.vendorId != null)
            {
                result = "vendorId=" + filter.vendorId;

                filters.Add(result);
            }
            if (filter.productId != null)
            {
                result = "productId=" + filter.productId;

                filters.Add(result);
            }
            if (filter.locationId != null)
            {
                result = "locationId=" + filter.locationId;

                filters.Add(result);
            }
            if (filter.sheftId != null)
            {
                result = "sheftId=" + filter.sheftId;

                filters.Add(result);
            }
            if (filter.roleGroupId != null)
            {
                result = "roleGroupId=" + filter.roleGroupId;

                filters.Add(result);
            }
            if (filter.isActive != null)
            {
                result = "isActive=" + filter.isActive;

                filters.Add(result);
            }

            if (!string.IsNullOrEmpty(filter.name))
            {
                result = "name=" + filter.name;

                filters.Add(result);
            }
            if (!string.IsNullOrEmpty(filter.code))
            {
                result = "code=" + filter.code;
                filters.Add(result);
            }
            if (!string.IsNullOrEmpty(filter.description))
            {
                result = "description=" + filter.description;
                filters.Add(result);
            }

            if (filters.Count > 0)
            {
                result = String.Join("&", filters);
            }


            return result;


        }

    }
}
