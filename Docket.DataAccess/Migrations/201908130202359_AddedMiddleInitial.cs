namespace Docket.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMiddleInitial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Client", "MiddleInitial", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Client", "MiddleInitial");
        }
    }
}
