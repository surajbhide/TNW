namespace TNW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeCurrencyRequiredInPortFolioAccount : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PortfolioAccounts", "CurrencyType_Id", "dbo.CurrencyTypes");
            DropIndex("dbo.PortfolioAccounts", new[] { "CurrencyType_Id" });
            AlterColumn("dbo.PortfolioAccounts", "CurrencyType_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.PortfolioAccounts", "CurrencyType_Id");
            AddForeignKey("dbo.PortfolioAccounts", "CurrencyType_Id", "dbo.CurrencyTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PortfolioAccounts", "CurrencyType_Id", "dbo.CurrencyTypes");
            DropIndex("dbo.PortfolioAccounts", new[] { "CurrencyType_Id" });
            AlterColumn("dbo.PortfolioAccounts", "CurrencyType_Id", c => c.Int());
            CreateIndex("dbo.PortfolioAccounts", "CurrencyType_Id");
            AddForeignKey("dbo.PortfolioAccounts", "CurrencyType_Id", "dbo.CurrencyTypes", "Id");
        }
    }
}
