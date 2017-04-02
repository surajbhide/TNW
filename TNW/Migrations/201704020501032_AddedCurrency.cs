namespace TNW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddedCurrency : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrencyTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CurrencyName = c.String(),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.PortfolioAccounts", "CurrencyType_Id", c => c.Int());
            CreateIndex("dbo.PortfolioAccounts", "CurrencyType_Id");
            AddForeignKey("dbo.PortfolioAccounts", "CurrencyType_Id", "dbo.CurrencyTypes", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.PortfolioAccounts", "CurrencyType_Id", "dbo.CurrencyTypes");
            DropIndex("dbo.PortfolioAccounts", new[] { "CurrencyType_Id" });
            DropColumn("dbo.PortfolioAccounts", "CurrencyType_Id");
            DropTable("dbo.CurrencyTypes");
        }
    }
}
