namespace TNW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeUserfieldRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PortfolioAccounts", "CurrencyTypeId", "dbo.CurrencyTypes");
            AddForeignKey("dbo.PortfolioAccounts", "CurrencyTypeId", "dbo.CurrencyTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PortfolioAccounts", "CurrencyTypeId", "dbo.CurrencyTypes");
            AddForeignKey("dbo.PortfolioAccounts", "CurrencyTypeId", "dbo.CurrencyTypes", "Id", cascadeDelete: true);
        }
    }
}
