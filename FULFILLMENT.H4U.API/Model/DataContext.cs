using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FULFILLMENT.H4U.API.Model;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GoodReceiveHeader> GoodReceiveHeaders { get; set; }

    public virtual DbSet<GoodReceiveItem> GoodReceiveItems { get; set; }

    public virtual DbSet<GoodReceiveStatus> GoodReceiveStatuses { get; set; }

    public virtual DbSet<InboundHeader> InboundHeaders { get; set; }

    public virtual DbSet<InboundItem> InboundItems { get; set; }

    public virtual DbSet<InboundStatus> InboundStatuses { get; set; }

    public virtual DbSet<MasterBin> MasterBins { get; set; }

    public virtual DbSet<MasterBinStatus> MasterBinStatuses { get; set; }

    public virtual DbSet<MasterBinType> MasterBinTypes { get; set; }

    public virtual DbSet<MasterBox> MasterBoxes { get; set; }

    public virtual DbSet<MasterLocation> MasterLocations { get; set; }

    public virtual DbSet<MasterLocationType> MasterLocationTypes { get; set; }

    public virtual DbSet<MasterPlatform> MasterPlatforms { get; set; }

    public virtual DbSet<MasterProduct> MasterProducts { get; set; }

    public virtual DbSet<MasterProductsType> MasterProductsTypes { get; set; }

    public virtual DbSet<MasterSheft> MasterShefts { get; set; }

    public virtual DbSet<MasterShipping> MasterShippings { get; set; }

    public virtual DbSet<MasterVendor> MasterVendors { get; set; }

    public virtual DbSet<MasterWarehouse> MasterWarehouses { get; set; }

    public virtual DbSet<Packing> Packings { get; set; }

    public virtual DbSet<PicklistHeader> PicklistHeaders { get; set; }

    public virtual DbSet<PicklistItem> PicklistItems { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }

    public virtual DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }

    public virtual DbSet<PurchaseOrderStatus> PurchaseOrderStatuses { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<StockManualLog> StockManualLogs { get; set; }

    public virtual DbSet<SysAccess> SysAccesses { get; set; }

    public virtual DbSet<SysApplication> SysApplications { get; set; }

    public virtual DbSet<SysDocument> SysDocuments { get; set; }

    public virtual DbSet<SysMenuGroup> SysMenuGroups { get; set; }

    public virtual DbSet<SysMenuList> SysMenuLists { get; set; }

    public virtual DbSet<SysRoleGroup> SysRoleGroups { get; set; }

    public virtual DbSet<SysRoleList> SysRoleLists { get; set; }

    public virtual DbSet<SysUser> SysUsers { get; set; }

    public virtual DbSet<VendorBalanceLog> VendorBalanceLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=45.154.26.223;port=3306;user=h4u_db;password=5NnruL47kPEIzapCv;database=h4u_db;charset=utf8");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GoodReceiveHeader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("good_receive_header");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(100)
                .HasComment("เลขที่เอกสาร")
                .HasColumnName("document_no");
            entity.Property(e => e.InboundHeaderId)
                .HasComment("ใบแจ้งส่งของ Request Inbound - Header ID")
                .HasColumnName("inbound_header_id");
            entity.Property(e => e.ReceiveDate)
                .HasComment("วันที่ทำรับ")
                .HasColumnType("datetime")
                .HasColumnName("receive_date");
            entity.Property(e => e.ReceiveStatus)
                .HasMaxLength(100)
                .HasComment("สถานะ")
                .HasColumnName("receive_status");
            entity.Property(e => e.Remark)
                .HasMaxLength(100)
                .HasColumnName("remark");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");
        });

        modelBuilder.Entity<GoodReceiveItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("good_receive_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasComment("จำนวนสินค้า")
                .HasColumnName("amount");
            entity.Property(e => e.BinId).HasColumnName("bin_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.HeaderId)
                .HasComment("ใบเอกสารหลัก")
                .HasColumnName("header_id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Lot)
                .HasMaxLength(100)
                .HasColumnName("lot");
            entity.Property(e => e.ProductId)
                .HasComment("รายการสินค้า")
                .HasColumnName("product_id");
            entity.Property(e => e.ProductSku)
                .HasMaxLength(100)
                .HasColumnName("product_sku");
            entity.Property(e => e.ReceiveStatus)
                .HasMaxLength(100)
                .HasColumnName("receive_status");
            entity.Property(e => e.Remark)
                .HasMaxLength(100)
                .HasColumnName("remark");
            entity.Property(e => e.Seq).HasColumnName("seq");
            entity.Property(e => e.SheftId).HasColumnName("sheft_id");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<GoodReceiveStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("good_receive_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassColor)
                .HasMaxLength(100)
                .HasColumnName("class_color");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasColumnName("value");
        });

        modelBuilder.Entity<InboundHeader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inbound_header");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArrivedDate)
                .HasColumnType("datetime")
                .HasColumnName("arrived_date");
            entity.Property(e => e.CarNumber)
                .HasMaxLength(100)
                .HasComment("ทะเบียนรถ")
                .HasColumnName("car_number");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(100)
                .HasComment("เลขที่เอกสาร")
                .HasColumnName("document_no");
            entity.Property(e => e.DocumentStatus)
                .HasMaxLength(100)
                .HasComment("สถานะจัดส่ง EX : DRAFT , SHIPPING , RECEIVED , CANCEL , REJECT")
                .HasColumnName("document_status");
            entity.Property(e => e.Remark)
                .HasMaxLength(500)
                .HasColumnName("remark");
            entity.Property(e => e.ShippingDate)
                .HasComment("วันที่จะจัดส่ง")
                .HasColumnType("datetime")
                .HasColumnName("shipping_date");
            entity.Property(e => e.ShippingValue)
                .HasMaxLength(100)
                .HasColumnName("shipping_value");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorId)
                .HasComment("คู่ค้าที่ทำรายการ")
                .HasColumnName("vendor_id");
        });

        modelBuilder.Entity<InboundItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inbound_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasComment("จำนวน")
                .HasColumnName("amount");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.HeaderId)
                .HasComment("เอกสารใบรายการ")
                .HasColumnName("header_id");
            entity.Property(e => e.ItemStatus)
                .HasMaxLength(100)
                .HasComment("สถานะสินค้า")
                .HasColumnName("item_status");
            entity.Property(e => e.ProductId)
                .HasComment("รายการสินค้า")
                .HasColumnName("product_id");
            entity.Property(e => e.Remark)
                .HasMaxLength(500)
                .HasColumnName("remark");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<InboundStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inbound_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassColor)
                .HasMaxLength(100)
                .HasColumnName("class_color");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("isActive");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasColumnName("value");
        });

        modelBuilder.Entity<MasterBin>(entity =>
        {
            entity.HasKey(e => e.BinId).HasName("PRIMARY");

            entity.ToTable("master_bin");

            entity.Property(e => e.BinId).HasColumnName("bin_id");
            entity.Property(e => e.BinDescription)
                .HasMaxLength(500)
                .HasColumnName("bin_description");
            entity.Property(e => e.BinName)
                .HasMaxLength(200)
                .HasColumnName("bin_name");
            entity.Property(e => e.BinNumber)
                .HasMaxLength(100)
                .HasColumnName("bin_number");
            entity.Property(e => e.BinStatus)
                .HasMaxLength(500)
                .HasColumnName("bin_status");
            entity.Property(e => e.BinType)
                .HasMaxLength(500)
                .HasColumnName("bin_type");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.PicklistId).HasColumnName("picklist_id");
            entity.Property(e => e.SheftId).HasColumnName("sheft_id");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<MasterBinStatus>(entity =>
        {
            entity.HasKey(e => e.BinStatusId).HasName("PRIMARY");

            entity.ToTable("master_bin_status");

            entity.HasIndex(e => e.BinStatusName, "master_bin_status_bin_status_name_IDX");

            entity.Property(e => e.BinStatusId).HasColumnName("bin_status_id");
            entity.Property(e => e.BinStatusName)
                .HasMaxLength(100)
                .HasColumnName("bin_status_name");
            entity.Property(e => e.ClassColor)
                .HasMaxLength(100)
                .HasColumnName("class_color");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasColumnName("display_name");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
        });

        modelBuilder.Entity<MasterBinType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PRIMARY");

            entity.ToTable("master_bin_type");

            entity.HasIndex(e => e.TypeName, "master_bin_type_type_name_IDX");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.ClassColor)
                .HasMaxLength(100)
                .HasColumnName("class_color");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasColumnName("display_name");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<MasterBox>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("master_box");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Size)
                .HasMaxLength(100)
                .HasColumnName("size");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<MasterLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PRIMARY");

            entity.ToTable("master_location");

            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LocationDescription)
                .HasMaxLength(500)
                .HasColumnName("location_description");
            entity.Property(e => e.LocationName)
                .HasMaxLength(100)
                .HasColumnName("location_name");
            entity.Property(e => e.LocationType)
                .HasMaxLength(20)
                .HasColumnName("location_type");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<MasterLocationType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PRIMARY");

            entity.ToTable("master_location_type");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.ClassColor)
                .HasMaxLength(100)
                .HasColumnName("class_color");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasColumnName("display_name");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<MasterPlatform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("master_platforms");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.NameEn)
                .HasMaxLength(20)
                .HasColumnName("name_en");
            entity.Property(e => e.NameTh)
                .HasMaxLength(100)
                .HasColumnName("name_th");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasColumnName("value");
            entity.Property(e => e.Website)
                .HasMaxLength(1000)
                .HasColumnName("website");
        });

        modelBuilder.Entity<MasterProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("master_products");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.FileLocation)
                .HasMaxLength(1000)
                .HasColumnName("file_location");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ProductColor)
                .HasMaxLength(20)
                .HasColumnName("product_color");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(500)
                .HasColumnName("product_description");
            entity.Property(e => e.ProductDimension)
                .HasMaxLength(100)
                .HasColumnName("product_dimension");
            entity.Property(e => e.ProductHeight).HasColumnName("product_height");
            entity.Property(e => e.ProductLength).HasColumnName("product_length");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductSku)
                .HasMaxLength(20)
                .HasColumnName("product_sku");
            entity.Property(e => e.ProductTypeId).HasColumnName("product_type_id");
            entity.Property(e => e.ProductWeight).HasColumnName("product_weight");
            entity.Property(e => e.ProductWidth).HasColumnName("product_width");
            entity.Property(e => e.UnitOfDimension)
                .HasMaxLength(100)
                .HasColumnName("unit_of_dimension");
            entity.Property(e => e.UnitOfWeight)
                .HasMaxLength(100)
                .HasColumnName("unit_of_weight");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");
            entity.Property(e => e.VendorSku)
                .HasMaxLength(30)
                .HasColumnName("vendor_sku");
        });

        modelBuilder.Entity<MasterProductsType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("master_products_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.NameEn)
                .HasMaxLength(20)
                .HasColumnName("name_en");
            entity.Property(e => e.NameTh)
                .HasMaxLength(100)
                .HasColumnName("name_th");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasColumnName("value");
        });

        modelBuilder.Entity<MasterSheft>(entity =>
        {
            entity.HasKey(e => e.SheftId).HasName("PRIMARY");

            entity.ToTable("master_sheft");

            entity.Property(e => e.SheftId).HasColumnName("sheft_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.SheftDescription)
                .HasMaxLength(500)
                .HasColumnName("sheft_description");
            entity.Property(e => e.SheftName)
                .HasMaxLength(100)
                .HasColumnName("sheft_name");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<MasterShipping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("master_shipping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.NameEn)
                .HasMaxLength(20)
                .HasColumnName("name_en");
            entity.Property(e => e.NameTh)
                .HasMaxLength(100)
                .HasColumnName("name_th");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.Url)
                .HasMaxLength(1000)
                .HasColumnName("url");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasColumnName("value");
        });

        modelBuilder.Entity<MasterVendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PRIMARY");

            entity.ToTable("master_vendor");

            entity.Property(e => e.VendorId).HasColumnName("vendor_id");
            entity.Property(e => e.Balance).HasColumnName("balance");
            entity.Property(e => e.ContactAddress)
                .HasMaxLength(200)
                .HasColumnName("contact_address");
            entity.Property(e => e.ContactName)
                .HasMaxLength(100)
                .HasColumnName("contact_name");
            entity.Property(e => e.ContactTel)
                .HasMaxLength(100)
                .HasColumnName("contact_tel");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LineId)
                .HasMaxLength(50)
                .HasColumnName("line_id");
            entity.Property(e => e.SerectKey)
                .HasMaxLength(100)
                .HasColumnName("serect_key");
            entity.Property(e => e.TaxId)
                .HasMaxLength(100)
                .HasColumnName("tax_id");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorCode)
                .HasMaxLength(10)
                .HasComment("Ex. SCG")
                .HasColumnName("vendor_code");
            entity.Property(e => e.VendorName)
                .HasMaxLength(100)
                .HasColumnName("vendor_name");
            entity.Property(e => e.Website)
                .HasMaxLength(50)
                .HasColumnName("website");
        });

        modelBuilder.Entity<MasterWarehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("master_warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasColumnName("name_en");
            entity.Property(e => e.NameTh)
                .HasMaxLength(100)
                .HasColumnName("name_th");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<Packing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("packing");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BinId).HasColumnName("bin_id");
            entity.Property(e => e.BoxId).HasColumnName("box_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(100)
                .HasComment("เลขที่เอกสาร")
                .HasColumnName("document_no");
            entity.Property(e => e.DocumentStatus)
                .HasMaxLength(100)
                .HasComment("สถานะ")
                .HasColumnName("document_status");
            entity.Property(e => e.PicklistHeaderId).HasColumnName("picklist_header_id");
            entity.Property(e => e.PurchaseOrderHeaderId)
                .HasComment("เลขที่ออเดอร์")
                .HasColumnName("purchase_order_header_id");
            entity.Property(e => e.Remark)
                .HasMaxLength(1000)
                .HasColumnName("remark");
            entity.Property(e => e.ShippingPrice).HasColumnName("shipping_price");
            entity.Property(e => e.TrackingNumber)
                .HasMaxLength(100)
                .HasColumnName("tracking_number");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");
        });

        modelBuilder.Entity<PicklistHeader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("picklist_header");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BinId).HasColumnName("bin_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(100)
                .HasColumnName("document_no");
            entity.Property(e => e.DocumentStatus)
                .HasMaxLength(100)
                .HasColumnName("document_status");
            entity.Property(e => e.PurchaseOrderHeaderId).HasColumnName("purchase_order_header_id");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");
        });

        modelBuilder.Entity<PicklistItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("picklist_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.AtBin).HasColumnName("at_bin");
            entity.Property(e => e.AtLocation).HasColumnName("at_location");
            entity.Property(e => e.AtSheft).HasColumnName("at_sheft");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.HeaderId).HasColumnName("header_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Remark)
                .HasMaxLength(500)
                .HasColumnName("remark");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageId).HasName("PRIMARY");

            entity.ToTable("product_image");

            entity.Property(e => e.ProductImageId).HasColumnName("product_image_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.FileName)
                .HasMaxLength(300)
                .HasColumnName("file_name");
            entity.Property(e => e.FilePath)
                .HasMaxLength(500)
                .HasColumnName("file_path");
            entity.Property(e => e.FileType)
                .HasMaxLength(100)
                .HasColumnName("file_type");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<PurchaseOrderHeader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("purchase_order_header");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(100)
                .HasComment("เลขที่เอกสาร")
                .HasColumnName("document_no");
            entity.Property(e => e.DocumentStatus)
                .HasMaxLength(100)
                .HasComment("สถานะ")
                .HasColumnName("document_status");
            entity.Property(e => e.FromAddress)
                .HasMaxLength(100)
                .HasColumnName("from_address");
            entity.Property(e => e.FromEmail)
                .HasMaxLength(100)
                .HasColumnName("from_email");
            entity.Property(e => e.FromName)
                .HasMaxLength(100)
                .HasColumnName("from_name");
            entity.Property(e => e.FromTel)
                .HasMaxLength(100)
                .HasColumnName("from_tel");
            entity.Property(e => e.PlatformValue)
                .HasMaxLength(100)
                .HasColumnName("platform_value");
            entity.Property(e => e.Remark)
                .HasMaxLength(1000)
                .HasColumnName("remark");
            entity.Property(e => e.ShippingValue)
                .HasMaxLength(100)
                .HasComment("Dropdown select")
                .HasColumnName("shipping_value");
            entity.Property(e => e.ToAddress)
                .HasMaxLength(100)
                .HasColumnName("to_address");
            entity.Property(e => e.ToEmail)
                .HasMaxLength(100)
                .HasColumnName("to_email");
            entity.Property(e => e.ToName)
                .HasMaxLength(100)
                .HasColumnName("to_name");
            entity.Property(e => e.ToTel)
                .HasMaxLength(100)
                .HasColumnName("to_tel");
            entity.Property(e => e.TrackingNumber)
                .HasMaxLength(100)
                .HasColumnName("tracking_number");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorFlagAddress)
                .HasComment("ใช้ที่อยู่เดียวกับ vendor master")
                .HasColumnName("vendor_flag_address");
            entity.Property(e => e.VendorId)
                .HasComment("บริษัทคู่ค้า")
                .HasColumnName("vendor_id");
        });

        modelBuilder.Entity<PurchaseOrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("purchase_order_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.HeaderId).HasColumnName("header_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Remark)
                .HasMaxLength(1000)
                .HasColumnName("remark");
            entity.Property(e => e.StatusValue)
                .HasMaxLength(100)
                .HasColumnName("status_value");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<PurchaseOrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("purchase_order_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassColor)
                .HasMaxLength(100)
                .HasColumnName("class_color");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("stock");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.BinId).HasColumnName("bin_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.SheftId).HasColumnName("sheft_id");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");
        });

        modelBuilder.Entity<StockManualLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("stock_manual_log");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AmountChange).HasColumnName("amount_change");
            entity.Property(e => e.AmountOld).HasColumnName("amount_old");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Reason)
                .HasMaxLength(500)
                .HasColumnName("reason");
            entity.Property(e => e.StockId).HasColumnName("stock_id");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<SysAccess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sys_access");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccessDetail)
                .HasMaxLength(100)
                .HasColumnName("access_detail");
            entity.Property(e => e.AccessFunction)
                .HasMaxLength(100)
                .HasColumnName("access_function");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(100)
                .HasColumnName("ip_address");
            entity.Property(e => e.MacAddress)
                .HasMaxLength(100)
                .HasColumnName("mac_address");
            entity.Property(e => e.MenuCode).HasColumnName("menu_code");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("datetime")
                .HasColumnName("transaction_date");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("user_name");
            entity.Property(e => e.VendorCode).HasColumnName("vendor_code");
        });

        modelBuilder.Entity<SysApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PRIMARY");

            entity.ToTable("sys_application");

            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.ApplicationDescription)
                .HasMaxLength(100)
                .HasColumnName("application_description");
            entity.Property(e => e.ApplicationName)
                .HasMaxLength(100)
                .HasColumnName("application_name");
        });

        modelBuilder.Entity<SysDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sys_document");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DocumentDay)
                .HasMaxLength(100)
                .HasColumnName("document_day");
            entity.Property(e => e.DocumentMonth)
                .HasMaxLength(100)
                .HasColumnName("document_month");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(100)
                .HasColumnName("document_no");
            entity.Property(e => e.DocumentYear)
                .HasMaxLength(100)
                .HasColumnName("document_year");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Tpye)
                .HasMaxLength(100)
                .HasColumnName("tpye");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");
        });

        modelBuilder.Entity<SysMenuGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sys_menu_group");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApplicationName)
                .HasMaxLength(100)
                .HasColumnName("application_name");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.MenuCode)
                .HasMaxLength(100)
                .HasColumnName("menu_code");
            entity.Property(e => e.MenuDrsciption)
                .HasMaxLength(100)
                .HasColumnName("menu_drsciption");
            entity.Property(e => e.MenuIcon)
                .HasMaxLength(100)
                .HasColumnName("menu_icon");
            entity.Property(e => e.MenuName)
                .HasMaxLength(100)
                .HasColumnName("menu_name");
            entity.Property(e => e.MenuSequence).HasColumnName("menu_sequence");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<SysMenuList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sys_menu_list");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(100)
                .HasColumnName("action");
            entity.Property(e => e.ApplicationName)
                .HasMaxLength(100)
                .HasColumnName("application_name");
            entity.Property(e => e.Controller)
                .HasMaxLength(100)
                .HasColumnName("controller");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.MenuCode)
                .HasMaxLength(100)
                .HasColumnName("menu_code");
            entity.Property(e => e.MenuDesciption)
                .HasMaxLength(100)
                .HasColumnName("menu_desciption");
            entity.Property(e => e.MenuGroup).HasColumnName("menu_group");
            entity.Property(e => e.MenuIcon)
                .HasMaxLength(100)
                .HasColumnName("menu_icon");
            entity.Property(e => e.MenuName)
                .HasMaxLength(100)
                .HasColumnName("menu_name");
            entity.Property(e => e.MenuSequence).HasColumnName("menu_sequence");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<SysRoleGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sys_role_group");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasColumnName("value");
        });

        modelBuilder.Entity<SysRoleList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sys_role_list");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.MenuGroupId).HasColumnName("menu_group_id");
            entity.Property(e => e.MenuId).HasColumnName("menu_id");
            entity.Property(e => e.RoleGroupId).HasColumnName("role_group_id");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<SysUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("sys_users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ApplicationName)
                .HasMaxLength(100)
                .HasColumnName("application_name");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.FirstNameEn)
                .HasMaxLength(100)
                .HasColumnName("first_name_en");
            entity.Property(e => e.FirstNameTh)
                .HasMaxLength(100)
                .HasColumnName("first_name_th");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastNameEn)
                .HasMaxLength(100)
                .HasColumnName("last_name_en");
            entity.Property(e => e.LastNameTh)
                .HasMaxLength(100)
                .HasColumnName("last_name_th");
            entity.Property(e => e.Mobile)
                .HasMaxLength(100)
                .HasColumnName("mobile");
            entity.Property(e => e.Role)
                .HasMaxLength(100)
                .HasColumnName("role");
            entity.Property(e => e.SecretKey)
                .HasMaxLength(200)
                .HasColumnName("secretKey");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("user_name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(100)
                .HasColumnName("user_password");
            entity.Property(e => e.VenderId).HasColumnName("vender_id");
        });

        modelBuilder.Entity<VendorBalanceLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vendor_balance_log");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AmountChange).HasColumnName("amount_change");
            entity.Property(e => e.AmountOld).HasColumnName("amount_old");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
