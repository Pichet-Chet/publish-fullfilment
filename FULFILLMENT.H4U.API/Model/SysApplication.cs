using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class SysApplication
{
    public int ApplicationId { get; set; }

    public string ApplicationName { get; set; } = null!;

    public string? ApplicationDescription { get; set; }
}
