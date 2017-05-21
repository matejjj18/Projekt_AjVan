namespace AjVan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TerenChangeDecimalToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tereni", "GeoSirina", c => c.Double());
            AlterColumn("dbo.Tereni", "GeoDuzina", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tereni", "GeoDuzina", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Tereni", "GeoSirina", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
