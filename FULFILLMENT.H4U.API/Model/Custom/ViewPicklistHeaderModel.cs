namespace FULFILLMENT.H4U.API.Model.Custom
{
    public class ViewPicklistHeaderModel : PicklistHeader
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



        #region Purchase Order

        public string? PurchaseOrderDocument { get; set; }

        public string? FromName { get; set; }

        public string? FromAddress { get; set; }

        public string? FromTel { get; set; }

        public string? FromEmail { get; set; }

        public string? ToName { get; set; }

        public string? ToAddress { get; set; }

        public string? ToTel { get; set; }

        public string? ToEmail { get; set; }

        #endregion
    }
}
