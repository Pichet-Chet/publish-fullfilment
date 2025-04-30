using System;
namespace FULFILLMENT.H4U.API.Model.Custom
{
    public class ViewGoodReceiveItemModel : GoodReceiveItem
    {

        #region Goods Receive Header
        public string? headerDocumentNo { get; set; }
        public DateTime? headerReceiveDate { get; set; }
        public string? headerReceiveLot { get; set; }
        public string? headerReceiveStatus { get; set; }
        public string? headerRemark { get; set; }

        #endregion


        #region Product master
        //public string? productSku { get; set; }
        public string? productName { get; set; }
        public string? productDescription { get; set; }
        public int? productWidth { get; set; }
        public int? productHeight { get; set; }
        public string? productDimension { get; set; }
        public string? unitOfDimension { get; set; }
        public int? productWeight { get; set; }
        public string? unitOfWeight { get; set; }


        #endregion


        #region Location Master

        public string? locationName { get; set; }
        public string? locationDescription { get; set; }
        public string? locationType { get; set; }


        #endregion


        #region Sheft master
        public string? sheftName { get; set; }
        public string? sheftDescription { get; set; }


        #endregion


        #region Bin master
        public string? binNumber { get; set; }
        public string? binName { get; set; }
        public string? binDescription { get; set; }
        public string? binType { get; set; }
        public string? binStatus { get; set; }


        #endregion



    }
}

