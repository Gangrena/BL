namespace BetterLife.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Locations", "PersonProfile_PersonProfileId1", "dbo.PersonProfiles");
            DropForeignKey("dbo.Locations", "PersonProfile_PersonProfileId2", "dbo.PersonProfiles");
            DropIndex("dbo.Locations", new[] { "PersonProfile_PersonProfileId1" });
            DropIndex("dbo.Locations", new[] { "PersonProfile_PersonProfileId2" });
            DropColumn("dbo.Locations", "PersonProfile_PersonProfileId1");
            DropColumn("dbo.Locations", "PersonProfile_PersonProfileId2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "PersonProfile_PersonProfileId2", c => c.Int());
            AddColumn("dbo.Locations", "PersonProfile_PersonProfileId1", c => c.Int());
            CreateIndex("dbo.Locations", "PersonProfile_PersonProfileId2");
            CreateIndex("dbo.Locations", "PersonProfile_PersonProfileId1");
            AddForeignKey("dbo.Locations", "PersonProfile_PersonProfileId2", "dbo.PersonProfiles", "PersonProfileId");
            AddForeignKey("dbo.Locations", "PersonProfile_PersonProfileId1", "dbo.PersonProfiles", "PersonProfileId");
        }
    }
}
