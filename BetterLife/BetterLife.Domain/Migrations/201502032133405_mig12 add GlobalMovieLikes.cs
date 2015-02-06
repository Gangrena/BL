namespace BetterLife.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig12addGlobalMovieLikes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "PersonProfile_PersonProfileId", "dbo.PersonProfiles");
            DropIndex("dbo.Movies", new[] { "PersonProfile_PersonProfileId" });
            CreateTable(
                "dbo.GlobalMovieLikes",
                c => new
                    {
                        GlobalMovieLikeId = c.Int(nullable: false, identity: true),
                        GlobalMovie_GlobalMovieId = c.Int(),
                        PersonProfile_PersonProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.GlobalMovieLikeId)
                .ForeignKey("dbo.GlobalMovies", t => t.GlobalMovie_GlobalMovieId)
                .ForeignKey("dbo.PersonProfiles", t => t.PersonProfile_PersonProfileId)
                .Index(t => t.GlobalMovie_GlobalMovieId)
                .Index(t => t.PersonProfile_PersonProfileId);
            
            CreateTable(
                "dbo.GlobalMovies",
                c => new
                    {
                        GlobalMovieId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        DataId = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GlobalMovieId);
            
            AlterColumn("dbo.GlobalBooks", "DataId", c => c.String(nullable: false));
            DropTable("dbo.Movies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DataId = c.String(),
                        PersonId = c.Int(nullable: false),
                        PersonProfile_PersonProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.MovieId);
            
            DropForeignKey("dbo.GlobalMovieLikes", "PersonProfile_PersonProfileId", "dbo.PersonProfiles");
            DropForeignKey("dbo.GlobalMovieLikes", "GlobalMovie_GlobalMovieId", "dbo.GlobalMovies");
            DropIndex("dbo.GlobalMovieLikes", new[] { "PersonProfile_PersonProfileId" });
            DropIndex("dbo.GlobalMovieLikes", new[] { "GlobalMovie_GlobalMovieId" });
            AlterColumn("dbo.GlobalBooks", "DataId", c => c.String());
            DropTable("dbo.GlobalMovies");
            DropTable("dbo.GlobalMovieLikes");
            CreateIndex("dbo.Movies", "PersonProfile_PersonProfileId");
            AddForeignKey("dbo.Movies", "PersonProfile_PersonProfileId", "dbo.PersonProfiles", "PersonProfileId");
        }
    }
}
