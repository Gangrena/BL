namespace BetterLife.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "AuthorFirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "AuthorLastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "AuthorLastName", c => c.String());
            AlterColumn("dbo.Books", "AuthorFirstName", c => c.String());
            AlterColumn("dbo.Books", "Title", c => c.String());
        }
    }
}
