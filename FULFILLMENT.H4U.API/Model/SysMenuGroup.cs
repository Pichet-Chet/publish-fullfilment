using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class SysMenuGroup
{
    public int Id { get; set; }

    public string ApplicationName { get; set; } = null!;

    public string? MenuCode { get; set; }

    public string? MenuName { get; set; }

    public string? MenuDrsciption { get; set; }

    public int? MenuSequence { get; set; }

    public string? MenuIcon { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
