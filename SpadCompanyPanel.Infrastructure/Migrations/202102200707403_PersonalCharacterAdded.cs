namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonalCharacterAdded : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PersonalCharacters");
        }
    }
}
