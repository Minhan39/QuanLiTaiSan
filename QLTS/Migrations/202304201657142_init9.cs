namespace QLTS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MinuteEABAssetDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinuteEnterAssetAtBeginId = c.Int(nullable: false),
                        AssetDetailId = c.Int(nullable: false),
                        AssetStatusId = c.Int(nullable: false),
                        AtCreate = c.DateTime(),
                        AtUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssetDetail", t => t.AssetDetailId, cascadeDelete: true)
                .ForeignKey("dbo.MinuteEnterAssetAtBegin", t => t.MinuteEnterAssetAtBeginId, cascadeDelete: true)
                .Index(t => t.MinuteEnterAssetAtBeginId)
                .Index(t => t.AssetDetailId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MinuteEABAssetDetail", "MinuteEnterAssetAtBeginId", "dbo.MinuteEnterAssetAtBegin");
            DropForeignKey("dbo.MinuteEABAssetDetail", "AssetDetailId", "dbo.AssetDetail");
            DropIndex("dbo.MinuteEABAssetDetail", new[] { "AssetDetailId" });
            DropIndex("dbo.MinuteEABAssetDetail", new[] { "MinuteEnterAssetAtBeginId" });
            DropTable("dbo.MinuteEABAssetDetail");
        }
    }
}
