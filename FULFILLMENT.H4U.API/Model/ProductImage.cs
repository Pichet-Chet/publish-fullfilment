using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class ProductImage
{
    public int ProductImageId { get; set; }

    public int? ProductId { get; set; }

    public string? FileName { get; set; }

    public string? FileType { get; set; }

    public string? FilePath { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsActive { get; set; }
}
