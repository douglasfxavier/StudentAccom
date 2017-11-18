namespace StudentAccom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_user_fields : DbMigration
    {
        public override void Up()
        {
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Company = c.String(maxLength: 100),
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
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Accommodations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        TypeAccom = c.Int(nullable: false),
                        Location = c.String(nullable: false),
                        RoomsNumber = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        TypeRent = c.Int(nullable: false),
                        CleaningFee = c.Single(nullable: false),
                        SecurityDeposit = c.Single(nullable: false),
                        LandlordID = c.String(),
                        Internet = c.Boolean(nullable: false),
                        Wifi = c.Boolean(nullable: false),
                        CableTV = c.Boolean(nullable: false),
                        TV = c.Boolean(nullable: false),
                        Doorman = c.Boolean(nullable: false),
                        Kitchen = c.Boolean(nullable: false),
                        IndividualBathroom = c.Boolean(nullable: false),
                        SharedBathroom = c.Boolean(nullable: false),
                        Heating = c.Boolean(nullable: false),
                        AirConditioning = c.Boolean(nullable: false),
                        WashingMachine = c.Boolean(nullable: false),
                        DryingMachine = c.Boolean(nullable: false),
                        Pool = c.Boolean(nullable: false),
                        Gym = c.Boolean(nullable: false),
                        Smoking = c.Boolean(nullable: false),
                        Pets = c.Boolean(nullable: false),
                        Parties = c.Boolean(nullable: false),
                        CommercialActivities = c.Boolean(nullable: false),
                        Comment = c.String(),
                        Status = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        ImageData = c.Binary(),
                        MimeType = c.String(),
                        Accommodation_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.Accommodations", t => t.Accommodation_ID)
                .Index(t => t.Accommodation_ID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Accommodations", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Images", "Accommodation_ID", "dbo.Accommodations");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Images", new[] { "Accommodation_ID" });
            DropIndex("dbo.Accommodations", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Images");
            DropTable("dbo.Accommodations");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
