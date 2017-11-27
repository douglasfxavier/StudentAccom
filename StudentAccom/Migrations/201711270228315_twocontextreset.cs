namespace StudentAccom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twocontextreset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accommodations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 500),
                        TypeAccom = c.Int(nullable: false),
                        Location = c.String(nullable: false, maxLength: 100),
                        RoomsNumber = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TypeRent = c.Int(nullable: false),
                        CleaningFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SecurityDeposit = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                    })
                .PrimaryKey(t => t.ID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Accommodation_ID", "dbo.Accommodations");
            DropIndex("dbo.Images", new[] { "Accommodation_ID" });
            DropTable("dbo.Images");
            DropTable("dbo.Accommodations");
        }
    }
}
