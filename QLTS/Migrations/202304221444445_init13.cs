namespace QLTS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AssetDetail", "AssetId", "dbo.Asset");
            DropIndex("dbo.AssetDetail", new[] { "AssetId" });
            AddColumn("dbo.AssetDetail", "AssetModel_Id", c => c.Int());
            CreateIndex("dbo.AssetDetail", "AssetModel_Id");
            AddForeignKey("dbo.AssetDetail", "AssetModel_Id", "dbo.Asset", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssetDetail", "AssetModel_Id", "dbo.Asset");
            DropIndex("dbo.AssetDetail", new[] { "AssetModel_Id" });
            DropColumn("dbo.AssetDetail", "AssetModel_Id");
            CreateIndex("dbo.AssetDetail", "AssetId");
            AddForeignKey("dbo.AssetDetail", "AssetId", "dbo.Asset", "Id", cascadeDelete: true);
        }
    }
}
