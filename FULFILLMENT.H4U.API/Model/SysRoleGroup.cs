using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class SysRoleGroup
{
    public int Id { get; set; }

    public string? Value { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsActive { get; set; }
}
