namespace QLTS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AutoUpdateAssetStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssetDetailId = c.Int(nullable: false),
                        AssetStatusId = c.Int(nullable: false),
                        AtImplementation = c.DateTime(nullable: false),
                        isUsed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AutoUpdateAssetStatus");
        }
    }
}
