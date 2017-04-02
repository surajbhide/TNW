namespace TNW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCurrencyTypeIdforeignkeytoPortFolioAccount : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PortfolioAccounts", name: "CurrencyType_Id", newName: "CurrencyTypeId");
            RenameIndex(table: "dbo.PortfolioAccounts", name: "IX_CurrencyType_Id", newName: "IX_CurrencyTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PortfolioAccounts", name: "IX_CurrencyTypeId", newName: "IX_CurrencyType_Id");
            RenameColumn(table: "dbo.PortfolioAccounts", name: "CurrencyTypeId", newName: "CurrencyType_Id");
        }
    }
}
