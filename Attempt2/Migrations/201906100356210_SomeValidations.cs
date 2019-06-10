namespace Attempt2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeValidations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Images", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Images", "NumberOfLikes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Images", "NumberOfLikes", c => c.Int());
            AlterColumn("dbo.Images", "Title", c => c.String());
        }
    }
}
