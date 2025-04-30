using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class PurchaseOrderItem
{
    public int Id { get; set; }

    public int? HeaderId { get; set; }

    public int? ProductId { get; set; }

    public int? Amount { get; set; }

    public string? StatusValue { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Remark { get; set; }
}
