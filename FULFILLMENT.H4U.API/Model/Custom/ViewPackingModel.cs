namespace FULFILLMENT.H4U.API.Model.Custom
{
    public class ViewPackingModel : Packing
    {
        public ViewPackingModel()
        {
            purchaseOrderItems = new List<ViewPurchaseOrderItemModel>();
            purchaseOrderHeader = new PurchaseOrderHeader();
            picklistHeader = new PicklistHeader();
            picklistItems = new List<PicklistItem>();
            masterBox = new MasterBox();
        }

        public PurchaseOrderHeader purchaseOrderHeader { get; set; }
        public List<ViewPurchaseOrderItemModel> purchaseOrderItems  { get; set; }
        public PicklistHeader picklistHeader { get; set; }
        public List<PicklistItem> picklistItems { get; set; }
        public MasterBox masterBox { get; set; }

        public string ProductName { get; set; }

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

    }
}
