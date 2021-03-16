namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someNotUsedModelsAndRepositoriesDeleted : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Testimonials");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Testimonials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        Image = c.String(),
                        Description = c.String(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
