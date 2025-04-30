namespace FULFILLMENT.H4U.API.Model.Custom
{
    public class ViewPurchaseOrderHeaderModel : PurchaseOrderHeader
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

        #region status

        public string? color { get; set; }

        public int? BoxId { get; set; }
        public string? BoxName { get; set; }
        public float? ShippingPrice { get; set; }
        public DateTime? ShippingDate { get; set; }


        #endregion

    }
}
