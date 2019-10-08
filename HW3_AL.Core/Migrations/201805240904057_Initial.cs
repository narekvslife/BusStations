namespace HW3_AL.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavouriteStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 15),
                        Station_Id = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stations", t => t.Station_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Station_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false, maxLength: 5),
                        FirstDTime = c.Int(nullable: false),
                        LastDTime = c.Int(nullable: false),
                        Interval = c.Int(nullable: false),
                        TimeToNextStop = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Surname = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RouteStations",
                c => new
                    {
                        Route_Id = c.Int(nullable: false),
                        Station_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Route_Id, t.Station_Id })
                .ForeignKey("dbo.Routes", t => t.Route_Id, cascadeDelete: true)
                .ForeignKey("dbo.Stations", t => t.Station_Id, cascadeDelete: true)
                .Index(t => t.Route_Id)
                .Index(t => t.Station_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavouriteStations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.FavouriteStations", "Station_Id", "dbo.Stations");
            DropForeignKey("dbo.RouteStations", "Station_Id", "dbo.Stations");
            DropForeignKey("dbo.RouteStations", "Route_Id", "dbo.Routes");
            DropIndex("dbo.RouteStations", new[] { "Station_Id" });
            DropIndex("dbo.RouteStations", new[] { "Route_Id" });
            DropIndex("dbo.FavouriteStations", new[] { "User_Id" });
            DropIndex("dbo.FavouriteStations", new[] { "Station_Id" });
            DropTable("dbo.RouteStations");
            DropTable("dbo.Users");
            DropTable("dbo.Routes");
            DropTable("dbo.Stations");
            DropTable("dbo.FavouriteStations");
        }
    }
}
