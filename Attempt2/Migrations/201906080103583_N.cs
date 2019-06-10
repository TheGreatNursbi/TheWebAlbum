namespace Attempt2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class N : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Images", "NumberOfLikes", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Images", "NumberOfLikes", c => c.Int(nullable: false));
        }
    }
}
