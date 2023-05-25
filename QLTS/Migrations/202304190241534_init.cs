namespace QLTS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MinuteAssetDetail", "AssetDetailId", "dbo.AssetDetail");
            DropForeignKey("dbo.Minute", "TypeOfMinutesId", "dbo.TypeOfMinutesModels");
            DropForeignKey("dbo.MinuteAssetDetail", "MinuteId", "dbo.Minute");
            DropForeignKey("dbo.MinuteAssetDetail", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.MinuteAssetInventory", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.MinuteAssetInventory", "MinuteId", "dbo.Minute");
            DropForeignKey("dbo.MinuteAssetLiquidation", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.MinuteAssetLiquidation", "MinuteId", "dbo.Minute");
            DropForeignKey("dbo.MinuteAssetMaintenance", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.MinuteAssetMaintenance", "MinuteId", "dbo.Minute");
            DropForeignKey("dbo.MinuteEnterAssetAtBegin", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.MinuteEnterAssetAtBegin", "MinuteId", "dbo.Minute");
            DropIndex("dbo.MinuteAssetDetail", new[] { "MinuteId" });
            DropIndex("dbo.MinuteAssetDetail", new[] { "AssetDetailId" });
            DropIndex("dbo.MinuteAssetDetail", new[] { "StaffId" });
            DropIndex("dbo.Minute", new[] { "TypeOfMinutesId" });
            DropIndex("dbo.MinuteAssetInventory", new[] { "MinuteId" });
            DropIndex("dbo.MinuteAssetInventory", new[] { "DepartmentId" });
            DropIndex("dbo.MinuteAssetLiquidation", new[] { "MinuteId" });
            DropIndex("dbo.MinuteAssetLiquidation", new[] { "DepartmentId" });
            DropIndex("dbo.MinuteAssetMaintenance", new[] { "MinuteId" });
            DropIndex("dbo.MinuteAssetMaintenance", new[] { "DepartmentId" });
            DropIndex("dbo.MinuteEnterAssetAtBegin", new[] { "MinuteId" });
            DropIndex("dbo.MinuteEnterAssetAtBegin", new[] { "DepartmentId" });
            DropTable("dbo.MinuteAssetDetail");
            DropTable("dbo.Minute");
            DropTable("dbo.TypeOfMinutesModels");
            DropTable("dbo.MinuteAssetInventory");
            DropTable("dbo.MinuteAssetLiquidation");
            DropTable("dbo.MinuteAssetMaintenance");
            DropTable("dbo.MinuteEnterAssetAtBegin");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MinuteEnterAssetAtBegin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinuteId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        AtCreate = c.DateTime(),
                        AtUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MinuteAssetMaintenance",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinuteId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AtCreate = c.DateTime(),
                        AtUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MinuteAssetLiquidation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinuteId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyerName = c.String(maxLength: 100),
                        AtCreate = c.DateTime(),
                        AtUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MinuteAssetInventory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinuteId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        AtCreate = c.DateTime(),
                        AtUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeOfMinutesModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AtCreate = c.DateTime(),
                        AtUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Minute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfMinutesId = c.Int(nullable: false),
                        AtEstablishment = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        EnglishName = c.String(),
                        Initials = c.String(),
                        AtCreate = c.DateTime(),
                        AtUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MinuteAssetDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinuteId = c.Int(nullable: false),
                        AssetDetailId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        AtCreate = c.DateTime(),
                        AtUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.MinuteEnterAssetAtBegin", "DepartmentId");
            CreateIndex("dbo.MinuteEnterAssetAtBegin", "MinuteId");
            CreateIndex("dbo.MinuteAssetMaintenance", "DepartmentId");
            CreateIndex("dbo.MinuteAssetMaintenance", "MinuteId");
            CreateIndex("dbo.MinuteAssetLiquidation", "DepartmentId");
            CreateIndex("dbo.MinuteAssetLiquidation", "MinuteId");
            CreateIndex("dbo.MinuteAssetInventory", "DepartmentId");
            CreateIndex("dbo.MinuteAssetInventory", "MinuteId");
            CreateIndex("dbo.Minute", "TypeOfMinutesId");
            CreateIndex("dbo.MinuteAssetDetail", "StaffId");
            CreateIndex("dbo.MinuteAssetDetail", "AssetDetailId");
            CreateIndex("dbo.MinuteAssetDetail", "MinuteId");
            AddForeignKey("dbo.MinuteEnterAssetAtBegin", "MinuteId", "dbo.Minute", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MinuteEnterAssetAtBegin", "DepartmentId", "dbo.Department", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MinuteAssetMaintenance", "MinuteId", "dbo.Minute", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MinuteAssetMaintenance", "DepartmentId", "dbo.Department", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MinuteAssetLiquidation", "MinuteId", "dbo.Minute", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MinuteAssetLiquidation", "DepartmentId", "dbo.Department", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MinuteAssetInventory", "MinuteId", "dbo.Minute", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MinuteAssetInventory", "DepartmentId", "dbo.Department", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MinuteAssetDetail", "StaffId", "dbo.Staff", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MinuteAssetDetail", "MinuteId", "dbo.Minute", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Minute", "TypeOfMinutesId", "dbo.TypeOfMinutesModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MinuteAssetDetail", "AssetDetailId", "dbo.AssetDetail", "Id", cascadeDelete: true);
        }
    }
}
