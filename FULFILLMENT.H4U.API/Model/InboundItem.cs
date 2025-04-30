using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class InboundItem
{
    public int Id { get; set; }

    /// <summary>
    /// เอกสารใบรายการ
    /// </summary>
    public int? HeaderId { get; set; }

    /// <summary>
    /// รายการสินค้า
    /// </summary>
    public int? ProductId { get; set; }

    /// <summary>
    /// จำนวน
    /// </summary>
    public int? Amount { get; set; }

    /// <summary>
    /// สถานะสินค้า
    /// </summary>
    public string? ItemStatus { get; set; }

    public string? Remark { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
