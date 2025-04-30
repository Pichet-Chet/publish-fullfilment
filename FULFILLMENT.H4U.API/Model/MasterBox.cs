using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class MasterBox
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Size { get; set; }

    public string? Description { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsActive { get; set; }
}
