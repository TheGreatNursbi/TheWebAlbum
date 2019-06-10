namespace Attempt2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class key : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Images", new[] { "UserId" });
            DropPrimaryKey("dbo.Images");
            AlterColumn("dbo.Images", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Images", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Images", "Id");
            CreateIndex("dbo.Images", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Images", new[] { "UserId" });
            DropPrimaryKey("dbo.Images");
            AlterColumn("dbo.Images", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Images", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Images", "UserId");
            CreateIndex("dbo.Images", "UserId");
        }
    }
}
