namespace AutoNuoma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RentCost = c.Double(nullable: false),
                        Manufacturer = c.String(),
                        Model = c.String(),
                        RegistrationNumber = c.String(),
                        FirstRegistration = c.DateTime(nullable: false),
                        FuelType = c.Int(nullable: false),
                        OdometerReading = c.Int(nullable: false),
                        Power = c.Int(nullable: false),
                        SeatCount = c.Int(nullable: false),
                        WindowType = c.Int(nullable: false),
                        DoorCount = c.Int(nullable: false),
                        BodyType = c.Int(nullable: false),
                        GearboxType = c.Int(nullable: false),
                        FuelConsumption = c.Double(nullable: false),
                        HasNavigationSystem = c.Boolean(nullable: false),
                        HasChildChair = c.Boolean(nullable: false),
                        HasAC = c.Boolean(nullable: false),
                        HasUSB = c.Boolean(nullable: false),
                        IsDamaged = c.Boolean(nullable: false),
                        RentPointId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RentPoints", t => t.RentPointId, cascadeDelete: true)
                .Index(t => t.RentPointId);
            
            CreateTable(
                "dbo.RentPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Country = c.String(),
                        Name = c.String(),
                        PhoneNumber = c.String(maxLength: 20),
                        Longitude = c.Single(nullable: false),
                        Latitude = c.Single(nullable: false),
                        BeginningOfWork = c.String(),
                        EndingOfWork = c.String(),
                        WorkingDays = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OdometerReadingAtStart = c.Int(nullable: false),
                        OdometerReadingAtEnd = c.Int(nullable: false),
                        StartingDate = c.DateTime(nullable: false),
                        EndingDate = c.DateTime(nullable: false),
                        Deposit = c.Double(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        IsReturned = c.Boolean(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        SubscriptionId = c.Int(),
                        UserId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Subscriptions", t => t.SubscriptionId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.SubscriptionId)
                .Index(t => t.UserId)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Frequency = c.Int(nullable: false),
                        BeginningDate = c.DateTime(nullable: false),
                        EndingDate = c.DateTime(nullable: false),
                        Sum = c.Double(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Type = c.Int(nullable: false),
                        Level = c.Int(),
                        IdentityCode = c.String(),
                        Birthday = c.DateTime(),
                        PhoneNumber = c.String(maxLength: 20),
                        Address = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Nationality = c.String(),
                        HasNewsletter = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CaseNumber = c.Int(nullable: false),
                        IsCaseClosed = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                        ChatId = c.Int(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.ChatId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ChatId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Title = c.String(),
                        Text = c.String(),
                        Evaluation = c.Int(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        Request_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Requests", t => t.Request_Id)
                .Index(t => t.Request_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Request_Id", "dbo.Requests");
            DropForeignKey("dbo.Requests", "UserId", "dbo.Users");
            DropForeignKey("dbo.Requests", "SubscriptionId", "dbo.Subscriptions");
            DropForeignKey("dbo.Subscriptions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Chats", "UserId", "dbo.Users");
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropForeignKey("dbo.Requests", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Cars", "RentPointId", "dbo.RentPoints");
            DropIndex("dbo.Reviews", new[] { "Request_Id" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "ChatId" });
            DropIndex("dbo.Chats", new[] { "UserId" });
            DropIndex("dbo.Subscriptions", new[] { "UserId" });
            DropIndex("dbo.Requests", new[] { "CarId" });
            DropIndex("dbo.Requests", new[] { "UserId" });
            DropIndex("dbo.Requests", new[] { "SubscriptionId" });
            DropIndex("dbo.Cars", new[] { "RentPointId" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Messages");
            DropTable("dbo.Chats");
            DropTable("dbo.Users");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.Requests");
            DropTable("dbo.RentPoints");
            DropTable("dbo.Cars");
        }
    }
}
