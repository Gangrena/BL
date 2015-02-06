namespace BetterLife.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig12bookdeleted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "PersonProfile_PersonProfileId", "dbo.PersonProfiles");
            DropIndex("dbo.Books", new[] { "PersonProfile_PersonProfileId" });
            DropTable("dbo.Books");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        AuthorFirstName = c.String(nullable: false),
                        AuthorLastName = c.String(nullable: false),
                        DataId = c.String(),
                        Created = c.DateTime(nullable: false),
                        PersonId = c.Int(nullable: false),
                        PersonProfile_PersonProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateIndex("dbo.Books", "PersonProfile_PersonProfileId");
            AddForeignKey("dbo.Books", "PersonProfile_PersonProfileId", "dbo.PersonProfiles", "PersonProfileId");
        }
    }
}
