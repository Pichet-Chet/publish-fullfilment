using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class GoodReceiveHeader
{
    public int Id { get; set; }

    /// <summary>
    /// เลขที่เอกสาร
    /// </summary>
    public string? DocumentNo { get; set; }

    /// <summary>
    /// วันที่ทำรับ
    /// </summary>
    public DateTime? ReceiveDate { get; set; }

    /// <summary>
    /// สถานะ
    /// </summary>
    public string? ReceiveStatus { get; set; }

    /// <summary>
    /// ใบแจ้งส่งของ Request Inbound - Header ID
    /// </summary>
    public int? InboundHeaderId { get; set; }

    public int? VendorId { get; set; }

    public string? Remark { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
