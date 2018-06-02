namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeRequireFromDenialReason : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Donations", "DenialReason", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Donations", "DenialReason", c => c.String(nullable: false));
        }
    }
}
