namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHospitals : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bloods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Rh = c.Int(nullable: false),
                        BloodBankId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BloodBanks", t => t.BloodBankId, cascadeDelete: true)
                .Index(t => t.BloodBankId);
            
            CreateTable(
                "dbo.BloodBanks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        County_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Counties", t => t.County_Id, cascadeDelete: true)
                .Index(t => t.County_Id);
            
            CreateTable(
                "dbo.Counties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Localities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        County_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Counties", t => t.County_Id, cascadeDelete: true)
                .Index(t => t.County_Id);
            
            CreateTable(
                "dbo.Hospitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        BloodBank_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BloodBanks", t => t.BloodBank_Id, cascadeDelete: true)
                .Index(t => t.BloodBank_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        ExpireTime = c.DateTime(nullable: false),
                        BloodBank_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BloodBanks", t => t.BloodBank_Id, cascadeDelete: true)
                .Index(t => t.BloodBank_Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CNP = c.String(nullable: false),
                        PatientName = c.String(nullable: false),
                        Priority = c.Int(nullable: false),
                        BloodType = c.Int(nullable: false),
                        Rh = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Doctor_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Doctor_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Doctor_Id);
            
            AddColumn("dbo.AspNetUsers", "Locality_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "County_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Hospital_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Locality_Id");
            CreateIndex("dbo.AspNetUsers", "County_Id");
            CreateIndex("dbo.AspNetUsers", "Hospital_Id");
            AddForeignKey("dbo.AspNetUsers", "Locality_Id", "dbo.Localities", "Id");
            AddForeignKey("dbo.AspNetUsers", "County_Id", "dbo.Counties", "Id");
            AddForeignKey("dbo.AspNetUsers", "Hospital_Id", "dbo.Hospitals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Requests", "Doctor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bloods", "BloodBankId", "dbo.BloodBanks");
            DropForeignKey("dbo.Products", "BloodBank_Id", "dbo.BloodBanks");
            DropForeignKey("dbo.AspNetUsers", "Hospital_Id", "dbo.Hospitals");
            DropForeignKey("dbo.Hospitals", "BloodBank_Id", "dbo.BloodBanks");
            DropForeignKey("dbo.BloodBanks", "County_Id", "dbo.Counties");
            DropForeignKey("dbo.AspNetUsers", "County_Id", "dbo.Counties");
            DropForeignKey("dbo.AspNetUsers", "Locality_Id", "dbo.Localities");
            DropForeignKey("dbo.Localities", "County_Id", "dbo.Counties");
            DropIndex("dbo.Requests", new[] { "Doctor_Id" });
            DropIndex("dbo.Requests", new[] { "User_Id" });
            DropIndex("dbo.Products", new[] { "BloodBank_Id" });
            DropIndex("dbo.Hospitals", new[] { "BloodBank_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Hospital_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "County_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Locality_Id" });
            DropIndex("dbo.Localities", new[] { "County_Id" });
            DropIndex("dbo.BloodBanks", new[] { "County_Id" });
            DropIndex("dbo.Bloods", new[] { "BloodBankId" });
            DropColumn("dbo.AspNetUsers", "Hospital_Id");
            DropColumn("dbo.AspNetUsers", "County_Id");
            DropColumn("dbo.AspNetUsers", "Locality_Id");
            DropTable("dbo.Requests");
            DropTable("dbo.Products");
            DropTable("dbo.Hospitals");
            DropTable("dbo.Localities");
            DropTable("dbo.Counties");
            DropTable("dbo.BloodBanks");
            DropTable("dbo.Bloods");
        }
    }
}
