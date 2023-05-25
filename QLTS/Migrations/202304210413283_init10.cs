namespace QLTS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MinuteAssetInventory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfMinutesId = c.Int(nullable: false),
                        AtEstablishment = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        EnglishName = c.String(),
                        Initials = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        AtCreate = c.DateTime(),
                        AtUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Staff", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.StaffId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MinuteAssetInventory", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.MinuteAssetInventory", "DepartmentId", "dbo.Department");
            DropIndex("dbo.MinuteAssetInventory", new[] { "StaffId" });
            DropIndex("dbo.MinuteAssetInventory", new[] { "DepartmentId" });
            DropTable("dbo.MinuteAssetInventory");
        }
    }
}
