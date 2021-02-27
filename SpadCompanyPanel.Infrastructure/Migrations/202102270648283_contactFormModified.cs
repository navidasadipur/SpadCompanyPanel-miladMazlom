namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactFormModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactForms", "Phone", c => c.String(nullable: false, maxLength: 600));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactForms", "Phone");
        }
    }
}
