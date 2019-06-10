namespace Attempt2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserNameAdding : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "UserName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "UserName");
        }
    }
}
