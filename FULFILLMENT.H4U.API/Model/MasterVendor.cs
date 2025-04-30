using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class MasterVendor
{
    public int VendorId { get; set; }

    /// <summary>
    /// Ex. SCG
    /// </summary>
    public string? VendorCode { get; set; }

    public string? VendorName { get; set; }

    public string? TaxId { get; set; }

    public string? ContactName { get; set; }

    public string? ContactTel { get; set; }

    public string? ContactAddress { get; set; }

    public string? LineId { get; set; }

    public string? Website { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsActive { get; set; }

    public string? SerectKey { get; set; }

    public string? Email { get; set; }

    public float? Balance { get; set; }
}
