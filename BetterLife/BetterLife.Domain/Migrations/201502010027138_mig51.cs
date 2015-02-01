namespace BetterLife.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig51 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locations", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "Street", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "IsHomeTown", c => c.Boolean());
            AlterColumn("dbo.Locations", "IsCurrentLocation", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Locations", "IsCurrentLocation", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Locations", "IsHomeTown", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Locations", "Street", c => c.String());
            AlterColumn("dbo.Locations", "Country", c => c.String());
            AlterColumn("dbo.Locations", "City", c => c.String());
        }
    }
}
