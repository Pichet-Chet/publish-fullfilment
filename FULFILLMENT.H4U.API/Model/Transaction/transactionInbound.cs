namespace FULFILLMENT.H4U.API.Model.Transaction
{
    public class transactionInbound
    {
        public InboundHeader header { get; set; }

        public List<InboundItem> item { get; set; }
    }
}
