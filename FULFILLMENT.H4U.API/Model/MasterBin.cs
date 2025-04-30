using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class MasterBin
{
    public int BinId { get; set; }

    public string? BinNumber { get; set; }

    public string? BinName { get; set; }

    public string? BinDescription { get; set; }

    public string? BinType { get; set; }

    public string? BinStatus { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool IsActive { get; set; }

    public int? PicklistId { get; set; }

    public int? SheftId { get; set; }
}
