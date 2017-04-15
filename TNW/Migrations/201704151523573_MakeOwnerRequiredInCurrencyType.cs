namespace TNW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeOwnerRequiredInCurrencyType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CurrencyTypes", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.CurrencyTypes", new[] { "ApplicationUserId" });
            AlterColumn("dbo.CurrencyTypes", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.CurrencyTypes", "ApplicationUserId");
            AddForeignKey("dbo.CurrencyTypes", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CurrencyTypes", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.CurrencyTypes", new[] { "ApplicationUserId" });
            AlterColumn("dbo.CurrencyTypes", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.CurrencyTypes", "ApplicationUserId");
            AddForeignKey("dbo.CurrencyTypes", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
