namespace QLTS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssetDetail", t => t.AssetDetailId, cascadeDelete: true)
                .ForeignKey("dbo.MinuteAssetDeliveryReceive", t => t.MinuteAssetDeliveryReceiveId, cascadeDelete: true)
                .Index(t => t.MinuteAssetDeliveryReceiveId)
                .Index(t => t.AssetDetailId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MinuteAssetDeliveryReceiveAssetDetail", "MinuteAssetDeliveryReceiveId", "dbo.MinuteAssetDeliveryReceive");
            DropForeignKey("dbo.MinuteAssetDeliveryReceiveAssetDetail", "AssetDetailId", "dbo.AssetDetail");
            DropIndex("dbo.MinuteAssetDeliveryReceiveAssetDetail", new[] { "AssetDetailId" });
            DropIndex("dbo.MinuteAssetDeliveryReceiveAssetDetail", new[] { "MinuteAssetDeliveryReceiveId" });
            DropTable("dbo.MinuteAssetDeliveryReceiveAssetDetail");
        }
    }
}
