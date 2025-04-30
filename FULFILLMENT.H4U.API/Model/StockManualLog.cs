using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class StockManualLog
{
    public int Id { get; set; }

    public int? StockId { get; set; }

    public string? Type { get; set; }

    public int? AmountOld { get; set; }

    public int? AmountChange { get; set; }

    public string? Reason { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
