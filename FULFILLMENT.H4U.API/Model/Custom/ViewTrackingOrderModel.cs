namespace FULFILLMENT.H4U.API.Model.Custom
{
    public class ViewTrackingOrderModel
    {
        #region Vendor Master

        public string? VendorCode { get; set; }

        public string? VendorName { get; set; }

        public string? ContactName { get; set; }

        public string? ContactTel { get; set; }

        public string? ContactAddress { get; set; }

        public string? LineId { get; set; }

        public string? Email { get; set; }

        #endregion



        #region Purchase Order

        public string? PurchaseOrderNo { get; set; }
        public string? PurchaseOrderBy { get; set; }
        public DateTime? PurchaseOrderDate { get; set; }
        public string? PurchaseOrderShipping { get; set; }

        #endregion


        #region Picklist 

        public string? PicklistNo { get; set; }
        public string? PicklistBy { get; set; }
        public DateTime? PicklistDate { get; set; }

        #endregion


        #region Packing

        public string? PackingNo { get; set; }
        public string? PackingBy { get; set; }
        public DateTime? PackingDate { get; set; }

        #endregion


        #region Shipped

        public string? ShippedBy { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? ShippedTracking { get; set; }
        public string? ShippedWebsite { get; set; }

        #endregion

    }
}
