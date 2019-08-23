namespace Docket.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Client", "Street", c => c.String(maxLength: 100));
            AddColumn("dbo.Client", "City", c => c.String(maxLength: 50));
            AddColumn("dbo.Client", "State", c => c.String(maxLength: 50));
            AddColumn("dbo.Client", "Zip", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Client", "Zip");
            DropColumn("dbo.Client", "State");
            DropColumn("dbo.Client", "City");
            DropColumn("dbo.Client", "Street");
        }
    }
}
