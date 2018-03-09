namespace XmlGenerator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBasicInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Templates", "UserName", c => c.String());
            AddColumn("dbo.Templates", "Email", c => c.String());
            AddColumn("dbo.Templates", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Templates", "City");
            DropColumn("dbo.Templates", "Email");
            DropColumn("dbo.Templates", "UserName");
        }
    }
}
