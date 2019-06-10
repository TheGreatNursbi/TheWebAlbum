namespace Attempt2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yeah : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Likes", "Image_Id", "dbo.Images");
            DropIndex("dbo.Likes", new[] { "Image_Id" });
            DropColumn("dbo.Likes", "ImageId");
            RenameColumn(table: "dbo.Likes", name: "Image_Id", newName: "ImageId");
            AlterColumn("dbo.Likes", "ImageId", c => c.Int(nullable: false));
            AlterColumn("dbo.Likes", "ImageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Likes", "ImageId");
            AddForeignKey("dbo.Likes", "ImageId", "dbo.Images", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "ImageId", "dbo.Images");
            DropIndex("dbo.Likes", new[] { "ImageId" });
            AlterColumn("dbo.Likes", "ImageId", c => c.Int());
            AlterColumn("dbo.Likes", "ImageId", c => c.String());
            RenameColumn(table: "dbo.Likes", name: "ImageId", newName: "Image_Id");
            AddColumn("dbo.Likes", "ImageId", c => c.String());
            CreateIndex("dbo.Likes", "Image_Id");
            AddForeignKey("dbo.Likes", "Image_Id", "dbo.Images", "Id");
        }
    }
}
