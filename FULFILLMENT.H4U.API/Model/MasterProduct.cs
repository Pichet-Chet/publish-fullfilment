using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class MasterProduct
{
    public int ProductId { get; set; }

    public string? ProductSku { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public string? ProductColor { get; set; }

    public int? VendorId { get; set; }

    public string? VendorSku { get; set; }

    public int? ProductHeight { get; set; }

    public int? ProductWidth { get; set; }

    public string? ProductDimension { get; set; }

    public int? ProductWeight { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsActive { get; set; }

    public string? UnitOfDimension { get; set; }

    public string? UnitOfWeight { get; set; }

    public int? ProductTypeId { get; set; }

    public string? FileLocation { get; set; }

    public int? ProductLength { get; set; }
}
