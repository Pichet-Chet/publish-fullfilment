using System;
namespace FULFILLMENT.H4U.API.Model.Custom
{
    public class ViewInboundItemModel : InboundItem
    {

        #region Product Master

        public string? ProductSku { get; set; }

        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public string? ProductColor { get; set; }

        public int? ProductHeight { get; set; }

        public int? ProductWidth { get; set; }

        public string? ProductDimension { get; set; }

        public int? ProductWeight { get; set; }


        #endregion

        #region GR

        public int? Received { get; set; }

        #endregion


    }
}

