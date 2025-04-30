using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class SysRoleList
{
    public int Id { get; set; }

    public int? RoleGroupId { get; set; }

    public int? MenuGroupId { get; set; }

    public int? MenuId { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsActive { get; set; }
}
