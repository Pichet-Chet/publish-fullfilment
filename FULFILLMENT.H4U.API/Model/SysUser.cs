using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class SysUser
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserPassword { get; set; }

    public string? FirstNameTh { get; set; }

    public string? LastNameTh { get; set; }

    public string? FirstNameEn { get; set; }

    public string? LastNameEn { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsActive { get; set; }

    public int? VenderId { get; set; }

    public string SecretKey { get; set; } = null!;

    public string? ApplicationName { get; set; }

    public string? Role { get; set; }
}
