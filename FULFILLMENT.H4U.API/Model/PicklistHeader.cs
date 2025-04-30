using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class PicklistHeader
{
    public int Id { get; set; }

    public string? DocumentNo { get; set; }

    public string? DocumentStatus { get; set; }

    public int? VendorId { get; set; }

    public int? PurchaseOrderHeaderId { get; set; }

    public int? BinId { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
