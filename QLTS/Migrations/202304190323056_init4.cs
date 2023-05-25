namespace QLTS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MinuteAssetDeliveryReceiveAssetDetail", newName: "MinuteADRAssetDetail");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MinuteADRAssetDetail", newName: "MinuteAssetDeliveryReceiveAssetDetail");
        }
    }
}
