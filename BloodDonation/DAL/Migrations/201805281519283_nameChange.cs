namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nameChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Amount", c => c.Single(nullable: false));
            DropColumn("dbo.Products", "Àmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Àmount", c => c.Single(nullable: false));
            DropColumn("dbo.Products", "Amount");
        }
    }
}
