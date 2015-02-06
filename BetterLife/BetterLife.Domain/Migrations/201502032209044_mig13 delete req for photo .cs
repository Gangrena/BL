namespace BetterLife.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig13deletereqforphoto : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GlobalBooks", "DataId", c => c.String());
            AlterColumn("dbo.GlobalMovies", "DataId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GlobalMovies", "DataId", c => c.String(nullable: false));
            AlterColumn("dbo.GlobalBooks", "DataId", c => c.String(nullable: false));
        }
    }
}
