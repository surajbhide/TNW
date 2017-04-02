namespace TNW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddedUserToCurrency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CurrencyTypes", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.CurrencyTypes", "ApplicationUserId");
            AddForeignKey("dbo.CurrencyTypes", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.CurrencyTypes", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.CurrencyTypes", new[] { "ApplicationUserId" });
            DropColumn("dbo.CurrencyTypes", "ApplicationUserId");
        }
    }
}
