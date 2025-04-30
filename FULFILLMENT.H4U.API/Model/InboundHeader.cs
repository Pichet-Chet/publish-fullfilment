using System;
using System.Collections.Generic;

namespace FULFILLMENT.H4U.API.Model;

public partial class InboundHeader
{
    public int Id { get; set; }

    /// <summary>
    /// เลขที่เอกสาร
    /// </summary>
    public string? DocumentNo { get; set; }

    /// <summary>
    /// สถานะจัดส่ง EX : DRAFT , SHIPPING , RECEIVED , CANCEL , REJECT
    /// </summary>
    public string? DocumentStatus { get; set; }

    /// <summary>
    /// คู่ค้าที่ทำรายการ
    /// </summary>
    public int? VendorId { get; set; }

    /// <summary>
    /// ทะเบียนรถ
    /// </summary>
    public string? CarNumber { get; set; }

    /// <summary>
    /// วันที่จะจัดส่ง
    /// </summary>
    public DateTime? ShippingDate { get; set; }

    public DateTime? ArrivedDate { get; set; }

    public string? Remark { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? ShippingValue { get; set; }
}
