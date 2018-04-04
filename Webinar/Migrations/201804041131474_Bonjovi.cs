namespace Webinar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bonjovi : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CheckingAccts", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.CheckingAccts", new[] { "ApplicationUserId" });
            AlterColumn("dbo.CheckingAccts", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.CheckingAccts", "ApplicationUserId");
            AddForeignKey("dbo.CheckingAccts", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CheckingAccts", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.CheckingAccts", new[] { "ApplicationUserId" });
            AlterColumn("dbo.CheckingAccts", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.CheckingAccts", "ApplicationUserId");
            AddForeignKey("dbo.CheckingAccts", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
