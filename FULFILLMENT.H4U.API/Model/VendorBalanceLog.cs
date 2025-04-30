using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class VendorBalanceLog
{
    public int Id { get; set; }

    public int? VendorId { get; set; }

    public string? Type { get; set; }

    public float? AmountOld { get; set; }

    public float? AmountChange { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
