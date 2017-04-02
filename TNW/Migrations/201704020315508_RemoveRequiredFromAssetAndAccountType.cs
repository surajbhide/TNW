namespace TNW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredFromAssetAndAccountType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountTypes", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AssetTypes", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.AccountTypes", new[] { "OwnerId" });
            DropIndex("dbo.AssetTypes", new[] { "OwnerId" });
            AlterColumn("dbo.AccountTypes", "OwnerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.AssetTypes", "OwnerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AccountTypes", "OwnerId");
            CreateIndex("dbo.AssetTypes", "OwnerId");
            AddForeignKey("dbo.AccountTypes", "OwnerId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AssetTypes", "OwnerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssetTypes", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AccountTypes", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.AssetTypes", new[] { "OwnerId" });
            DropIndex("dbo.AccountTypes", new[] { "OwnerId" });
            AlterColumn("dbo.AssetTypes", "OwnerId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AccountTypes", "OwnerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AssetTypes", "OwnerId");
            CreateIndex("dbo.AccountTypes", "OwnerId");
            AddForeignKey("dbo.AssetTypes", "OwnerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccountTypes", "OwnerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
