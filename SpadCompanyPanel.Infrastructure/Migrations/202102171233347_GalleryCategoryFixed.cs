namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GalleryCategoryFixed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GalleryCategories", "Title", c => c.String(nullable: false, maxLength: 400));
            DropColumn("dbo.GalleryCategories", "CategoryName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GalleryCategories", "CategoryName", c => c.String(maxLength: 700));
            DropColumn("dbo.GalleryCategories", "Title");
        }
    }
}
