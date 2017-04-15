namespace TNW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeOwnerRequiredInAccountType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountTypes", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.AccountTypes", new[] { "OwnerId" });
            AlterColumn("dbo.AccountTypes", "OwnerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AccountTypes", "OwnerId");
            AddForeignKey("dbo.AccountTypes", "OwnerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountTypes", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.AccountTypes", new[] { "OwnerId" });
            AlterColumn("dbo.AccountTypes", "OwnerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AccountTypes", "OwnerId");
            AddForeignKey("dbo.AccountTypes", "OwnerId", "dbo.AspNetUsers", "Id");
        }
    }
}
