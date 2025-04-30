using System;
namespace FULFILLMENT.H4U.API.Model.Custom
{
	public class ViewGoodReceiveHeaderModel : GoodReceiveHeader
	{

        #region Inbound Header

        public string? InboundHeaderDocumentNo { get; set; }

        public string? InboundHeaderStatus { get; set; }

        public string? InboundHeaderCarNo { get; set; }

        public DateTime? InboundShippingDate { get; set; }

        public DateTime? InboundArrivedDate { get; set; }

        #endregion

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

