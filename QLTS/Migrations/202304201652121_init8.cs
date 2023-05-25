namespace QLTS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MinuteEnterAssetAtBegin",
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
            DropForeignKey("dbo.MinuteEnterAssetAtBegin", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.MinuteEnterAssetAtBegin", "DepartmentId", "dbo.Department");
            DropIndex("dbo.MinuteEnterAssetAtBegin", new[] { "StaffId" });
            DropIndex("dbo.MinuteEnterAssetAtBegin", new[] { "DepartmentId" });
            DropTable("dbo.MinuteEnterAssetAtBegin");
        }
    }
}
