namespace BetterLife.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photos", "PersonProfile_PersonProfileId", "dbo.PersonProfiles");
            DropForeignKey("dbo.GlobalMovieLikes", "PersonProfile_PersonProfileId", "dbo.PersonProfiles");
            DropForeignKey("dbo.Locations", "PersonProfile_PersonProfileId", "dbo.PersonProfiles");
            DropIndex("dbo.Photos", new[] { "PersonProfile_PersonProfileId" });
            DropIndex("dbo.GlobalMovieLikes", new[] { "PersonProfile_PersonProfileId" });
            DropIndex("dbo.Locations", new[] { "PersonProfile_PersonProfileId" });
            RenameColumn(table: "dbo.Photos", name: "PersonProfile_PersonProfileId", newName: "PersonProfileId");
            RenameColumn(table: "dbo.GlobalMovieLikes", name: "PersonProfile_PersonProfileId", newName: "PersonProfileId");
            RenameColumn(table: "dbo.Locations", name: "PersonProfile_PersonProfileId", newName: "PersonProfileId");
            AddColumn("dbo.GlobalMovies", "PersonProfileId", c => c.Int(nullable: false));
            AlterColumn("dbo.Photos", "PersonProfileId", c => c.Int(nullable: false));
            AlterColumn("dbo.GlobalMovieLikes", "PersonProfileId", c => c.Int(nullable: false));
            AlterColumn("dbo.Locations", "PersonProfileId", c => c.Int(nullable: false));
            CreateIndex("dbo.Photos", "PersonProfileId");
            CreateIndex("dbo.GlobalMovieLikes", "PersonProfileId");
            CreateIndex("dbo.Locations", "PersonProfileId");
            AddForeignKey("dbo.Photos", "PersonProfileId", "dbo.PersonProfiles", "PersonProfileId", cascadeDelete: true);
            AddForeignKey("dbo.GlobalMovieLikes", "PersonProfileId", "dbo.PersonProfiles", "PersonProfileId", cascadeDelete: true);
            AddForeignKey("dbo.Locations", "PersonProfileId", "dbo.PersonProfiles", "PersonProfileId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "PersonProfileId", "dbo.PersonProfiles");
            DropForeignKey("dbo.GlobalMovieLikes", "PersonProfileId", "dbo.PersonProfiles");
            DropForeignKey("dbo.Photos", "PersonProfileId", "dbo.PersonProfiles");
            DropIndex("dbo.Locations", new[] { "PersonProfileId" });
            DropIndex("dbo.GlobalMovieLikes", new[] { "PersonProfileId" });
            DropIndex("dbo.Photos", new[] { "PersonProfileId" });
            AlterColumn("dbo.Locations", "PersonProfileId", c => c.Int());
            AlterColumn("dbo.GlobalMovieLikes", "PersonProfileId", c => c.Int());
            AlterColumn("dbo.Photos", "PersonProfileId", c => c.Int());
            DropColumn("dbo.GlobalMovies", "PersonProfileId");
            RenameColumn(table: "dbo.Locations", name: "PersonProfileId", newName: "PersonProfile_PersonProfileId");
            RenameColumn(table: "dbo.GlobalMovieLikes", name: "PersonProfileId", newName: "PersonProfile_PersonProfileId");
            RenameColumn(table: "dbo.Photos", name: "PersonProfileId", newName: "PersonProfile_PersonProfileId");
            CreateIndex("dbo.Locations", "PersonProfile_PersonProfileId");
            CreateIndex("dbo.GlobalMovieLikes", "PersonProfile_PersonProfileId");
            CreateIndex("dbo.Photos", "PersonProfile_PersonProfileId");
            AddForeignKey("dbo.Locations", "PersonProfile_PersonProfileId", "dbo.PersonProfiles", "PersonProfileId");
            AddForeignKey("dbo.GlobalMovieLikes", "PersonProfile_PersonProfileId", "dbo.PersonProfiles", "PersonProfileId");
            AddForeignKey("dbo.Photos", "PersonProfile_PersonProfileId", "dbo.PersonProfiles", "PersonProfileId");
        }
    }
}
