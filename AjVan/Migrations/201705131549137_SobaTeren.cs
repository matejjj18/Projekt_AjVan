namespace AjVan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SobaTeren : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sobe", "TerenId", c => c.Long(nullable: false));
            CreateIndex("dbo.Sobe", "TerenId");
            AddForeignKey("dbo.Sobe", "TerenId", "dbo.Tereni", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sobe", "TerenId", "dbo.Tereni");
            DropIndex("dbo.Sobe", new[] { "TerenId" });
            DropColumn("dbo.Sobe", "TerenId");
        }
    }
}
