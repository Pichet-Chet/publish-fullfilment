using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class MasterBinStatus
{
    public int BinStatusId { get; set; }

    public string BinStatusName { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string? ClassColor { get; set; }

    public bool? IsActive { get; set; }
}
