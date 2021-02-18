namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GalleryModelAndGalleryCategoryModelRefactor : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Categories", newName: "GalleryCategories");
            RenameColumn(table: "dbo.Galleries", name: "Category_Id", newName: "GalleryCategory_Id");
            RenameIndex(table: "dbo.Galleries", name: "IX_Category_Id", newName: "IX_GalleryCategory_Id");
            AddColumn("dbo.Galleries", "GaleryCategoryId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Galleries", "GaleryCategoryId");
            RenameIndex(table: "dbo.Galleries", name: "IX_GalleryCategory_Id", newName: "IX_Category_Id");
            RenameColumn(table: "dbo.Galleries", name: "GalleryCategory_Id", newName: "Category_Id");
            RenameTable(name: "dbo.GalleryCategories", newName: "Categories");
        }
    }
}
