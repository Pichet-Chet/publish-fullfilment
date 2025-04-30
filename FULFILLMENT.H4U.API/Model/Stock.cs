using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class Stock
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? Amount { get; set; }

    public int? LocationId { get; set; }

    public int? SheftId { get; set; }

    public int? BinId { get; set; }

    public string? Status { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsActive { get; set; }

    public int? VendorId { get; set; }
}
