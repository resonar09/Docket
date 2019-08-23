namespace Docket.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAddress1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Client", "Zip", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Client", "Zip", c => c.String(nullable: false));
        }
    }
}
