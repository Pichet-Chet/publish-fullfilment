using FULFILLMENT.H4U.API.Model;
namespace FULFILLMENT.H4U.API.Model.Custom
{
    public class ViewMasterProductModel : MasterProduct
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

        #region Product Type
        public string? ProductTypeName { get; set; }

        #endregion
    }
}
