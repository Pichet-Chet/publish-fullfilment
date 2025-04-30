using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class SysDocument
{
    public int Id { get; set; }

    public string? Tpye { get; set; }

    public int? VendorId { get; set; }

    public string? DocumentNo { get; set; }

    public string? DocumentYear { get; set; }

    public string? DocumentMonth { get; set; }

    public string? DocumentDay { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? ProductId { get; set; }
}
