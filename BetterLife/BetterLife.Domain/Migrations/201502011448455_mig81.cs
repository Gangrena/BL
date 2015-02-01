namespace BetterLife.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig81 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TvShows", "PersonProfile_PersonProfileId", "dbo.PersonProfiles");
            DropForeignKey("dbo.PhotoAlbums", "PersonProfile_PersonProfileId", "dbo.PersonProfiles");
            DropForeignKey("dbo.Photos", "PhotoAlbum_PhotoAlbumId", "dbo.PhotoAlbums");
            DropIndex("dbo.TvShows", new[] { "PersonProfile_PersonProfileId" });
            DropIndex("dbo.Photos", new[] { "PhotoAlbum_PhotoAlbumId" });
            DropIndex("dbo.PhotoAlbums", new[] { "PersonProfile_PersonProfileId" });
            AddColumn("dbo.Books", "PersonId", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "PersonId", c => c.Int(nullable: false));
            DropColumn("dbo.Photos", "PhotoAlbum_PhotoAlbumId");
            DropTable("dbo.TvShows");
            DropTable("dbo.PhotoAlbums");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PhotoAlbums",
                c => new
                    {
                        PhotoAlbumId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Created = c.DateTime(nullable: false),
                        PersonProfile_PersonProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.PhotoAlbumId);
            
            CreateTable(
                "dbo.TvShows",
                c => new
                    {
                        TvShowId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PersonProfile_PersonProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.TvShowId);
            
            AddColumn("dbo.Photos", "PhotoAlbum_PhotoAlbumId", c => c.Int());
            DropColumn("dbo.Movies", "PersonId");
            DropColumn("dbo.Books", "PersonId");
            CreateIndex("dbo.PhotoAlbums", "PersonProfile_PersonProfileId");
            CreateIndex("dbo.Photos", "PhotoAlbum_PhotoAlbumId");
            CreateIndex("dbo.TvShows", "PersonProfile_PersonProfileId");
            AddForeignKey("dbo.Photos", "PhotoAlbum_PhotoAlbumId", "dbo.PhotoAlbums", "PhotoAlbumId");
            AddForeignKey("dbo.PhotoAlbums", "PersonProfile_PersonProfileId", "dbo.PersonProfiles", "PersonProfileId");
            AddForeignKey("dbo.TvShows", "PersonProfile_PersonProfileId", "dbo.PersonProfiles", "PersonProfileId");
        }
    }
}
