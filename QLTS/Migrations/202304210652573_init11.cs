namespace QLTS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init11 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AutoUpdateAssetStatus", newName: "AutoUpdateAssetDetail");
            AddColumn("dbo.AutoUpdateAssetDetail", "RoomId", c => c.Int(nullable: false));
            AddColumn("dbo.MinuteADRAssetDetail", "RoomId", c => c.Int(nullable: false));
            AddColumn("dbo.MinuteAIAssetDetail", "RoomId", c => c.Int(nullable: false));
            AddColumn("dbo.MinuteEABAssetDetail", "RoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.MinuteADRAssetDetail", "RoomId");
            CreateIndex("dbo.MinuteAIAssetDetail", "RoomId");
            CreateIndex("dbo.MinuteEABAssetDetail", "RoomId");
            AddForeignKey("dbo.MinuteADRAssetDetail", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MinuteAIAssetDetail", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MinuteEABAssetDetail", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MinuteEABAssetDetail", "RoomId", "dbo.Room");
            DropForeignKey("dbo.MinuteAIAssetDetail", "RoomId", "dbo.Room");
            DropForeignKey("dbo.MinuteADRAssetDetail", "RoomId", "dbo.Room");
            DropIndex("dbo.MinuteEABAssetDetail", new[] { "RoomId" });
            DropIndex("dbo.MinuteAIAssetDetail", new[] { "RoomId" });
            DropIndex("dbo.MinuteADRAssetDetail", new[] { "RoomId" });
            DropColumn("dbo.MinuteEABAssetDetail", "RoomId");
            DropColumn("dbo.MinuteAIAssetDetail", "RoomId");
            DropColumn("dbo.MinuteADRAssetDetail", "RoomId");
            DropColumn("dbo.AutoUpdateAssetDetail", "RoomId");
            RenameTable(name: "dbo.AutoUpdateAssetDetail", newName: "AutoUpdateAssetStatus");
        }
    }
}
