namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Heroes", "XP", c => c.Int(nullable: false));
            AddColumn("dbo.Heroes", "Level", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Heroes", "Level");
            DropColumn("dbo.Heroes", "XP");
        }
    }
}
