namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Heroes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Image_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Image_Id)
                .Index(t => t.Image_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Img = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Powers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Image_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Image_Id)
                .Index(t => t.Image_Id);
            
            CreateTable(
                "dbo.PowerHeroes",
                c => new
                    {
                        Power_Id = c.Guid(nullable: false),
                        Hero_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Power_Id, t.Hero_Id })
                .ForeignKey("dbo.Powers", t => t.Power_Id, cascadeDelete: true)
                .ForeignKey("dbo.Heroes", t => t.Hero_Id, cascadeDelete: true)
                .Index(t => t.Power_Id)
                .Index(t => t.Hero_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Powers", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.PowerHeroes", "Hero_Id", "dbo.Heroes");
            DropForeignKey("dbo.PowerHeroes", "Power_Id", "dbo.Powers");
            DropForeignKey("dbo.Heroes", "Image_Id", "dbo.Images");
            DropIndex("dbo.PowerHeroes", new[] { "Hero_Id" });
            DropIndex("dbo.PowerHeroes", new[] { "Power_Id" });
            DropIndex("dbo.Powers", new[] { "Image_Id" });
            DropIndex("dbo.Heroes", new[] { "Image_Id" });
            DropTable("dbo.PowerHeroes");
            DropTable("dbo.Powers");
            DropTable("dbo.Images");
            DropTable("dbo.Heroes");
        }
    }
}
