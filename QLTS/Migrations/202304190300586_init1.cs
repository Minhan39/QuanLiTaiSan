namespace QLTS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MinuteAssetDeliveryReceiveAssetDetail", "AssetDetailId", "dbo.AssetDetail");
            DropForeignKey("dbo.MinuteAssetDeliveryReceiveAssetDetail", "MinuteAssetDeliveryReceiveId", "dbo.MinuteAssetDeliveryReceive");
            DropIndex("dbo.MinuteAssetDeliveryReceiveAssetDetail", new[] { "MinuteAssetDeliveryReceiveId" });
            DropIndex("dbo.MinuteAssetDeliveryReceiveAssetDetail", new[] { "AssetDetailId" });
            DropTable("dbo.MinuteAssetDeliveryReceiveAssetDetail");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MinuteAssetDeliveryReceiveAssetDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinuteAssetDeliveryReceiveId = c.Int(nullable: false),
                        AssetDetailId = c.Int(nullable: false),
                        AssetStatusId = c.Int(nullable: false),
                        AtCreate = c.DateTime(),
                        AtUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.MinuteAssetDeliveryReceiveAssetDetail", "AssetDetailId");
            CreateIndex("dbo.MinuteAssetDeliveryReceiveAssetDetail", "MinuteAssetDeliveryReceiveId");
            AddForeignKey("dbo.MinuteAssetDeliveryReceiveAssetDetail", "MinuteAssetDeliveryReceiveId", "dbo.MinuteAssetDeliveryReceive", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MinuteAssetDeliveryReceiveAssetDetail", "AssetDetailId", "dbo.AssetDetail", "Id", cascadeDelete: true);
        }
    }
}
