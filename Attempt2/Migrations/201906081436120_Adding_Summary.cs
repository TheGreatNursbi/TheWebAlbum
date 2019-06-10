namespace Attempt2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_Summary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Summary", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "Summary");
        }
    }
}
