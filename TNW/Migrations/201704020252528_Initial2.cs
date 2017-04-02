namespace TNW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountTypes", "Comments", c => c.String());
            AddColumn("dbo.AssetTypes", "Comments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssetTypes", "Comments");
            DropColumn("dbo.AccountTypes", "Comments");
        }
    }
}
