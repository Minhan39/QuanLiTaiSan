namespace QLTS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ErrorReport",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StaffId = c.Int(nullable: false),
                        AssetDetailId = c.Int(nullable: false),
                        RoomId = c.Int(),
                        Message = c.String(),
                        Image = c.Binary(),
                        Step = c.Int(nullable: false),
                        AtReport = c.DateTime(nullable: false),
                        AtReciver = c.DateTime(),
                        AtRepair = c.DateTime(),
                        AtEnd = c.DateTime(),
                        AtCreate = c.DateTime(),
                        AtUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssetDetail", t => t.AssetDetailId, cascadeDelete: true)
                .ForeignKey("dbo.Room", t => t.RoomId)
                .ForeignKey("dbo.Staff", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.StaffId)
                .Index(t => t.AssetDetailId)
                .Index(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ErrorReport", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.ErrorReport", "RoomId", "dbo.Room");
            DropForeignKey("dbo.ErrorReport", "AssetDetailId", "dbo.AssetDetail");
            DropIndex("dbo.ErrorReport", new[] { "RoomId" });
            DropIndex("dbo.ErrorReport", new[] { "AssetDetailId" });
            DropIndex("dbo.ErrorReport", new[] { "StaffId" });
            DropTable("dbo.ErrorReport");
        }
    }
}
