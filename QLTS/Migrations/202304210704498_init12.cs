namespace QLTS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ErrorReport", "AssetId", "dbo.Asset");
            DropForeignKey("dbo.ErrorReport", "RoomId", "dbo.Room");
            DropForeignKey("dbo.ErrorReport", "StaffId", "dbo.Staff");
            DropIndex("dbo.ErrorReport", new[] { "StaffId" });
            DropIndex("dbo.ErrorReport", new[] { "AssetId" });
            DropIndex("dbo.ErrorReport", new[] { "RoomId" });
            AddColumn("dbo.AssetDetail", "RoomId", c => c.Int(nullable: false));
            DropTable("dbo.ErrorReport");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ErrorReport",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StaffId = c.Int(nullable: false),
                        AssetId = c.Int(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.AssetDetail", "RoomId");
            CreateIndex("dbo.ErrorReport", "RoomId");
            CreateIndex("dbo.ErrorReport", "AssetId");
            CreateIndex("dbo.ErrorReport", "StaffId");
            AddForeignKey("dbo.ErrorReport", "StaffId", "dbo.Staff", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ErrorReport", "RoomId", "dbo.Room", "Id");
            AddForeignKey("dbo.ErrorReport", "AssetId", "dbo.Asset", "Id", cascadeDelete: true);
        }
    }
}
