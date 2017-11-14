namespace StudentAccom.Migrations.StudentAccomConfiguration
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
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        TypeAccom = c.Int(nullable: false),
                        Location = c.String(nullable: false),
                        RoomsNumber = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        TypeRent = c.Int(nullable: false),
                        CleaningFee = c.Single(nullable: false),
                        SecurityDeposit = c.Single(nullable: false),
                        LandlordID = c.Int(nullable: false),
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
