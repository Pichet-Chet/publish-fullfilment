namespace FULFILLMENT.H4U.API.Model.Transaction
{
    public class transactionGoodReceive
    {
        public GoodReceiveHeader header { get; set; }

        public List<GoodReceiveItem> item { get; set; }
    }
}
