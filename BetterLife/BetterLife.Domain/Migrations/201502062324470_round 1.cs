namespace BetterLife.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class round1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "SecPersonProfileId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "SecPersonProfileId");
        }
    }
}
