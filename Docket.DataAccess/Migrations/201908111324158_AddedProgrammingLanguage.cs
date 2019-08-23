namespace Docket.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProgrammingLanguage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProgrammingLanguage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Client", "FavoriteLanguageId", c => c.Int());
            CreateIndex("dbo.Client", "FavoriteLanguageId");
            AddForeignKey("dbo.Client", "FavoriteLanguageId", "dbo.ProgrammingLanguage", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Client", "FavoriteLanguageId", "dbo.ProgrammingLanguage");
            DropIndex("dbo.Client", new[] { "FavoriteLanguageId" });
            DropColumn("dbo.Client", "FavoriteLanguageId");
            DropTable("dbo.ProgrammingLanguage");
        }
    }
}
