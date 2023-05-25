using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QLTS.Models
{
    public class QLTSDbContext : DbContext
    {
        public QLTSDbContext() : base("name=DefaultConnection") { }

        public DbSet<AreaModel> Areas { get; set; }
        public DbSet<StaffModel> Staffs { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<UnitModel> Units { get; set; }
        public DbSet<SupplierModel> Suppliers { get; set; }
        public DbSet<AssetModel> Assets { get; set; }
        public DbSet<RoomModel> Rooms { get; set; }
        public DbSet<ErrorReportModel> ErrorReports { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }
        public DbSet<AccessoryModel> Accessories { get; set; }
        public DbSet<AssetDetailModel> AssetDetails { get; set; }
        /*public DbSet<MinuteAssetDetailModel> MinuteAssetDetails { get; set; }
        public DbSet<MinuteAssetMaintenanceModel> MinuteAssetMaintenances { get; set; }
        public DbSet<MinuteAssetLiquidationModel> MinuteAssetLiquidations { get; set; }*/
        public DbSet<MinuteAssetDeliveryReceiveModel> MinuteAssetDeliveryReceives { get; set; }
        public DbSet<MinuteADRAssetDetailModel> MinuteADRAssetDetails { get; set; }
        public DbSet<AutoUpdateAssetDetailModel> AutoUpdateAssetDetails { get; set; }
        public DbSet<MinuteEnterAssetAtBeginModel> MinuteEnterAssetAtBegins { get; set; }
        public DbSet<MinuteEABAssetDetailModel> MinuteEABAssetDetails { get; set; }
        public DbSet<MinuteAssetInventoryModel> MinuteAssetInventories { get; set; }
        public DbSet<MinuteAIAssetDetailModel> MinuteAIAssetDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*modelBuilder.Properties<DateTime>()
                .Configure(c => c.HasColumnType("date"));*/
            modelBuilder.Entity<SupplierModel>()
                .Property(e => e.TaxCode)
                .IsUnicode(false);
            modelBuilder.Entity<SupplierModel>()
                .Property(e => e.Phone)
                .IsUnicode(false);
            modelBuilder.Entity<SupplierModel>()
                .Property(e => e.Email)
                .IsUnicode(false);
            modelBuilder.Entity<AssetModel>()
                .Property(e => e.QualityStatus)
                .HasPrecision(18, 0);
            modelBuilder.Entity<AssetModel>()
                .Property(e => e.DepreciationRate)
                .HasPrecision(18, 0);
            modelBuilder.Entity<AccessoryModel>()
                .Property(e => e.Value)
                .HasPrecision(18, 0);
            modelBuilder.Entity<AssetDetailModel>()
                .Property(e => e.Value)
                .HasPrecision(18, 0);
            modelBuilder.Entity<MinuteAssetDeliveryReceiveModel>()
                .HasRequired(m => m.ReceiveDepartment)
                .WithMany()
                .HasForeignKey(m => m.ReceiveDepartmentId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<MinuteAssetDeliveryReceiveModel>()
                .HasRequired(m => m.DeliveryDepartment)
                .WithMany()
                .HasForeignKey(m => m.DeliveryDepartmentId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<MinuteAssetDeliveryReceiveModel>()
                .HasRequired(m => m.ReceiveStaff)
                .WithMany()
                .HasForeignKey(m => m.ReceiveStaffId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<MinuteAssetDeliveryReceiveModel>()
                .HasRequired(m => m.DeliveryStaff)
                .WithMany()
                .HasForeignKey(m => m.DeliveryStaffId)
                .WillCascadeOnDelete(false);
        }
    }
}