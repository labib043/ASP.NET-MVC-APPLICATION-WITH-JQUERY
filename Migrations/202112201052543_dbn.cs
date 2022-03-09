namespace Bikemvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BikeInfoes",
                c => new
                    {
                        BikeInfoId = c.Int(nullable: false, identity: true),
                        BikeName = c.String(nullable: false, maxLength: 8),
                    })
                .PrimaryKey(t => t.BikeInfoId);
            
            CreateTable(
                "dbo.ModelInfoes",
                c => new
                    {
                        ModelInfoId = c.Int(nullable: false, identity: true),
                        ModelName = c.String(nullable: false, maxLength: 8),
                        IsAvailable = c.Boolean(nullable: false),
                        ReleaseDate = c.DateTime(),
                        ModelPrice = c.Int(nullable: false),
                        Image = c.String(),
                        BikeInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ModelInfoId)
                .ForeignKey("dbo.BikeInfoes", t => t.BikeInfoId, cascadeDelete: true)
                .Index(t => t.BikeInfoId);
            
            CreateTable(
                "dbo.SignUps",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModelInfoes", "BikeInfoId", "dbo.BikeInfoes");
            DropIndex("dbo.ModelInfoes", new[] { "BikeInfoId" });
            DropTable("dbo.SignUps");
            DropTable("dbo.ModelInfoes");
            DropTable("dbo.BikeInfoes");
        }
    }
}
