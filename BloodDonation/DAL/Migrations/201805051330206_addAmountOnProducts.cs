namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAmountOnProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "ProductType", c => c.Int(nullable: false));
            AddColumn("dbo.Requests", "Amount", c => c.Single(nullable: false));
            AddColumn("dbo.Products", "Àmount", c => c.Single(nullable: false));
            AlterColumn("dbo.Products", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Type", c => c.String(nullable: false));
            DropColumn("dbo.Products", "Àmount");
            DropColumn("dbo.Requests", "Amount");
            DropColumn("dbo.Requests", "ProductType");
        }
    }
}
