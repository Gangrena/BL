namespace BetterLife.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class round2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Description", c => c.String());
        }
    }
}
