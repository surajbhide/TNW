namespace TNW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeOwnerRequiredInAssetType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AssetTypes", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.AssetTypes", new[] { "OwnerId" });
            AlterColumn("dbo.AssetTypes", "OwnerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AssetTypes", "OwnerId");
            AddForeignKey("dbo.AssetTypes", "OwnerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssetTypes", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.AssetTypes", new[] { "OwnerId" });
            AlterColumn("dbo.AssetTypes", "OwnerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AssetTypes", "OwnerId");
            AddForeignKey("dbo.AssetTypes", "OwnerId", "dbo.AspNetUsers", "Id");
        }
    }
}
