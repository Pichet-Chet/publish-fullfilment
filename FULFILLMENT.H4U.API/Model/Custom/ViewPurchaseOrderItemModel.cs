namespace FULFILLMENT.H4U.API.Model.Custom
{
    public class ViewPurchaseOrderItemModel : PurchaseOrderItem
    {

        #region Purchase Order Header

        public string? headerDocumentNo { get; set; }

        public bool? VendorFlagAddress { get; set; }

        public string? FromName { get; set; }

        public string? FromAddress { get; set; }

        public string? FromTel { get; set; }

        public string? FromEmail { get; set; }

        public string? ToName { get; set; }

        public string? ToAddress { get; set; }

        public string? ToTel { get; set; }

        public string? ToEmail { get; set; }

        public string? ShippingValue { get; set; }
        public string? PlatformValue { get; set; }
        public string? Remark { get; set; }


        #endregion



        #region Product master
        public string? productSku { get; set; }
        public string? productName { get; set; }
        public string? productColor { get; set; }
        public string? productDescription { get; set; }
        public int? productWidth { get; set; }
        public int? productHeight { get; set; }
        public string? productDimension { get; set; }
        public string? unitOfDimension { get; set; }
        public int? productWeight { get; set; }
        public string? unitOfWeight { get; set; }


        #endregion


    }
}
