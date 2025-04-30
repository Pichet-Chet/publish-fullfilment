using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class SysAccess
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? AccessFunction { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? AccessDetail { get; set; }

    public string? IpAddress { get; set; }

    public string? MacAddress { get; set; }

    public int? VendorCode { get; set; }

    public int? MenuCode { get; set; }
}
