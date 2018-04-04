namespace Webinar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Blake : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CheckingAccts", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CheckingAccts", "Balance", c => c.Double(nullable: false));
        }
    }
}
