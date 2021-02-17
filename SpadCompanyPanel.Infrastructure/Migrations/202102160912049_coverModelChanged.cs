namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coverModelChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Covers", "ImageTitle", c => c.String());
            AlterColumn("dbo.Covers", "Title", c => c.String(maxLength: 700));
            AlterColumn("dbo.Covers", "SubTitle", c => c.String(maxLength: 2400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Covers", "SubTitle", c => c.String(nullable: false, maxLength: 2400));
            AlterColumn("dbo.Covers", "Title", c => c.String(nullable: false, maxLength: 700));
            AlterColumn("dbo.Covers", "ImageTitle", c => c.String(nullable: false));
        }
    }
}
