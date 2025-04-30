using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class Packing
{
    public int Id { get; set; }

    /// <summary>
    /// เลขที่เอกสาร
    /// </summary>
    public string? DocumentNo { get; set; }

    /// <summary>
    /// สถานะ
    /// </summary>
    public string? DocumentStatus { get; set; }

    /// <summary>
    /// เลขที่ออเดอร์
    /// </summary>
    public int? PurchaseOrderHeaderId { get; set; }

    public int? PicklistHeaderId { get; set; }

    public int? BinId { get; set; }

    public int? BoxId { get; set; }

    public int? VendorId { get; set; }

    public string? Remark { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? TrackingNumber { get; set; }

    public float? ShippingPrice { get; set; }
}
