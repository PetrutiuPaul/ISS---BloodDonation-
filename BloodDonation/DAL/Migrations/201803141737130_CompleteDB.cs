namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompleteDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "Doctor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "County_Id", "dbo.Counties");
            DropForeignKey("dbo.AspNetUsers", "Locality_Id", "dbo.Localities");
            DropIndex("dbo.AspNetUsers", new[] { "Locality_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "County_Id" });
            DropIndex("dbo.Requests", new[] { "Doctor_Id" });
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        BloodType = c.Int(nullable: false),
                        RhType = c.Int(nullable: false),
                        Succesfull = c.Int(nullable: false),
                        DenialReason = c.String(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                        BloodBank_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BloodBanks", t => t.BloodBank_Id, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: false)
                .Index(t => t.User_Id)
                .Index(t => t.BloodBank_Id);
            
            CreateTable(
                "dbo.BloodTestResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Donation_Id = c.Int(nullable: false),
                        AnalyzeResult = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Donations", t => t.Donation_Id, cascadeDelete: true)
                .Index(t => t.Donation_Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Request_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Requests", t => t.Request_Id, cascadeDelete: false)
                .Index(t => t.Request_Id);
            
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Notification_Id = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notifications", t => t.Notification_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Notification_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "BloodType", c => c.Int());
            AddColumn("dbo.AspNetUsers", "RhType", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Locality_Id1", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "CNP", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Locality_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "County_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "County_Id");
            CreateIndex("dbo.AspNetUsers", "Locality_Id1");
            AddForeignKey("dbo.AspNetUsers", "County_Id", "dbo.Counties", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "Locality_Id1", "dbo.Localities", "Id");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Region");
            DropColumn("dbo.Requests", "Doctor_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "Doctor_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Region", c => c.String());
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            DropForeignKey("dbo.AspNetUsers", "Locality_Id1", "dbo.Localities");
            DropForeignKey("dbo.AspNetUsers", "County_Id", "dbo.Counties");
            DropForeignKey("dbo.UserNotifications", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserNotifications", "Notification_Id", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "Request_Id", "dbo.Requests");
            DropForeignKey("dbo.Donations", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BloodTestResults", "Donation_Id", "dbo.Donations");
            DropForeignKey("dbo.Donations", "BloodBank_Id", "dbo.BloodBanks");
            DropIndex("dbo.UserNotifications", new[] { "User_Id" });
            DropIndex("dbo.UserNotifications", new[] { "Notification_Id" });
            DropIndex("dbo.Notifications", new[] { "Request_Id" });
            DropIndex("dbo.BloodTestResults", new[] { "Donation_Id" });
            DropIndex("dbo.Donations", new[] { "BloodBank_Id" });
            DropIndex("dbo.Donations", new[] { "User_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Locality_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "County_Id" });
            AlterColumn("dbo.AspNetUsers", "County_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Locality_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "CNP", c => c.String());
            DropColumn("dbo.AspNetUsers", "Locality_Id1");
            DropColumn("dbo.AspNetUsers", "RhType");
            DropColumn("dbo.AspNetUsers", "BloodType");
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropTable("dbo.UserNotifications");
            DropTable("dbo.Notifications");
            DropTable("dbo.BloodTestResults");
            DropTable("dbo.Donations");
            CreateIndex("dbo.Requests", "Doctor_Id");
            CreateIndex("dbo.AspNetUsers", "County_Id");
            CreateIndex("dbo.AspNetUsers", "Locality_Id");
            AddForeignKey("dbo.AspNetUsers", "Locality_Id", "dbo.Localities", "Id");
            AddForeignKey("dbo.AspNetUsers", "County_Id", "dbo.Counties", "Id");
            AddForeignKey("dbo.Requests", "Doctor_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
