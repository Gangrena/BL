namespace BetterLife.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastfight : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PersonProfileMessages", "PersonProfile_PersonProfileId", "dbo.PersonProfiles");
            AddColumn("dbo.PersonProfileMessages", "SecPersonProfile_PersonProfileId", c => c.Int());
            AddColumn("dbo.PersonProfileMessages", "PersonProfile_PersonProfileId1", c => c.Int());
            CreateIndex("dbo.PersonProfileMessages", "SecPersonProfile_PersonProfileId");
            CreateIndex("dbo.PersonProfileMessages", "PersonProfile_PersonProfileId1");
            AddForeignKey("dbo.PersonProfileMessages", "SecPersonProfile_PersonProfileId", "dbo.PersonProfiles", "PersonProfileId");
            AddForeignKey("dbo.PersonProfileMessages", "PersonProfile_PersonProfileId1", "dbo.PersonProfiles", "PersonProfileId");
            DropColumn("dbo.PersonProfileMessages", "SecPersonProfile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PersonProfileMessages", "SecPersonProfile", c => c.Int(nullable: false));
            DropForeignKey("dbo.PersonProfileMessages", "PersonProfile_PersonProfileId1", "dbo.PersonProfiles");
            DropForeignKey("dbo.PersonProfileMessages", "SecPersonProfile_PersonProfileId", "dbo.PersonProfiles");
            DropIndex("dbo.PersonProfileMessages", new[] { "PersonProfile_PersonProfileId1" });
            DropIndex("dbo.PersonProfileMessages", new[] { "SecPersonProfile_PersonProfileId" });
            DropColumn("dbo.PersonProfileMessages", "PersonProfile_PersonProfileId1");
            DropColumn("dbo.PersonProfileMessages", "SecPersonProfile_PersonProfileId");
            AddForeignKey("dbo.PersonProfileMessages", "PersonProfile_PersonProfileId", "dbo.PersonProfiles", "PersonProfileId");
        }
    }
}
