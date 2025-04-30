using System;
namespace FULFILLMENT.H4U.API.Model.Custom
{
    public class FilterModel
    {
        public string? documentNo { get; set; }
        public string? documentStatus { get; set; }
        public string? binType { get; set; }
        public int? vendorId { get; set; }
        public int? productId { get; set; }
        public string? productIdStr { get; set; }
        public int? locationId { get; set; }
        public int? sheftId { get; set; }
        public int? binId { get; set; }
        public int? picklistId { get; set; }
        public string? createBy { get; set; }
        public string? updateBy { get; set; }
        public int? roleGroupId { get; set; }
        public string? roleGroupValue { get; set; }

        public bool? isActive { get; set; }

        public DateTime? dateFrom { get; set; }

        public DateTime? dateTo { get; set; }

        public string? name { get; set; }

        public string? code { get; set; }

        public string? description { get; set; }
        public string? shipping { get; set; }
        public string? platform { get; set; }
        public string? operation { get; set; }

        public string? purchaseOrderNo { get; set; }

    }
}

