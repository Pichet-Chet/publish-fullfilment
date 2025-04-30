using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FULFILLMENT.H4U.API.Model;

public partial class WararichH4uContext : DbContext
{
    public WararichH4uContext()
    {
    }

    public WararichH4uContext(DbContextOptions<WararichH4uContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=43.229.79.175;port=3306;user=wararich_h4u;password=acRZ4dHQF;database=wararich_h4u;charset=utf8;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EfmigrationsHistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__EFMigrationsHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<GoodReceiveHeader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("good_receive_header");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasComment("เลขที่เอกสาร")
                .HasColumnName("document_no");
            entity.Property(e => e.InboundHeaderId)
                .HasDefaultValueSql("'NULL'")
                .HasComment("ใบแจ้งส่งของ Request Inbound - Header ID")
                .HasColumnType("int(11)")
                .HasColumnName("inbound_header_id");
            entity.Property(e => e.ReceiveDate)
                .HasDefaultValueSql("'NULL'")
                .HasComment("วันที่ทำรับ")
                .HasColumnType("datetime")
                .HasColumnName("receive_date");
            entity.Property(e => e.ReceiveStatus)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasComment("สถานะ")
                .HasColumnName("receive_status");
            entity.Property(e => e.Remark)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("remark");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("vendor_id");
        });

        modelBuilder.Entity<GoodReceiveItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("good_receive_item");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasDefaultValueSql("'NULL'")
                .HasComment("จำนวนสินค้า")
                .HasColumnType("int(11)")
                .HasColumnName("amount");
            entity.Property(e => e.BinId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("bin_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.HeaderId)
                .HasDefaultValueSql("'NULL'")
                .HasComment("ใบเอกสารหลัก")
                .HasColumnType("int(11)")
                .HasColumnName("header_id");
            entity.Property(e => e.LocationId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("location_id");
            entity.Property(e => e.Lot)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("lot");
            entity.Property(e => e.ProductId)
                .HasDefaultValueSql("'NULL'")
                .HasComment("รายการสินค้า")
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.ReceiveStatus)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("receive_status");
            entity.Property(e => e.Remark)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("remark");
            entity.Property(e => e.Seq)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("seq");
            entity.Property(e => e.SheftId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("sheft_id");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<GoodReceiveStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("good_receive_status");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ClassColor)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("class_color");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasColumnName("value");
        });

        modelBuilder.Entity<InboundHeader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inbound_header");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ArrivedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("arrived_date");
            entity.Property(e => e.CarNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasComment("ทะเบียนรถ")
                .HasColumnName("car_number");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasComment("เลขที่เอกสาร")
                .HasColumnName("document_no");
            entity.Property(e => e.DocumentStatus)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasComment("สถานะจัดส่ง EX : DRAFT , SHIPPING , RECEIVED , CANCEL , REJECT")
                .HasColumnName("document_status");
            entity.Property(e => e.Remark)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("remark");
            entity.Property(e => e.ShippingDate)
                .HasDefaultValueSql("'NULL'")
                .HasComment("วันที่จะจัดส่ง")
                .HasColumnType("datetime")
                .HasColumnName("shipping_date");
            entity.Property(e => e.ShippingValue)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("shipping_value");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorId)
                .HasDefaultValueSql("'NULL'")
                .HasComment("คู่ค้าที่ทำรายการ")
                .HasColumnType("int(11)")
                .HasColumnName("vendor_id");
        });

        modelBuilder.Entity<InboundItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inbound_item");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasDefaultValueSql("'NULL'")
                .HasComment("จำนวน")
                .HasColumnType("int(11)")
                .HasColumnName("amount");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.HeaderId)
                .HasDefaultValueSql("'NULL'")
                .HasComment("เอกสารใบรายการ")
                .HasColumnType("int(11)")
                .HasColumnName("header_id");
            entity.Property(e => e.ItemStatus)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasComment("สถานะสินค้า")
                .HasColumnName("item_status");
            entity.Property(e => e.ProductId)
                .HasDefaultValueSql("'NULL'")
                .HasComment("รายการสินค้า")
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.Remark)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("remark");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<InboundStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inbound_status");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ClassColor)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("class_color");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("isActive");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasColumnName("value");
        });

        modelBuilder.Entity<MasterBin>(entity =>
        {
            entity.HasKey(e => e.BinId).HasName("PRIMARY");

            entity.ToTable("master_bin");

            entity.Property(e => e.BinId)
                .HasColumnType("int(11)")
                .HasColumnName("bin_id");
            entity.Property(e => e.BinDescription)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("bin_description");
            entity.Property(e => e.BinName)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("bin_name");
            entity.Property(e => e.BinNumber)
                .HasMaxLength(10)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("bin_number");
            entity.Property(e => e.BinStatus)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("bin_status");
            entity.Property(e => e.BinType)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("bin_type");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.PicklistId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("picklist_id");
            entity.Property(e => e.SheftId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("sheft_id");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<MasterBinStatus>(entity =>
        {
            entity.HasKey(e => e.BinStatusId).HasName("PRIMARY");

            entity.ToTable("master_bin_status");

            entity.HasIndex(e => e.BinStatusName, "master_bin_status_bin_status_name_IDX");

            entity.Property(e => e.BinStatusId)
                .HasColumnType("int(11)")
                .HasColumnName("bin_status_id");
            entity.Property(e => e.BinStatusName)
                .HasMaxLength(100)
                .HasColumnName("bin_status_name");
            entity.Property(e => e.ClassColor)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("class_color");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
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

            entity.Property(e => e.TypeId)
                .HasColumnType("int(11)")
                .HasColumnName("type_id");
            entity.Property(e => e.ClassColor)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("class_color");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("display_name");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<MasterBox>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("master_box");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name");
            entity.Property(e => e.Size)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("size");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<MasterLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PRIMARY");

            entity.ToTable("master_location");

            entity.Property(e => e.LocationId)
                .HasColumnType("int(11)")
                .HasColumnName("location_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("is_active");
            entity.Property(e => e.LocationDescription)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("location_description");
            entity.Property(e => e.LocationName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("location_name");
            entity.Property(e => e.LocationType)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("location_type");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<MasterLocationType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PRIMARY");

            entity.ToTable("master_location_type");

            entity.Property(e => e.TypeId)
                .HasColumnType("int(11)")
                .HasColumnName("type_id");
            entity.Property(e => e.ClassColor)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("class_color");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("display_name");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<MasterPlatform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("master_platforms");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.NameEn)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name_en");
            entity.Property(e => e.NameTh)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name_th");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("value");
            entity.Property(e => e.Website)
                .HasMaxLength(1000)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("website");
        });

        modelBuilder.Entity<MasterProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("master_products");

            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.FileLocation)
                .HasMaxLength(1000)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("file_location");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("is_active");
            entity.Property(e => e.ProductColor)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("product_color");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("product_description");
            entity.Property(e => e.ProductDimension)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("product_dimension");
            entity.Property(e => e.ProductHeight)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("product_height");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("product_name");
            entity.Property(e => e.ProductSku)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("product_sku");
            entity.Property(e => e.ProductTypeId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("product_type_id");
            entity.Property(e => e.ProductWeight)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("product_weight");
            entity.Property(e => e.ProductWidth)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("product_width");
            entity.Property(e => e.UnitOfDimension)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("unit_of_dimension");
            entity.Property(e => e.UnitOfWeight)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("unit_of_weight");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("vendor_id");
            entity.Property(e => e.VendorSku)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("vendor_sku");
        });

        modelBuilder.Entity<MasterProductsType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("master_products_type");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.NameEn)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name_en");
            entity.Property(e => e.NameTh)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name_th");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("value");
        });

        modelBuilder.Entity<MasterSheft>(entity =>
        {
            entity.HasKey(e => e.SheftId).HasName("PRIMARY");

            entity.ToTable("master_sheft");

            entity.Property(e => e.SheftId)
                .HasColumnType("int(11)")
                .HasColumnName("sheft_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("is_active");
            entity.Property(e => e.LocationId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("location_id");
            entity.Property(e => e.SheftDescription)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("sheft_description");
            entity.Property(e => e.SheftName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("sheft_name");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<MasterShipping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("master_shipping");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.NameEn)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name_en");
            entity.Property(e => e.NameTh)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name_th");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.Url)
                .HasMaxLength(1000)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("url");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("value");
        });

        modelBuilder.Entity<MasterVendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PRIMARY");

            entity.ToTable("master_vendor");

            entity.Property(e => e.VendorId)
                .HasColumnType("int(11)")
                .HasColumnName("vendor_id");
            entity.Property(e => e.Balance)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("balance");
            entity.Property(e => e.ContactAddress)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("contact_address");
            entity.Property(e => e.ContactName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("contact_name");
            entity.Property(e => e.ContactTel)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("contact_tel");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("is_active");
            entity.Property(e => e.LineId)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("line_id");
            entity.Property(e => e.SerectKey)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("serect_key");
            entity.Property(e => e.TaxId)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("tax_id");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorCode)
                .HasMaxLength(10)
                .HasDefaultValueSql("'NULL'")
                .HasComment("Ex. SCG")
                .HasColumnName("vendor_code");
            entity.Property(e => e.VendorName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("vendor_name");
            entity.Property(e => e.Website)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("website");
        });

        modelBuilder.Entity<MasterWarehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("master_warehouse");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("is_active");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name_en");
            entity.Property(e => e.NameTh)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name_th");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<Packing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("packing");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.BinId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("bin_id");
            entity.Property(e => e.BoxId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("box_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasComment("เลขที่เอกสาร")
                .HasColumnName("document_no");
            entity.Property(e => e.DocumentStatus)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasComment("สถานะ")
                .HasColumnName("document_status");
            entity.Property(e => e.PicklistHeaderId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("picklist_header_id");
            entity.Property(e => e.PurchaseOrderHeaderId)
                .HasDefaultValueSql("'NULL'")
                .HasComment("เลขที่ออเดอร์")
                .HasColumnType("int(11)")
                .HasColumnName("purchase_order_header_id");
            entity.Property(e => e.Remark)
                .HasMaxLength(1000)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("remark");
            entity.Property(e => e.TrackingNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("tracking_number");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("vendor_id");
        });

        modelBuilder.Entity<PicklistHeader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("picklist_header");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.BinId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("bin_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("document_no");
            entity.Property(e => e.DocumentStatus)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("document_status");
            entity.Property(e => e.PurchaseOrderHeaderId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("purchase_order_header_id");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("vendor_id");
        });

        modelBuilder.Entity<PicklistItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("picklist_item");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("amount");
            entity.Property(e => e.AtBin)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("at_bin");
            entity.Property(e => e.AtLocation)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("at_location");
            entity.Property(e => e.AtSheft)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("at_sheft");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.HeaderId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("header_id");
            entity.Property(e => e.ProductId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.Remark)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("remark");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageId).HasName("PRIMARY");

            entity.ToTable("product_image");

            entity.Property(e => e.ProductImageId)
                .HasColumnType("int(11)")
                .HasColumnName("product_image_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.FileName)
                .HasMaxLength(300)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("file_name");
            entity.Property(e => e.FilePath)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("file_path");
            entity.Property(e => e.FileType)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("file_type");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("is_active");
            entity.Property(e => e.ProductId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<PurchaseOrderHeader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("purchase_order_header");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasComment("เลขที่เอกสาร")
                .HasColumnName("document_no");
            entity.Property(e => e.DocumentStatus)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasComment("สถานะ")
                .HasColumnName("document_status");
            entity.Property(e => e.FromAddress)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("from_address");
            entity.Property(e => e.FromEmail)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("from_email");
            entity.Property(e => e.FromName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("from_name");
            entity.Property(e => e.FromTel)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("from_tel");
            entity.Property(e => e.PlatformValue)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("platform_value");
            entity.Property(e => e.Remark)
                .HasMaxLength(1000)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("remark");
            entity.Property(e => e.ShippingValue)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasComment("Dropdown select")
                .HasColumnName("shipping_value");
            entity.Property(e => e.ToAddress)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("to_address");
            entity.Property(e => e.ToEmail)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("to_email");
            entity.Property(e => e.ToName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("to_name");
            entity.Property(e => e.ToTel)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("to_tel");
            entity.Property(e => e.TrackingNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("tracking_number");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorFlagAddress)
                .HasDefaultValueSql("'NULL'")
                .HasComment("ใช้ที่อยู่เดียวกับ vendor master")
                .HasColumnName("vendor_flag_address");
            entity.Property(e => e.VendorId)
                .HasDefaultValueSql("'NULL'")
                .HasComment("บริษัทคู่ค้า")
                .HasColumnType("int(11)")
                .HasColumnName("vendor_id");
        });

        modelBuilder.Entity<PurchaseOrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("purchase_order_item");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("amount");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.HeaderId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("header_id");
            entity.Property(e => e.ProductId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.Remark)
                .HasMaxLength(1000)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("remark");
            entity.Property(e => e.StatusValue)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("status_value");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<PurchaseOrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("purchase_order_status");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ClassColor)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("class_color");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("stock");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("amount");
            entity.Property(e => e.BinId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("bin_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("is_active");
            entity.Property(e => e.LocationId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("location_id");
            entity.Property(e => e.ProductId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.SheftId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("sheft_id");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("status");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.VendorId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("vendor_id");
        });

        modelBuilder.Entity<StockManualLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("stock_manual_log");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AmountChange)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("amount_change");
            entity.Property(e => e.AmountOld)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("amount_old");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Reason)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("reason");
            entity.Property(e => e.StockId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("stock_id");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("type");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<SysAccess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sys_access");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AccessDetail)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("access_detail");
            entity.Property(e => e.AccessFunction)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("access_function");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("ip_address");
            entity.Property(e => e.MacAddress)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("mac_address");
            entity.Property(e => e.MenuCode)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("menu_code");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("transaction_date");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("user_name");
            entity.Property(e => e.VendorCode)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("vendor_code");
        });

        modelBuilder.Entity<SysApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PRIMARY");

            entity.ToTable("sys_application");

            entity.Property(e => e.ApplicationId)
                .HasColumnType("int(11)")
                .HasColumnName("application_id");
            entity.Property(e => e.ApplicationDescription)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("application_description");
            entity.Property(e => e.ApplicationName)
                .HasMaxLength(100)
                .HasColumnName("application_name");
        });

        modelBuilder.Entity<SysDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sys_document");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DocumentDay)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("document_day");
            entity.Property(e => e.DocumentMonth)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("document_month");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("document_no");
            entity.Property(e => e.DocumentYear)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("document_year");
            entity.Property(e => e.ProductId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("product_id");
            entity.Property(e => e.Tpye)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("tpye");
            entity.Property(e => e.VendorId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("vendor_id");
        });

        modelBuilder.Entity<SysMenuGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sys_menu_group");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ApplicationName)
                .HasMaxLength(100)
                .HasColumnName("application_name");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.MenuCode)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("menu_code");
            entity.Property(e => e.MenuDrsciption)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("menu_drsciption");
            entity.Property(e => e.MenuIcon)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("menu_icon");
            entity.Property(e => e.MenuName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("menu_name");
            entity.Property(e => e.MenuSequence)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("menu_sequence");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<SysMenuList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sys_menu_list");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("action");
            entity.Property(e => e.ApplicationName)
                .HasMaxLength(100)
                .HasColumnName("application_name");
            entity.Property(e => e.Controller)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("controller");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("is_active");
            entity.Property(e => e.MenuCode)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("menu_code");
            entity.Property(e => e.MenuDesciption)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("menu_desciption");
            entity.Property(e => e.MenuGroup)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("menu_group");
            entity.Property(e => e.MenuIcon)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("menu_icon");
            entity.Property(e => e.MenuName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("menu_name");
            entity.Property(e => e.MenuSequence)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("menu_sequence");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<SysRoleGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sys_role_group");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("name");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("value");
        });

        modelBuilder.Entity<SysRoleList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sys_role_list");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.MenuGroupId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("menu_group_id");
            entity.Property(e => e.MenuId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("menu_id");
            entity.Property(e => e.RoleGroupId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("role_group_id");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<SysUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("sys_users");

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.ApplicationName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("application_name");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("create_by");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("email");
            entity.Property(e => e.FirstNameEn)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("first_name_en");
            entity.Property(e => e.FirstNameTh)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("first_name_th");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("is_active");
            entity.Property(e => e.LastNameEn)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("last_name_en");
            entity.Property(e => e.LastNameTh)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("last_name_th");
            entity.Property(e => e.Mobile)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("mobile");
            entity.Property(e => e.Role)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("role");
            entity.Property(e => e.SecretKey)
                .HasMaxLength(200)
                .HasColumnName("secretKey");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("update_by");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("user_name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("user_password");
            entity.Property(e => e.VenderId)
                .HasColumnType("int(11)")
                .HasColumnName("vender_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
