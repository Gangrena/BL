namespace BetterLife.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig61 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "DataId", c => c.String());
            AddColumn("dbo.Movies", "DataId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "DataId");
            DropColumn("dbo.Books", "DataId");
        }
    }
}
