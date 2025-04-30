using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class GoodReceiveStatus
{
    public int Id { get; set; }

    public string Value { get; set; } = null!;

    public string? Name { get; set; }

    public string? ClassColor { get; set; }

    public bool? IsActive { get; set; }
}
