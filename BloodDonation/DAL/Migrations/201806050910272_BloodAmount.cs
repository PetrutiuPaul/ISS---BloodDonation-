namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BloodAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bloods", "Amount", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bloods", "Amount");
        }
    }
}
