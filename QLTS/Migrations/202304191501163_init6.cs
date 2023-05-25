namespace QLTS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.AutoUpdateAssetStatus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AutoUpdateAssetStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssetDetailId = c.Int(nullable: false),
                        AssetStatusId = c.Int(nullable: false),
                        AtApplicable = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
