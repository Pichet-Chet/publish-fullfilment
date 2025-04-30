using FULFILLMENT.H4U.API.Model;

namespace FULFILLMENT.H4U.API.Model.Custom
{
    public class ViewStockModel : Stock
    {
        #region Vendor Master

        public string? VendorCode { get; set; }

        public string? PrefixName { get; set; }

        public string? VendorName { get; set; }

        public string? TaxId { get; set; }

        public string? ContactName { get; set; }

        public string? ContactTel { get; set; }

        public string? ContactAddress { get; set; }

        public string? LineId { get; set; }

        public string? Website { get; set; }

        public string? SerectKey { get; set; }

        public string? Email { get; set; }

        #endregion


        #region Product master
        public string? productSku { get; set; }
        public string? productName { get; set; }
        public string? productDescription { get; set; }
        public int? productWidth { get; set; }
        public int? productHeight { get; set; }
        public string? productDimension { get; set; }
        public string? unitOfDimension { get; set; }
        public int? productWeight { get; set; }
        public string? unitOfWeight { get; set; }
        public string? productColor { get; set; }


        #endregion


        #region Location Master

        public string? locationName { get; set; }
        public string? locationDescription { get; set; }
        public string? locationType { get; set; }


        #endregion


        #region Sheft master
        public string? sheftName { get; set; }
        public string? sheftDescription { get; set; }


        #endregion


        #region Bin master
        public string? binNumber { get; set; }
        public string? binName { get; set; }
        public string? binDescription { get; set; }
        public string? binType { get; set; }
        public string? binStatus { get; set; }


        #endregion

    }
}
