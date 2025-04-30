using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class MasterSheft
{
    public int SheftId { get; set; }

    public int? LocationId { get; set; }

    public string? SheftName { get; set; }

    public string? SheftDescription { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsActive { get; set; }
}
