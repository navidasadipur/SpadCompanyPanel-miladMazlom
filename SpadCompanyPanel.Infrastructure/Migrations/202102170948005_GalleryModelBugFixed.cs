namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GalleryModelBugFixed : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Galleries", name: "GalleryCategory_Id", newName: "GalleryCategoryId");
            RenameIndex(table: "dbo.Galleries", name: "IX_GalleryCategory_Id", newName: "IX_GalleryCategoryId");
            DropColumn("dbo.Galleries", "GaleryCategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Galleries", "GaleryCategoryId", c => c.Int());
            RenameIndex(table: "dbo.Galleries", name: "IX_GalleryCategoryId", newName: "IX_GalleryCategory_Id");
            RenameColumn(table: "dbo.Galleries", name: "GalleryCategoryId", newName: "GalleryCategory_Id");
        }
    }
}
