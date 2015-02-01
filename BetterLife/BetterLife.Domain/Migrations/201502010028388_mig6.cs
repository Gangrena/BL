namespace BetterLife.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locations", "IsHomeTown", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Locations", "IsCurrentLocation", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Locations", "IsCurrentLocation", c => c.Boolean());
            AlterColumn("dbo.Locations", "IsHomeTown", c => c.Boolean());
        }
    }
}
