namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somAttributesinModelsFixed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AboutMes", "ImageTitle", c => c.String(nullable: false));
            AlterColumn("dbo.Covers", "ImageTitle", c => c.String(nullable: false));
            AlterColumn("dbo.Galleries", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Galleries", "Title", c => c.String());
            AlterColumn("dbo.Covers", "ImageTitle", c => c.String());
            AlterColumn("dbo.AboutMes", "ImageTitle", c => c.String());
        }
    }
}
