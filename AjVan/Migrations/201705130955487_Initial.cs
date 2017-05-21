namespace AjVan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Komentari",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Sadrzaj = c.String(),
                        Vrijeme = c.DateTime(nullable: false),
                        KorisnikId = c.String(maxLength: 128),
                        SobaId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.KorisnikId)
                .ForeignKey("dbo.Sobe", t => t.SobaId, cascadeDelete: true)
                .Index(t => t.KorisnikId)
                .Index(t => t.SobaId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        KontaktBroj = c.String(),
                        IsSystemAdmin = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Sobe",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Naziv = c.String(),
                        Opis = c.String(),
                        Pocetak = c.DateTime(nullable: false),
                        Trajanje = c.Time(nullable: false, precision: 7),
                        AdminId = c.String(maxLength: 128),
                        SportId = c.Long(nullable: false),
                        MaksimalniBrojIgraca = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AdminId)
                .ForeignKey("dbo.Sportovi", t => t.SportId, cascadeDelete: true)
                .Index(t => t.AdminId)
                .Index(t => t.SportId);
            
            CreateTable(
                "dbo.Sportovi",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Kvartovi",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tereni",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Naziv = c.String(),
                        Opis = c.String(),
                        VrstaTerena = c.Int(nullable: false),
                        Cijena = c.Decimal(precision: 18, scale: 2),
                        KvartId = c.Long(nullable: false),
                        GeoSirina = c.Decimal(precision: 18, scale: 2),
                        GeoDuzina = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kvartovi", t => t.KvartId, cascadeDelete: true)
                .Index(t => t.KvartId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.KorisniciSobe",
                c => new
                    {
                        KorisnikId = c.String(nullable: false, maxLength: 128),
                        SobaId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.KorisnikId, t.SobaId })
                .ForeignKey("dbo.AspNetUsers", t => t.KorisnikId, cascadeDelete: true)
                .ForeignKey("dbo.Sobe", t => t.SobaId, cascadeDelete: true)
                .Index(t => t.KorisnikId)
                .Index(t => t.SobaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Tereni", "KvartId", "dbo.Kvartovi");
            DropForeignKey("dbo.Komentari", "SobaId", "dbo.Sobe");
            DropForeignKey("dbo.KorisniciSobe", "SobaId", "dbo.Sobe");
            DropForeignKey("dbo.KorisniciSobe", "KorisnikId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sobe", "SportId", "dbo.Sportovi");
            DropForeignKey("dbo.Sobe", "AdminId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Komentari", "KorisnikId", "dbo.AspNetUsers");
            DropIndex("dbo.KorisniciSobe", new[] { "SobaId" });
            DropIndex("dbo.KorisniciSobe", new[] { "KorisnikId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Tereni", new[] { "KvartId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Sobe", new[] { "SportId" });
            DropIndex("dbo.Sobe", new[] { "AdminId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Komentari", new[] { "SobaId" });
            DropIndex("dbo.Komentari", new[] { "KorisnikId" });
            DropTable("dbo.KorisniciSobe");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tereni");
            DropTable("dbo.Kvartovi");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Sportovi");
            DropTable("dbo.Sobe");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Komentari");
        }
    }
}
