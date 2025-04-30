namespace FULFILLMENT.H4U.API.Model.Transaction
{
    public class transactionPurchaseOrder
    {
        public PurchaseOrderHeader header { get; set; }

        public List<PurchaseOrderItem> item { get; set; }

        public transactionPurchaseOrder()
        {
            header = new PurchaseOrderHeader();
            item = new List<PurchaseOrderItem>();

        }
    }
}
