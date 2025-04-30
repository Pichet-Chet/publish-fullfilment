using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class MasterLocationType
{
    public int TypeId { get; set; }

    public string? TypeName { get; set; }

    public string? DisplayName { get; set; }

    public string? ClassColor { get; set; }

    public bool? IsActive { get; set; }
}
