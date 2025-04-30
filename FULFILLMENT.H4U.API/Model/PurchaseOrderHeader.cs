using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class PurchaseOrderHeader
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
    /// บริษัทคู่ค้า
    /// </summary>
    public int? VendorId { get; set; }

    /// <summary>
    /// ใช้ที่อยู่เดียวกับ vendor master
    /// </summary>
    public bool? VendorFlagAddress { get; set; }

    public string? FromName { get; set; }

    public string? FromAddress { get; set; }

    public string? FromTel { get; set; }

    public string? FromEmail { get; set; }

    public string? ToName { get; set; }

    public string? ToAddress { get; set; }

    public string? ToTel { get; set; }

    public string? ToEmail { get; set; }

    /// <summary>
    /// Dropdown select
    /// </summary>
    public string? ShippingValue { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Remark { get; set; }

    public string? PlatformValue { get; set; }

    public string? TrackingNumber { get; set; }
}
