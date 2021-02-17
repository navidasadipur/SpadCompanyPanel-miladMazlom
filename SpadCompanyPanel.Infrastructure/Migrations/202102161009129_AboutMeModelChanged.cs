namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AboutMeModelChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AboutMes", "Image", c => c.String());
            AddColumn("dbo.AboutMes", "ImageTitle", c => c.String());
            AlterColumn("dbo.AboutMes", "Biography", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AboutMes", "Biography", c => c.String(nullable: false));
            DropColumn("dbo.AboutMes", "ImageTitle");
            DropColumn("dbo.AboutMes", "Image");
        }
    }
}
