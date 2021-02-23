namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ArticleCategories", newName: "GalleryCategories");
            RenameTable(name: "dbo.Certificates", newName: "Covers");
            DropForeignKey("dbo.Articles", "ArticleCategoryId", "dbo.ArticleCategories");
            DropForeignKey("dbo.ArticleComments", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleComments", "ParentId", "dbo.ArticleComments");
            DropForeignKey("dbo.ArticleHeadLines", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleTags", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Articles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FoodGalleries", "Food_Id", "dbo.Foods");
            DropForeignKey("dbo.Foods", "FoodTypeId", "dbo.FoodTypes");
            DropIndex("dbo.Articles", new[] { "ArticleCategoryId" });
            DropIndex("dbo.Articles", new[] { "UserId" });
            DropIndex("dbo.ArticleComments", new[] { "ParentId" });
            DropIndex("dbo.ArticleComments", new[] { "ArticleId" });
            DropIndex("dbo.ArticleHeadLines", new[] { "ArticleId" });
            DropIndex("dbo.ArticleTags", new[] { "ArticleId" });
            DropIndex("dbo.FoodGalleries", new[] { "Food_Id" });
            DropIndex("dbo.Foods", new[] { "FoodTypeId" });
            CreateTable(
                "dbo.AboutMes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        ImageTitle = c.String(),
                        Biography = c.String(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonalCharacters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 400),
                        ShortDescription = c.String(),
                        Image = c.String(),
                        ImageTitle = c.String(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Covers", "ImageTitle", c => c.String());
            AddColumn("dbo.Covers", "SubTitle", c => c.String(maxLength: 2400));
            AddColumn("dbo.Galleries", "GalleryCategoryId", c => c.Int());
            AlterColumn("dbo.Covers", "Title", c => c.String(maxLength: 700));
            AlterColumn("dbo.Galleries", "Title", c => c.String());
            CreateIndex("dbo.Galleries", "GalleryCategoryId");
            AddForeignKey("dbo.Galleries", "GalleryCategoryId", "dbo.GalleryCategories", "Id");
            DropColumn("dbo.Covers", "Description");
            DropColumn("dbo.FoodGalleries", "Food_Id");
            DropTable("dbo.Articles");
            DropTable("dbo.ArticleComments");
            DropTable("dbo.ArticleHeadLines");
            DropTable("dbo.ArticleTags");
            DropTable("dbo.Foods");
            DropTable("dbo.FoodTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FoodTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        FoodTypeId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 300),
                        ArticleId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleHeadLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 700),
                        Description = c.String(),
                        ArticleId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        Email = c.String(nullable: false, maxLength: 400),
                        Message = c.String(nullable: false, maxLength: 800),
                        AddedDate = c.DateTime(),
                        ParentId = c.Int(),
                        ArticleId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        ViewCount = c.Int(nullable: false),
                        Image = c.String(),
                        AddedDate = c.DateTime(),
                        ArticleCategoryId = c.Int(),
                        UserId = c.String(maxLength: 128),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.FoodGalleries", "Food_Id", c => c.Int());
            AddColumn("dbo.Covers", "Description", c => c.String(nullable: false));
            DropForeignKey("dbo.Galleries", "GalleryCategoryId", "dbo.GalleryCategories");
            DropIndex("dbo.Galleries", new[] { "GalleryCategoryId" });
            AlterColumn("dbo.Galleries", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Covers", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Galleries", "GalleryCategoryId");
            DropColumn("dbo.Covers", "SubTitle");
            DropColumn("dbo.Covers", "ImageTitle");
            DropTable("dbo.PersonalCharacters");
            DropTable("dbo.AboutMes");
            CreateIndex("dbo.Foods", "FoodTypeId");
            CreateIndex("dbo.FoodGalleries", "Food_Id");
            CreateIndex("dbo.ArticleTags", "ArticleId");
            CreateIndex("dbo.ArticleHeadLines", "ArticleId");
            CreateIndex("dbo.ArticleComments", "ArticleId");
            CreateIndex("dbo.ArticleComments", "ParentId");
            CreateIndex("dbo.Articles", "UserId");
            CreateIndex("dbo.Articles", "ArticleCategoryId");
            AddForeignKey("dbo.Foods", "FoodTypeId", "dbo.FoodTypes", "Id");
            AddForeignKey("dbo.FoodGalleries", "Food_Id", "dbo.Foods", "Id");
            AddForeignKey("dbo.Articles", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ArticleTags", "ArticleId", "dbo.Articles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ArticleHeadLines", "ArticleId", "dbo.Articles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ArticleComments", "ParentId", "dbo.ArticleComments", "Id");
            AddForeignKey("dbo.ArticleComments", "ArticleId", "dbo.Articles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Articles", "ArticleCategoryId", "dbo.ArticleCategories", "Id");
            RenameTable(name: "dbo.Covers", newName: "Certificates");
            RenameTable(name: "dbo.GalleryCategories", newName: "ArticleCategories");
        }
    }
}
