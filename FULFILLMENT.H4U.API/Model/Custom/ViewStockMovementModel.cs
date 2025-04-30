namespace FULFILLMENT.H4U.API.Model.Custom
{
    public class ViewStockMovementModel : StockManualLog
    {

        #region Product master

        public int? ProductId { get; set; }
        public string? ProductSku { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public int? ProductWidth { get; set; }
        public int? ProductHeight { get; set; }
        public string? ProductDimension { get; set; }
        public string? UnitOfDimension { get; set; }
        public int? ProductWeight { get; set; }
        public string? UnitOfWeight { get; set; }


        #endregion

        #region Location Master
        public int? LocationId { get; set; }
        public string? LocationName { get; set; }
        public string? LocationDescription { get; set; }
        public string? LocationType { get; set; }


        #endregion

        #region Sheft master
        public int? SheftId { get; set; }
        public string? SheftName { get; set; }
        public string? SheftDescription { get; set; }


        #endregion

        #region Bin master
        public int? BinId { get; set; }
        public string? BinNumber { get; set; }
        public string? BinName { get; set; }
        public string? BinDescription { get; set; }
        public string? BinType { get; set; }
        public string? BinStatus { get; set; }

        #endregion

        #region Vendor Master
        public int? VendorId { get; set; }

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
