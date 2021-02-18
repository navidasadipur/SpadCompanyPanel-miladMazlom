namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 700),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Galleries", "Category_Id", c => c.Int());
            AlterColumn("dbo.Galleries", "Title", c => c.String());
            CreateIndex("dbo.Galleries", "Category_Id");
            AddForeignKey("dbo.Galleries", "Category_Id", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Galleries", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Galleries", new[] { "Category_Id" });
            AlterColumn("dbo.Galleries", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Galleries", "Category_Id");
            DropTable("dbo.Categories");
        }
    }
}
