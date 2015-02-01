namespace BetterLife.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "PersonProfile_PersonProfileId", "dbo.PersonProfiles");
            DropIndex("dbo.Books", new[] { "PersonProfile_PersonProfileId" });
            CreateTable(
                "dbo.PersonProfileBooks",
                c => new
                    {
                        PersonProfile_PersonProfileId = c.Int(nullable: false),
                        Book_BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonProfile_PersonProfileId, t.Book_BookId })
                .ForeignKey("dbo.PersonProfiles", t => t.PersonProfile_PersonProfileId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_BookId, cascadeDelete: true)
                .Index(t => t.PersonProfile_PersonProfileId)
                .Index(t => t.Book_BookId);
            
            DropColumn("dbo.Books", "PersonProfile_PersonProfileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "PersonProfile_PersonProfileId", c => c.Int());
            DropForeignKey("dbo.PersonProfileBooks", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.PersonProfileBooks", "PersonProfile_PersonProfileId", "dbo.PersonProfiles");
            DropIndex("dbo.PersonProfileBooks", new[] { "Book_BookId" });
            DropIndex("dbo.PersonProfileBooks", new[] { "PersonProfile_PersonProfileId" });
            DropTable("dbo.PersonProfileBooks");
            CreateIndex("dbo.Books", "PersonProfile_PersonProfileId");
            AddForeignKey("dbo.Books", "PersonProfile_PersonProfileId", "dbo.PersonProfiles", "PersonProfileId");
        }
    }
}
