namespace TNW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccountType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        AssetTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssetTypes", t => t.AssetTypeId, cascadeDelete: true)
                .Index(t => t.AssetTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountTypes", "AssetTypeId", "dbo.AssetTypes");
            DropIndex("dbo.AccountTypes", new[] { "AssetTypeId" });
            DropTable("dbo.AccountTypes");
        }
    }
}
