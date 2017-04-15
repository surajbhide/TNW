namespace TNW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeOwnerRequiredInPortfolio : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PortfolioAccounts", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.PortfolioAccounts", new[] { "OwnerId" });
            AlterColumn("dbo.PortfolioAccounts", "OwnerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.PortfolioAccounts", "OwnerId");
            AddForeignKey("dbo.PortfolioAccounts", "OwnerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PortfolioAccounts", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.PortfolioAccounts", new[] { "OwnerId" });
            AlterColumn("dbo.PortfolioAccounts", "OwnerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.PortfolioAccounts", "OwnerId");
            AddForeignKey("dbo.PortfolioAccounts", "OwnerId", "dbo.AspNetUsers", "Id");
        }
    }
}
