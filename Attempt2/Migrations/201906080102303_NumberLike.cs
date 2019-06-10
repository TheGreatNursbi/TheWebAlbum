namespace Attempt2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumberLike : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "NumberOfLikes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "NumberOfLikes");
        }
    }
}
