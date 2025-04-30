using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class GoodReceiveItem
{
    public int Id { get; set; }

    /// <summary>
    /// ใบเอกสารหลัก
    /// </summary>
    public int? HeaderId { get; set; }

    /// <summary>
    /// รายการสินค้า
    /// </summary>
    public int? ProductId { get; set; }

    /// <summary>
    /// จำนวนสินค้า
    /// </summary>
    public int? Amount { get; set; }

    public int? LocationId { get; set; }

    public int? SheftId { get; set; }

    public int? BinId { get; set; }

    public string? ReceiveStatus { get; set; }

    public string? Lot { get; set; }

    public int? Seq { get; set; }

    public string? Remark { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? ProductSku { get; set; }
}
